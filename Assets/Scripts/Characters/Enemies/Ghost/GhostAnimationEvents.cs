using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimationEvents : MonoBehaviour
{
    public delegate void AnimationEvent();
    public event AnimationEvent OnAnimationFinished;
    public event AnimationEvent OnGhostAttacked;

    public void AnimationFinished()
    {
        if (OnAnimationFinished != null)
        {
            OnAnimationFinished();
        }
    }

    public void GhostAttacked()
    {
        if (OnGhostAttacked != null)
        {
            OnGhostAttacked();
        }
    }
}
