using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSFX : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip ghostAttack;
    [SerializeField] public AudioClip ghostDie;


    public void PlayGhostAttackSFX()
    {
        audioSource.PlayOneShot(ghostAttack);
    }

    public void PlayGhostDieSFX()
    {
        AudioSource.PlayClipAtPoint(ghostDie, Camera.main.transform.position);
    }
}
