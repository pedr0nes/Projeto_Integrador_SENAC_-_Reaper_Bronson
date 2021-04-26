using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSFX : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip bulletHit;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
     
    }

    public void PlayBulletHitSFX()
    {
        AudioSource.PlayClipAtPoint(bulletHit, Camera.main.transform.position);
    }

}
