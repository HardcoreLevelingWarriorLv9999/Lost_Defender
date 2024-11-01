using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource shootingChannel;

    public AudioClip M1911Shot;
    public AudioClip AK74Shot;
    public AudioClip BennelliM4Shot;

    public AudioSource reloadingSoundAK74;
    public AudioSource reloadingSoundM1911;
    public AudioSource reloadingSoundBennelliM4;

    

    public AudioSource emptyMagazineSoundM1911;

    public AudioSource throwablesChannel;
    public AudioClip grenadeSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.M1911:
                shootingChannel.PlayOneShot(M1911Shot); 
                break;
            case WeaponModel.AK74:
                shootingChannel.PlayOneShot(AK74Shot);
                break;
            case WeaponModel.BennelliM4:
                shootingChannel.PlayOneShot(BennelliM4Shot);
                break;
        }
    }
    public void PlayReloadingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.M1911:
                reloadingSoundM1911.Play();
                break;
            case WeaponModel.AK74:
                reloadingSoundAK74.Play();
                break;
            case WeaponModel.BennelliM4:
                reloadingSoundBennelliM4.Play();
                break;
        }
    }
}
