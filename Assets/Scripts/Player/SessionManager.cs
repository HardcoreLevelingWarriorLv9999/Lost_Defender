using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using LitJson;
using UnityEngine.TextCore.Text;
using UnityEditor;


public class SessionManager : NetworkBehaviour
{

    private static SessionManager _singleton = null;
    public static SessionManager singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = FindFirstObjectByType<SessionManager>();
            }
            return _singleton;
        }
    }

    private Dictionary<ulong, Character> _characters = new Dictionary<ulong, Character>();

    public void StartServer()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        //NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;


        NetworkManager.Singleton.StartServer();
    }
    //private void OnClientDisconnect(ulong clientId)
    //{
    //    _characters.Remove(clientId);
    //}
    private void OnClientConnected(ulong clientId)
    {

        ulong[] target = new ulong[1];
        target[0] = clientId;
        ClientRpcParams clientRpcParams = default;
        clientRpcParams.Send.TargetClientIds = target;
        OnClientConnectedClientRpc(clientRpcParams);
    }

    [ClientRpc]
    public void OnClientConnectedClientRpc(ClientRpcParams rpcParams = default)
    {
        long accountId = 0;
        SpawnCharacterServerRpc(accountId);
    }
    [ServerRpc(RequireOwnership = false)]
    public void SpawnCharacterServerRpc(long accountID,ServerRpcParams serverRpcParams = default)
    {
        Character prefab = PrefabManager.singleton.GetCharacterPrefab("Player");
        if (prefab != null)
        {
            Vector3 position = new Vector3(Random.Range(-5,5),0f,Random.Range(-5,5));

            Character character = Instantiate(prefab,position,Quaternion.identity);
            character.GetComponent<NetworkObject>().SpawnWithOwnership(serverRpcParams.Receive.SenderClientId);

            _characters.Add(serverRpcParams.Receive.SenderClientId, character);


            Dictionary<string,(string,int)> items = new Dictionary<string,(string,int)> { {"0",( "QBZ95", 30) }, { "1", ("AK74", 1) }, { "2", ("7.62x39mm", 2000) } };
            List<string> itemsId = new List<string>();
            List<string> equippedIds = new List<string>();

            for(int i = 0; i < items.Count; i++)
            {
                itemsId.Add(System.Guid.NewGuid().ToString());
            }

            string itemsJson = JsonMapper.ToJson(items);
            string itemsIdJson = JsonMapper.ToJson(itemsId);
            string equippedIdsJson = JsonMapper.ToJson(equippedIds);

            Item[] allitems = FindObjectsByType<Item>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            List<Item.Data> itemsOnGround = new List<Item.Data>();
            if(allitems != null)
            {
                for (int i  = 0; i < allitems.Length; i++)
                {
                    if (string.IsNullOrEmpty(allitems[i].networkID))
                    {
                        allitems[i].networkID = System.Guid.NewGuid().ToString();
                    }
                    if (allitems[i].transform.parent == null)
                    {
                        itemsOnGround.Add(allitems[i].GetData());
                    }

                }
            }

            string itemsOnGroundJson = JsonMapper.ToJson(itemsOnGround);

            character.InitiaLizeServer(items,itemsId,equippedIds,serverRpcParams.Receive.SenderClientId);
            character.InitializeClientRPC(itemsJson,itemsIdJson,equippedIdsJson, itemsOnGroundJson, serverRpcParams.Receive.SenderClientId);

            foreach (var client in _characters)
            {
                if (client.Value != null && client.Value != character)
                {
                    Character.Data data = client.Value.GetData();
                    string json = JsonMapper.ToJson(data);

                    ulong[] target = new ulong[1];
                    target[0] = serverRpcParams.Receive.SenderClientId;
                    ClientRpcParams clientRpcParams = default;
                    clientRpcParams.Send.TargetClientIds = target;

                    client.Value.InitializeClientRpc(json, client.Key, clientRpcParams);
                }
            }
        }
    }
    public void StartClient ()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void TradeItemsBetweenCharacters(Character character1, Character character2, Dictionary<Item, int> character1To2Items, Dictionary<Item, int> character2To1Items)
    {
        if (character1 == null || character2 == null || character1 == character2)
        {
            return;
        }
        Dictionary<string, int> serializable1To2 = new Dictionary<string, int>();
        Dictionary<string, int> serializable2To1 = new Dictionary<string, int>();
        if (character1To2Items != null)
        {
            foreach (var item in character1To2Items)
            {
                if (item.Value <= 0 && item.Key.GetType() == typeof(Ammo))
                {
                    continue;
                }
                if (item.Key != null && character1.inventory.Contains(item.Key))
                {
                    serializable1To2.Add(item.Key.networkID, item.Value);
                }
            }
        }
        if (character2To1Items != null)
        {
            foreach (var item in character2To1Items)
            {
                if (item.Value <= 0 && item.Key.GetType() == typeof(Ammo))
                {
                    continue;
                }
                if (item.Key != null && character2.inventory.Contains(item.Key))
                {
                    serializable2To1.Add(item.Key.networkID, item.Value);
                }
            }
        }
        if (serializable1To2.Count > 0 || serializable2To1.Count > 0)
        {
            string json1 = JsonMapper.ToJson(serializable1To2);
            string json2 = JsonMapper.ToJson(serializable2To1);
            TradeItemsBetweenCharactersServerRpc(character1.clientID, character2.clientID, json1, json2);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void TradeItemsBetweenCharactersServerRpc(ulong character1ID, ulong character2ID, string character1To2Json, string charactre2To1Json)
    {
        Character character1 = null;
        Character character2 = null;
        if (_characters.ContainsKey(character1ID))
        {
            character1 = _characters[character1ID];
        }
        if (_characters.ContainsKey(character2ID))
        {
            character2 = _characters[character2ID];
        }
        if (character1 == null || character2 == null || character1 == character2)
        {
            return;
        }

        Dictionary<string, int> serializable1To2 = JsonMapper.ToObject<Dictionary<string, int>>(character1To2Json);
        Dictionary<string, int> serializable2To1 = JsonMapper.ToObject<Dictionary<string, int>>(charactre2To1Json);

        Dictionary<string, int> items1To2 = new Dictionary<string, int>();
        Dictionary<string, (string, int)> splitItems1 = new Dictionary<string, (string, int)>();

        Dictionary<string, int> items2To1 = new Dictionary<string, int>();
        Dictionary<string, (string, int)> splitItems2 = new Dictionary<string, (string, int)>();

        foreach (var item in serializable1To2)
        {
            for (int i = 0; i < character1.inventory.Count; i++)
            {
                if (item.Key == character1.inventory[i].networkID)
                {
                    int count = item.Value;
                    int remained = 0;
                    if (character1.inventory[i].GetType() == typeof(Ammo))
                    {
                        Ammo ammo = (Ammo)character1.inventory[i];
                        if (count <= 0)
                        {
                            break;
                        }
                        else if (ammo.amount < count)
                        {
                            count = ammo.amount;
                        }
                        else if (ammo.amount > count)
                        {
                            remained = ammo.amount - count;
                            ammo.amount = count;
                        }
                    }
                    else if (character1.inventory[i].GetType() == typeof(Weapon))
                    {
                        count = ((Weapon)character1.inventory[i]).ammo;
                    }
                    else
                    {
                        count = 1;
                    }
                    if (remained > 0)
                    {
                        Item prefab = PrefabManager.singleton.GetItemPrefab(character1.inventory[i].id);
                        if (prefab != null)
                        {
                            Item splitItem = Instantiate(prefab, transform);
                            splitItem.networkID = System.Guid.NewGuid().ToString();
                            if (splitItem.GetType() == typeof(Ammo))
                            {
                                ((Ammo)splitItem).amount = remained;
                            }
                            character1.AddItemToInventoryLocally(splitItem);
                            splitItems1.Add(splitItem.networkID, (character1.inventory[i].id, remained));
                        }
                        else
                        {
                            break;
                        }
                    }
                    character2.AddItemToInventoryLocally(character1.inventory[i]);
                    character1.RemoveItemFromInventoryLocally(character1.inventory[i]);
                    items1To2.Add(item.Key, count);
                    break;
                }
            }
        }

        foreach (var item in serializable2To1)
        {
            for (int i = 0; i < character2.inventory.Count; i++)
            {
                if (item.Key == character2.inventory[i].networkID)
                {
                    int count = item.Value;
                    int remained = 0;
                    if (character2.inventory[i].GetType() == typeof(Ammo))
                    {
                        Ammo ammo = (Ammo)character2.inventory[i];
                        if (count <= 0)
                        {
                            break;
                        }
                        else if (ammo.amount < count)
                        {
                            count = ammo.amount;
                        }
                        else if (ammo.amount > count)
                        {
                            remained = ammo.amount - count;
                            ammo.amount = count;
                        }
                    }
                    else if (character2.inventory[i].GetType() == typeof(Weapon))
                    {
                        count = ((Weapon)character2.inventory[i]).ammo;
                    }
                    else
                    {
                        count = 1;
                    }
                    if (remained > 0)
                    {
                        Item prefab = PrefabManager.singleton.GetItemPrefab(character2.inventory[i].id);
                        if (prefab != null)
                        {
                            Item splitItem = Instantiate(prefab, transform);
                            splitItem.networkID = System.Guid.NewGuid().ToString();
                            if (splitItem.GetType() == typeof(Ammo))
                            {
                                ((Ammo)splitItem).amount = remained;
                            }
                            character2.AddItemToInventoryLocally(splitItem);
                            splitItems2.Add(splitItem.networkID, (character2.inventory[i].id, remained));
                        }
                        else
                        {
                            break;
                        }
                    }
                    character1.AddItemToInventoryLocally(character2.inventory[i]);
                    character2.RemoveItemFromInventoryLocally(character2.inventory[i]);
                    items2To1.Add(item.Key, count);
                    break;
                }
            }
        }

        if (items2To1.Count > 0 || items1To2.Count > 0)
        {
            string json1To2 = JsonMapper.ToJson(items1To2);
            string json1Split = JsonMapper.ToJson(splitItems1);
            string json2To1 = JsonMapper.ToJson(items2To1);
            string json2Split = JsonMapper.ToJson(splitItems2);
            TradeItemsBetweenCharactersClientRpc(character1ID, character2ID, json1To2, json1Split, json2To1, json2Split);
        }
    }

    [ClientRpc]
    private void TradeItemsBetweenCharactersClientRpc(ulong character1ID, ulong character2ID, string json1To2, string json1Split, string json2To1, string json2Split)
    {
        Character character1 = null;
        Character character2 = null;
        Character[] allCharacters = FindObjectsByType<Character>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        if (allCharacters != null)
        {
            for (int i = 0; i < allCharacters.Length; i++)
            {
                if (allCharacters[i].clientID == character1ID)
                {
                    character1 = allCharacters[i];
                }
                else if (allCharacters[i].clientID == character2ID)
                {
                    character2 = allCharacters[i];
                }
                if (character1 != null && character2 != null)
                {
                    break;
                }
            }
        }
        if (character1 == null || character2 == null || character1 == character2)
        {
            return;
        }

        Dictionary<string, int> items1To2 = JsonMapper.ToObject<Dictionary<string, int>>(json1To2);
        Dictionary<string, (string, int)> splitItems1 = JsonMapper.ToObject<Dictionary<string, (string, int)>>(json1Split);
        Dictionary<string, int> items2To1 = JsonMapper.ToObject<Dictionary<string, int>>(json2To1);
        Dictionary<string, (string, int)> splitItems2 = JsonMapper.ToObject<Dictionary<string, (string, int)>>(json2Split);

        foreach (var item in items1To2)
        {
            bool found = false;
            for (int i = 0; i < character1.inventory.Count; i++)
            {
                if (character1.inventory[i].networkID == item.Key)
                {
                    if (character1.inventory[i].GetType() == typeof(Ammo))
                    {
                        ((Ammo)character1.inventory[i]).amount = item.Value;
                    }
                    else if (character1.inventory[i].GetType() == typeof(Weapon))
                    {
                        ((Weapon)character1.inventory[i]).ammo = item.Value;
                    }
                    character2.AddItemToInventoryLocally(character1.inventory[i]);
                    character1.RemoveItemFromInventoryLocally(character1.inventory[i]);
                    found = true;
                    break;
                }
            }
            if (found == false)
            {
                // Problem
            }
        }

        foreach (var item in splitItems1)
        {
            Item prefab = PrefabManager.singleton.GetItemPrefab(item.Value.Item1);
            if (prefab != null)
            {
                Item splitItem = Instantiate(prefab, transform);
                splitItem.networkID = item.Key;
                if (item.Key.GetType() == typeof(Ammo))
                {
                    ((Ammo)splitItem).amount = item.Value.Item2;
                }
                character1.AddItemToInventoryLocally(splitItem);
            }
        }

        foreach (var item in items2To1)
        {
            bool found = false;
            for (int i = 0; i < character2.inventory.Count; i++)
            {
                if (character2.inventory[i].networkID == item.Key)
                {
                    if (character2.inventory[i].GetType() == typeof(Ammo))
                    {
                        ((Ammo)character2.inventory[i]).amount = item.Value;
                    }
                    else if (character2.inventory[i].GetType() == typeof(Weapon))
                    {
                        ((Weapon)character2.inventory[i]).ammo = item.Value;
                    }
                    character1.AddItemToInventoryLocally(character2.inventory[i]);
                    character2.RemoveItemFromInventoryLocally(character2.inventory[i]);
                    found = true;
                    break;
                }
            }
            if (found == false)
            {
                // Problem
            }
        }

        foreach (var item in splitItems2)
        {
            Item prefab = PrefabManager.singleton.GetItemPrefab(item.Value.Item1);
            if (prefab != null)
            {
                Item splitItem = Instantiate(prefab, transform);
                splitItem.networkID = item.Key;
                if (item.Key.GetType() == typeof(Ammo))
                {
                    ((Ammo)splitItem).amount = item.Value.Item2;
                }
                character2.AddItemToInventoryLocally(splitItem);
            }
        }
    }
}
