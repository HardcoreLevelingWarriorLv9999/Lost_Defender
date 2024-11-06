using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage;

    private void OnCollisionEnter(Collision objectWeHit)
    {
        CreateBulletImpactEffect(objectWeHit);

        Destroy(gameObject);

        var hitbox = objectWeHit.collider.GetComponent<Hitbox>();
        if (hitbox)
        {
            Vector3 direction = transform.forward;
            hitbox.OnHit(this, direction);
        }
    }

    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        // Debug.Log("Created a bullet hole");
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
