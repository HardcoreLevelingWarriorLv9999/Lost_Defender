using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform _weaponHolder = null;

    private Weapon _weapon = null; public Weapon weapon { get { return _weapon; } }

    private List<Item> _items = new List<Item>();

    private RigManager _rigManager = null;

    private void Awake()
    {
        _rigManager = GetComponent<RigManager>();
        Initialize(new Dictionary<string, int> { { "AN94", 1 } });
    }

    public void Initialize(Dictionary<string, int> items)
    {
        if(items != null && PrefabsManager.singleton != null)
        {
            int firstWeaponIndex = -1;

            foreach(var itemData in items)
            {
                Item prefab = PrefabsManager.singleton.GetItemPrefabs(itemData.Key);
                if(prefab != null && itemData.Value > 0)
                {
                    for(int i = 1; i <= itemData.Value; i++)
                    {
                        bool done = false;
                        Item item = Instantiate(prefab,transform);

                        if(item.GetType() == typeof(Weapon))
                        {
                            Weapon w = (Weapon)item;
                            item.transform.SetParent(_weaponHolder);
                            item.transform.localPosition = w.rightHandPosition;
                            item.transform.localEulerAngles = w.rightHandRotaion;

                            if(firstWeaponIndex < 0)
                            {
                                firstWeaponIndex = _items.Count;
                            }
                        }
                        else if( item.GetType() == typeof(Ammo))
                        {
                            Ammo a = (Ammo)item;
                            a.amount = itemData.Value;  

                            done = true;
                        }
                        item.gameObject.SetActive(false);
                        _items.Add(item);
                        if (done) break;
                        
                    }  
                }
            }
            if(firstWeaponIndex >= 0 && _weapon == null)
            {
                EquipWeapon((Weapon)_items[firstWeaponIndex]);
                
            }
        }
    }

    public void EquipWeapon (Weapon weapon)
    {
        if(_weapon != null)
        {
            HolsterWeapon();
        }
        if(weapon != null)
        {
            if(weapon.transform.parent != _weaponHolder)
            {
                weapon.transform.SetParent(_weaponHolder);
                weapon.transform.localPosition = weapon.rightHandPosition;
                weapon.transform.localEulerAngles = weapon.rightHandRotaion;
            }
            _rigManager.SetLeftHandGripData(weapon.leftHandPosition, weapon.leftHandRotaion);
            weapon.gameObject.SetActive(true);
            _weapon = weapon;

        }
    }

    public void HolsterWeapon()
    {
        if (_weapon != null)
        {
            _weapon.gameObject.SetActive(false );
            _weapon = null;
        }
    }
}
