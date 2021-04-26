using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSFX : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip batAttack;
    [SerializeField] public AudioClip batDie;

    public void PlayBatAttackSFX()
    {
        audioSource.PlayOneShot(batAttack);
    }

    public void PlayBatDieSFX()
    {
        AudioSource.PlayClipAtPoint(batDie, Camera.main.transform.position);
    }

}
