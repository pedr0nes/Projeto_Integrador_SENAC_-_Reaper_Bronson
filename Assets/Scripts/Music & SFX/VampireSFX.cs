using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSFX : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip vampireSpawn;
    [SerializeField] public AudioClip vampireAttack;
    [SerializeField] public AudioClip vampireDie;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayVampireSpawnSFX()
    {
        audioSource.PlayOneShot(vampireSpawn);
    }

    public void PlayVampireAttackSFX()
    {
        audioSource.PlayOneShot(vampireAttack);
    }

    public void PlayVampireDieSFX()
    {
        AudioSource.PlayClipAtPoint(vampireDie, Camera.main.transform.position);
    }
}
