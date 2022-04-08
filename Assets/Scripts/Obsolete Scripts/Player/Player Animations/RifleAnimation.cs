using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RifleAnimation : MonoBehaviour
{
    [SerializeField] Animator rifleAnimator;
    [SerializeField] Animator rifleFireAnimator;


    public void OnShoot()
    {
        rifleAnimator.SetTrigger("Shoot");
        rifleFireAnimator.SetTrigger("Shoot");
    }


}
