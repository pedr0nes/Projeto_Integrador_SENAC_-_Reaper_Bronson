using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimationEvents : MonoBehaviour
{
    [SerializeField] private Bat bat;
    [SerializeField] private BatData batData;

    public delegate void AnimationEvent();
    public event AnimationEvent OnAnimationFinished;
    public event AnimationEvent OnBatAttacked;


    public void AnimationFinished()
    {
        if (OnAnimationFinished != null)
        {
            OnAnimationFinished();
        }
    }

    public void BatAttacked()
    {
        if (OnBatAttacked != null)
        {
            OnBatAttacked();
        }
    }

}
