using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSFX : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip zombieSpawn;
    [SerializeField] public AudioClip zombieAttack;
    [SerializeField] public AudioClip zombieDie;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayZombieSpawnSFX()
    {
        audioSource.PlayOneShot(zombieSpawn);
    }

    public void PlayZombieAttackSFX()
    {
        audioSource.PlayOneShot(zombieAttack);
    }

    public void PlayZombieDieSFX()
    {

            AudioSource.PlayClipAtPoint(zombieDie, Camera.main.transform.position);
    }

}
