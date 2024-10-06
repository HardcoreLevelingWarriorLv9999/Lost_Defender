using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsManager : MonoBehaviour
{
    public Item[] _items = null;
    private  static PrefabsManager _singleton = null;

    public static PrefabsManager singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = FindFirstObjectByType<PrefabsManager>();
            }
            return _singleton;
        }
    }

    public Item GetItemPrefabs(string id) 
    {
        if(_items != null)
        {
            for(int i = 0; i < _items.Length; i++) 
            {
                if (_items[i] != null && _items[i].id == id)
                {
                    return _items[i];
                }
            }
        }

        return null;
    }
}
