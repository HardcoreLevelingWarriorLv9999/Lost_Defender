using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem.XR;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;
using static UnityEngine.ParticleSystem;

public class Character : MonoBehaviour
{
    public bool isLocalPlayer = false;
    [SerializeField] private string _id = ""; public string id {  get { return _id; } }
    [SerializeField] private Transform _weaponHolder = null;

    private Weapon _weapon = null; public Weapon weapon { get { return _weapon; } }
    private Ammo _ammo = null; public Ammo ammo { get { return _ammo; } }

    private List<Item> _items = new List<Item>();

    private RigManager _rigManager = null;
    private Animator _animator= null;
    private Weapon _weaponToEquip = null;
    private bool _reloading = false; public bool reloading { get { return _reloading;} }
    private bool _switchingWeapon = false; public bool switchingWeapon { get { return _switchingWeapon;} }


    private Rigidbody[] _ragdollRigibodies = null;
    private Collider[] _ragdollColliders = null;
    [SerializeField]
    private float _health = 100;

    private bool _grounded = false; public bool isGrounded { get { return _grounded;} set { _grounded = value; } }
    private bool _walking = false; public bool walking { get { return _walking;} set { _walking = value; } }
    private float _speedAnimationMultiplier = 0f; public float speedAnimationMultiplier { get { return _speedAnimationMultiplier; } }
    private bool _aiming = false; public bool aiming { get { return _aiming; } set{ _aiming = value; } }
    private bool _sprinting = false;public bool sprinting { get { return _sprinting;} set { _sprinting = value; } } 
    private float _aimLayerWeight = 0;

    private Vector2 _aimingMovingAnimationInput = Vector2.zero;
    public float leftHandWeight = 0;
    public float aimRigWeight = 0;

    private Vector3 _aimTarget = Vector3.zero; public Vector3 aimTarget {  get { return _aimTarget; } set {  _aimTarget = value; } }

    private  Vector3 _lastPosition = Vector3.zero;
    private void Awake()
    {
        _ragdollRigibodies = GetComponentsInChildren<Rigidbody>();
        _ragdollColliders = GetComponentsInChildren<Collider>();

        if( _ragdollRigibodies != null )
        {
            for(int i = 0; i < _ragdollRigibodies.Length; i++)
            {
                _ragdollRigibodies[i].mass *= 50;
            }
        }if( _ragdollColliders != null )
        {
            for(int i = 0; i < _ragdollColliders.Length; i++)
            {
                _ragdollColliders[i].isTrigger = false;
            }
        }
        SetRagdollStatus(false);

        _rigManager = GetComponent<RigManager>();
        _animator = GetComponent<Animator>();
        Initialize(new Dictionary<string, int> { { "M4A1", 1 }, { "AN94",1}, { "7.62x39mm", 1000} } );
    }

    private void Start()
    {
        if(isLocalPlayer)
        {
            SetLayer(transform, LayerMask.NameToLayer("LocalPlayer"));

        }
        else
        {
            SetLayer(transform, LayerMask.NameToLayer("NetworkPlayer"));
        }
    }
    private void Update()
    {
        bool armed = weapon != null;

        
        _aimLayerWeight = Mathf.Lerp(_aimLayerWeight, _switchingWeapon ||
                                    (armed && (_aiming || _reloading)) ? 1f : 0, 10f * Time.deltaTime);
        _animator.SetLayerWeight(1, _aimLayerWeight);

        aimRigWeight = Mathf.Lerp(aimRigWeight, armed && _aiming && !_reloading ? 1f : 0f, 10f * Time.deltaTime);
        leftHandWeight = Mathf.Lerp(leftHandWeight, armed && !_switchingWeapon && !_reloading &&
                                                    (_aiming || (_grounded && _weapon.type == Weapon.Handle.TwoHanded))
                                                    ? 1f : 0f, 10 * Time.deltaTime);

        _rigManager.aimTarget = _aimTarget;
        _rigManager.aimWeight = aimRigWeight;
        _rigManager.leftHandWeight = leftHandWeight;

        if (_sprinting)
        {
           
            _speedAnimationMultiplier = 3;
        }
        else if (_walking)
        {
            
            _speedAnimationMultiplier = 1;
        }
        else
        {
            _speedAnimationMultiplier = 2;
        }

        Vector3 deltaPosition = transform.InverseTransformDirection(transform.position - _lastPosition).normalized;

        _aimingMovingAnimationInput = Vector2.Lerp(_aimingMovingAnimationInput,new Vector2(deltaPosition.x,deltaPosition.z) * _speedAnimationMultiplier, 30f * Time.deltaTime);
        _animator.SetFloat("Speed_X", _aimingMovingAnimationInput.x);
        _animator.SetFloat("Speed_Y", _aimingMovingAnimationInput.y);
        _animator.SetFloat("Armed", armed ? 1 : 0);
        _animator.SetFloat("Aimed", _aiming ? 1 : 0);
        _lastPosition = transform.position;


    }

    private void LateUpdate()
    {
    }
    public void SetRagdollStatus(bool enabled)
    {
        if(_ragdollRigibodies != null)
        {
            for(int i = 0; i < _ragdollRigibodies.Length;i++)
            {
                _ragdollRigibodies[i].isKinematic = !enabled;
            }
        }
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
                _weaponToEquip = (Weapon)_items[firstWeaponIndex];
                OnEquip();
            }
        }
    }

    public void ChangeWeapon(float direction)
    {
        int x = direction > 0 ? 1 : direction < 0 ? -1 : 0;
        if(x != 0 && _switchingWeapon == false)
        {
            if(x > 0)
            {
                NextWeapon();
            }
            else
            {
                PrevWeapon();
            }

        }
    }

    private void NextWeapon()
    {
        int first = -1;
        int current = -1;
        for(int i =0; i < _items.Count; i++)
        {
            if (_items[i] != null && _items[i].GetType()== typeof(Weapon))
            {
                if(_weapon != null && _items[i].gameObject ==  _weapon.gameObject)
                {
                    current = i;
                }
                else
                {
                    if(current >= 0)
                    {
                        EquipWeapon((Weapon)_items[i]);
                        return;
                    }
                    else if(first < 0)
                    {
                        first = i;
                    }
                }
            }
        }

        if(first >= 0)
        {
            EquipWeapon((Weapon)_items[first]);
            
        }
    }
    
    private void PrevWeapon()
    {
        int last = -1;
        int current = -1;
        for (int i = _items.Count -1; i >= 0; i--)
        {
            if (_items[i] != null && _items[i].GetType() == typeof(Weapon))
            {
                if (_weapon != null && _items[i].gameObject == _weapon.gameObject)
                {
                    current = i;
                }
                else
                {
                    if (current >= 0)
                    {
                        EquipWeapon((Weapon)_items[i]);
                        return;
                    }
                    else if (last < 0)
                    {
                        last = i;
                    }
                }
            }
        }
        if(last >= 0)
        {
            EquipWeapon((Weapon)_items[last]);
        }
    }
    public void EquipWeapon (Weapon weapon)
    {
        if(_switchingWeapon || weapon == null)
        {
            return;
        }
        _weaponToEquip = weapon; //lưu vũ khí 
        if (_weapon != null)
        {
            HolsterWeapon();// tháo vũ khí khi nếu đã có vũ khí
        }
        else
        {
            //nếu chưa có vũ khí thì trang bị vào
            _switchingWeapon = true;
            _animator.SetTrigger("Equip");
        }
    }

    //trang bị vũ khí
    //gọi sau khi trigger "Equip" kích hoạt
    private void _EquipWeapon()
    {
        if(_weaponToEquip != null)
        {
            _weapon = _weaponToEquip;
            _weaponToEquip = null;  
            //cập nhật dữ liệu vị trí và góc quay đúng cho vũ khí
            if (_weapon.transform.parent != _weaponHolder)
            {
                _weapon.transform.SetParent(_weaponHolder);
                _weapon.transform.localPosition = _weapon.rightHandPosition;
                _weapon.transform.localEulerAngles = weapon.rightHandRotaion;
            }
            _rigManager.SetLeftHandGripData(_weapon.leftHandPosition, _weapon.leftHandRotaion);
            _weapon.gameObject.SetActive(true);

            //tìm đạn phù hợp cho vũ khí
            _ammo = null;
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null && _items[i].GetType() == typeof(Ammo) && _weapon.ammoId == _items[i].id)
                {
                    _ammo = (Ammo)_items[i];
                    break;
                }
            }
        }
        
    }
    //gọi hàm trang bị vũ khí
    public void OnEquip()
    {
        _EquipWeapon();
    }

    private void _HolsterWeapon()
    {
        if (_weapon != null)
        {
            _weapon.gameObject.SetActive(false);
            _weapon = null;
            _ammo = null;
        }
    } 

    //hàm tháo vũ khí 
    public void HolsterWeapon()
    {
        if(_switchingWeapon)
        {
            return;
        }
        if(_weapon != null)
        {
            _switchingWeapon = true;
            _animator.SetTrigger("Holster");
        }

    } 
    //tháo vũ khí 
    //gọi sau khi kích hoạt trigger "Holster"
    public void OnHolster()
    {
        _HolsterWeapon();
        if(_weaponToEquip != null)
        {
            OnEquip();
        }
    }
    public void ApplyDamage(Character shooter, Transform hit, float damage)
    {
        if(_health > 0)
        {
            _health -= damage;
            if (_health <= 0)
            {
                _health = 0;
                SetRagdollStatus(true);
                Destroy(_rigManager);
                Destroy(GetComponent<RigBuilder>());
                Destroy(_animator);


                ThirdPersonController thirdPersonController = GetComponent<ThirdPersonController>();
                if (thirdPersonController != null)
                {
                    Destroy(thirdPersonController);
                }
                CharacterController controller = GetComponent<CharacterController>();
                if (controller != null)
                {
                    Destroy(controller);
                }
                Destroy(this);
            }
        }

    }
    public void Reload() // call when is reloading 
    {
        if (_weapon != null && !_reloading && _weapon.ammo < _weapon.clipSize && _ammo != null && _ammo.amount > 0)
        {
            _animator.SetTrigger("Reload");
            _reloading = true; ;
        }
    }
    public void ReloadFininshed() // call when reloading is finnished
    {
        if(_weapon != null && _weapon.ammo < _weapon.clipSize && _ammo != null && _ammo.amount > 0)
        {
            int amount = _weapon.clipSize - _weapon.ammo;
            if(_ammo.amount < amount)
            {
                amount = _ammo.amount;
            }

            _ammo.amount -= amount;
            _weapon.ammo += amount;
        }
        _reloading = false;
    }

    public void EquipFinished()
    {
        _switchingWeapon = false;
    }
    public void HolsterFinished()
    {
        _switchingWeapon = false;
    }
    

    private void SetLayer(Transform root, int  layer)
    {
        var children = root.GetComponentsInChildren<Transform>(true);
        foreach(var child in children)
        {
            child.gameObject.layer = layer;
        }
    }
}
