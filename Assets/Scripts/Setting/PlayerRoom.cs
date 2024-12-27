using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRoom : MonoBehaviour
{
    private PhotonView photonview;
    public MonoBehaviour scriptA; 
    public MonoBehaviour scriptB;
    public MonoBehaviour scriptC;
    public MonoBehaviour scriptD;
    public MonoBehaviour scriptE;
    public MonoBehaviour scriptF;
    public MonoBehaviour scriptAA;
    public MonoBehaviour scriptBB;
    public MonoBehaviour scriptCC;
    public GameObject JUTPSDefaultUserInterface;
    public GameObject ThirdPersonCameraControllerVariant;
    private void Awake()
    {
        photonview = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (photonview.IsMine)
        {
            scriptA.enabled = true;
            scriptB.enabled = true;
            scriptC.enabled = true;
            scriptD.enabled = true;
            scriptE.enabled = true;
            scriptF.enabled = true;
            scriptAA.enabled = true;
            scriptBB.enabled = true;
            scriptCC.enabled = true;
            JUTPSDefaultUserInterface.SetActive(true);
            ThirdPersonCameraControllerVariant.SetActive(true);
            gameObject.layer = LayerMask.NameToLayer("Charater");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
