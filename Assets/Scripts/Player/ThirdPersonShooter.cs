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
    [SerializeField] private Transform PrefabBulletProjectile;
    [SerializeField] private Transform SpawnBulletPosition;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        normalSensitivity = sensitivity;
        aimSensitivity = sensitivity / 2;
    }

    private void Update()
    {
        Vector3 mouseWolrdPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {

            debugTransform.position = raycastHit.point;
            mouseWolrdPosition = raycastHit.point;
          

        }

        if (starterAssetsInputs.aim) 
        {
            AimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);


            Vector3 worldAimTarget = mouseWolrdPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            AimVirtualCamera.gameObject.SetActive(false);  
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);

        }


        if(starterAssetsInputs.shoot)
        {
            thirdPersonController.SetRotateOnMove(false);
            

            //Shoot projectile
            
            Vector3 aimDir = (mouseWolrdPosition - SpawnBulletPosition.position).normalized;
            Instantiate(PrefabBulletProjectile, SpawnBulletPosition.position, Quaternion.LookRotation(aimDir,Vector3.up));
            
            starterAssetsInputs.shoot = false;
        }
    }


}
