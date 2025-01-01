using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagementMap1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.StopMusic("Nen");
        AudioManager.Instance.PlayMusic("startMap1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
