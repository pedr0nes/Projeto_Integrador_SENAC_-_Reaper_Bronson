using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireAnimationEvents : MonoBehaviour
{
    [SerializeField] private Vampire vampire;
    [SerializeField] private VampireData vampireData;



    public delegate void AnimationEvent();
    public event AnimationEvent OnAnimationFinished;



    private void Awake()
    {
        //vampire = GetComponentInParent<Vampire>();
    }

    public void AnimationFinished()
    {
        if (OnAnimationFinished != null)
        {
            OnAnimationFinished();
        }
    }

    public void CallAttackDamage()
    {
        vampire.BatAttack();
    }




}
