using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody BulletRigibody;
    [SerializeField] public float speed = 10f;
    private void Awake()
    {
        BulletRigibody = GetComponent<Rigidbody>();
        BulletRigibody.useGravity = false;
    }

    private void Start()
    {
        BulletRigibody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if(other.gameObject.CompareTag("Target"))
        {
            Destroy(other.gameObject);
            Debug.Log("hit");

        }
    }
}
