using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using System.Net.Sockets;

public class ThirdPersonShooter : MonoBehaviour
{
   [SerializeField] private CinemachineVirtualCamera AimVirtualCamera;

    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;
    [Range(0f, 10f)]
    [SerializeField] private float sensitivity;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        normalSensitivity = sensitivity;
        aimSensitivity = sensitivity / 2;
    }

    private void Update()
    {
        if(starterAssetsInputs.aim) 
        {
            AimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
        }
        else
        {
            AimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
        }


        Vector2 screenCenterPoint = new Vector2 (Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            transform.position = raycastHit.point;
        }
    }


}
