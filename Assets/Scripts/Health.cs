using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
   public int health;
   public bool IsLocalPlayer;

   
   [Header("UI")]
   public TextMeshProUGUI healthText;


   [PunRPC]
   public void TakeDamge(int _damage)
   {
        health -= _damage;

        healthText.text = health.ToString();
    
            if (health <= 0)
            {
                if(IsLocalPlayer)
                    RoomManager.instance.SpawnPlayer();

                Destroy(gameObject);
            }
   }
}
