using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip playerJump;
    [SerializeField] public AudioClip scytheAttack;
    [SerializeField] public AudioClip weaponHit;
    [SerializeField] public AudioClip gunShot;
    [SerializeField] public AudioClip drawGun;
    [SerializeField] public AudioClip takeDamage;
    [SerializeField] public AudioClip death;
    


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayJumpSFX()
    {
        audioSource.PlayOneShot(playerJump);
    }

    public void PlayScytheAttackSFX()
    {
        audioSource.PlayOneShot(scytheAttack);
    }

    public void PlayWeaponHitSFX()
    {
        audioSource.PlayOneShot(weaponHit);
    }

    public void PlayGunShotSFX()
    {
        audioSource.PlayOneShot(gunShot);
    }

    public void PlayDrawGunSFX()
    {
        audioSource.PlayOneShot(drawGun);
    }

    public void PlayTakeDamageSFX()
    {
        audioSource.PlayOneShot(takeDamage);
    }

    public void PlayDeathSFX()
    {
        audioSource.PlayOneShot(death);
    }

}
