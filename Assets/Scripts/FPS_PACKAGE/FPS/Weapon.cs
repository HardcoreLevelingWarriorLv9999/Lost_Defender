using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;

    public bool isActiveWeapon;
    public int weaponDamage;


    [Header("Shooting")]
    public bool isShooting, readyToShoot;
    public bool allowReset = true;
    public float shootingDelay = 0.5f;

    [Header("Burst")]
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    [Header("Spread")]
    public float spreadIntensity;
    public float hipSpreadIntensity;
    public float adsSpreadIntensity;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;
    internal Animator animator;

    [Header("Reloading")]
    public float reloadTime;
    public float fullReloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    [Header("Original Spawn Values")]
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    bool isADS;
    public enum WeaponModel
    {
        M1911,
        AK74,
        BenelliM4
    }
    public WeaponModel thisWeaponModel;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;

        spreadIntensity = hipSpreadIntensity;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActiveWeapon)
        {
            if (Input.GetMouseButtonDown(1))
            {
                EnterADS();
            }
            if (Input.GetMouseButtonUp(1))
            {
                ExitADS();
            }

            GetComponent<Outline>().enabled = false;
            if (bulletsLeft == 0 && isShooting)
            {
                SoundManager.Instance.emptyMagazineSoundM1911.Play();
            }
            if (currentShootingMode == ShootingMode.Auto)
            {
                // Holding down left mouse button
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Single ||
                currentShootingMode == ShootingMode.Burst)
            {
                // Clicking left mouse button once
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && bulletsLeft != 0 && isReloading == false && WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel) > 0)
            {
                Reload();
            }
            // if you want to automatically reload when magazine is empty
            if (readyToShoot && isShooting == false && isReloading == false && bulletsLeft <= 0 && WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel) > 0)
            {
                EmptyReload();
            }
            if (readyToShoot && isShooting && bulletsLeft > 0 && isReloading == false)
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }

            else
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Default");
                }
            }
        }
    }

    private void FireWeapon()
    {

        bulletsLeft--;
        muzzleEffect.GetComponent<ParticleSystem>().Play();

        if (isADS)
        {
            animator.SetTrigger("RECOIL_ADS");
        }
        else
        {
            animator.SetTrigger("RECOIL");

            // Recoil ENDING
            animator.SetBool("IsRecoil", true);
            Debug.Log("1");
        }


        //SoundManager.Instance.shootingChannel.Play();
        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        Bullet bul = bullet.GetComponent<Bullet>();
        bul.bulletDamage = weaponDamage;

        // Pointing the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;

        // Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        // Destroy the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        // Checking if we're done shooting
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        // Burst mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1) // we already shot once before this check
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }
    private void EnterADS()
    {
        animator.SetTrigger("enterADS");
        isADS = true;
        HUDManager.Instance.middleDot.SetActive(false);
        spreadIntensity = adsSpreadIntensity;
    }
    private void ExitADS()
    {
        animator.SetTrigger("exitADS");
        isADS = false;
        HUDManager.Instance.middleDot.SetActive(true);
        spreadIntensity = hipSpreadIntensity;
    }
    private void Reload()
    {
        animator.SetTrigger("RELOAD");

        //SoundManager.Instance.reloadingSoundM1911.Play();
        SoundManager.Instance.PlayReloadingSound(thisWeaponModel);

        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }
    private void EmptyReload()
    {
        animator.SetTrigger("FULLRELOAD");

        //SoundManager.Instance.reloadingSoundM1911.Play();
        SoundManager.Instance.PlayReloadingSound(thisWeaponModel);

        isReloading = true;
        Invoke("ReloadCompleted", fullReloadTime);
    }
    void EndRecoilAnimation()
    { // Set the animation state to idle or reset the trigger
        animator.SetBool("IsRecoil", false);
    }
    private void ReloadCompleted()
    {
        if (WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel) > magazineSize)
        {
            bulletsLeft = magazineSize;
            WeaponManager.Instance.DecreaseTotalAmmo(bulletsLeft, thisWeaponModel);
        }
        else
        {
            bulletsLeft = WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel);
            WeaponManager.Instance.DecreaseTotalAmmo(bulletsLeft, thisWeaponModel);
        }

        isReloading = false;
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;

    }
    public Vector3 CalculateDirectionAndSpread()
    {
        // Shooting from the middle of the screen to check what we're shooting at
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            // Hitting something
            targetPoint = hit.point;
        }
        else
        {
            // Shooting at the air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float z = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        // Returning the shooting direction and spread
        return direction + new Vector3(0, y, z);
    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
    
