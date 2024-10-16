using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private string _id = "";public string id {  get { return _id; } }
    [SerializeField] private string _networkID = "";public string networkID {  get { return _networkID; } set { _networkID = value; } }

    private Rigidbody _rigidbody = null;
    private Collider _collider = null;

    private bool _canBePickedUp = false; public bool canBePickedUp { get { return _canBePickedUp; } set{ value = _canBePickedUp; }}
    private bool _initialized = false;

    [System.Serializable]
    public struct Data
    {
        public string id; 
        public string networkId;
        public int value;
        public float[] position;
        public float[] rotaion;
    }

    public Data GetData()
    {
        Data data = new Data();
        data.id = id;
        data.networkId = networkID;
        if (this is Weapon)
        {
            data.value = ((Weapon)this).ammo;
        }
        else if (this is Ammo)
        {
            data.value = ((Ammo)this).amount;
        }

        data.position = new float[3];
        data.position[0] = transform.position.x;
        data.position[1] = transform.position.y;
        data.position[2] = transform.position.z;   

        data.rotaion = new float[3];
        data.rotaion[0] = transform.eulerAngles.x;
        data.rotaion[1] = transform.eulerAngles.y;
        data.rotaion[2] = transform.eulerAngles.z;

        return data;
    }


    //dùng để override sang script Weapon
    public virtual void Awake()
    {
        Initialize();
    }

    public virtual void Start() 
    {
        if(transform.parent == null) //nếu chưa nhặt lên thì set item ở trên mặt đất
        {
            SetOnGroundStatus(true);
        }
    }



    // hàm để gọi khởi tạo item
    public void Initialize()
    {
        if( _initialized )
        {
            return;
        }
        
        _initialized = true;
        gameObject.tag = "Item";
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _collider.isTrigger = false;
        _rigidbody.mass = 40f;


    }

    //hàm thiết lập trạng thái cho item
    public void SetOnGroundStatus(bool status)
    {
        _rigidbody.isKinematic = !status;
        _collider.enabled = status;
        _canBePickedUp = status;
    }


}
