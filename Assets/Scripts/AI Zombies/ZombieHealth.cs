using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth; // máu tối đa
    public float currentHealth; // máu hiện tại
    public float hitForce;// lực đẩy
    Ragdoll ragdoll; 
    
    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        currentHealth = maxHealth;

        //thêm script Hitbox vào từng bộ phận có gắn Rigibody và liên kết tới zombieHealth của Hitbox
        var rigiBodies = GetComponentsInChildren<Rigidbody>();
        foreach(var rigiBody in rigiBodies)
        {
            Hitbox hitbox = rigiBody.gameObject.AddComponent<Hitbox>();
            hitbox.zombieHealth = this;
        }
    }

    // hàm nhận sát thương
    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        if(currentHealth <= 0.0f)
        {
            Die(direction);
        }
    }

    //hàm chết
    private void Die(Vector3 direction)
    {
        ragdoll.ActivateRagdoll();
        direction.y = 1;
        ragdoll.ApplyForce(direction * hitForce);
    }
}
