using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        string playerName = PlayerPrefs.GetString("PlayerName");
        GameObject player = PhotonNetwork.Instantiate(playerName, new Vector3(0f, 1f, 2f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
