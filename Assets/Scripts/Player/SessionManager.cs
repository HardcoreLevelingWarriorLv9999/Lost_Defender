using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using LitJson;
using UnityEngine.TextCore.Text;


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
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;


        NetworkManager.Singleton.StartServer();
    }
    private void OnClientDisconnect(ulong clientId)
    {
        _characters.Remove(clientId);
    }
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
        Character prefab = PrefabsManager.singleton.GetCharracterPrefabs("Player");
        if (prefab != null)
        {
            Vector3 position = new Vector3(Random.Range(-5,5),0f,Random.Range(-5,5));

            Character character = Instantiate(prefab,position,Quaternion.identity);
            character.GetComponent<NetworkObject>().SpawnWithOwnership(serverRpcParams.Receive.SenderClientId);

            _characters.Add(serverRpcParams.Receive.SenderClientId, character);


            Dictionary<string,int> items = new Dictionary<string,int> { { "QBZ95", 1 }, { "AK74", 1 }, { "7.62x39mm", 2000 } };
            List<string> itemsId = new List<string>();
            List<string> equippedIds = new List<string>();

            for(int i = 0; i < items.Count; i++)
            {
                itemsId.Add(System.Guid.NewGuid().ToString());
            }

            string itemsJson = JsonMapper.ToJson(items);
            string itemsIdJson = JsonMapper.ToJson(itemsId);
            string equippedIdsJson = JsonMapper.ToJson(equippedIds);

            character.InitiaLizeServer(items,itemsId,equippedIds,serverRpcParams.Receive.SenderClientId);
            character.InitializeClientRPC(itemsJson,itemsIdJson,equippedIdsJson,serverRpcParams.Receive.SenderClientId);

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
}
