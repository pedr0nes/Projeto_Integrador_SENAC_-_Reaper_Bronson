using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Bat Animation Events Class
 * Class created to manage animation events related to the Bat character in a Observer Pattern approach
 * All event names are self explanatory
 */

public class BatAnimationEvents : MonoBehaviour
{
    //Script References
    [SerializeField] private Bat bat;
    [SerializeField] private BatData batData;

    //Event Declaration
    public delegate void AnimationEvent();
    public event AnimationEvent OnAnimationFinished;
    public event AnimationEvent OnBatAttacked;

    #region Class Specific Methods

    //Notifies subscribed scripts when the current animation is finished, if needed
    public void AnimationFinished()
    {
        if (OnAnimationFinished != null)
        {
            OnAnimationFinished();
        }
    }

    //Notifies subscribed scripts when Bat character attacked
    public void BatAttacked()
    {
        if (OnBatAttacked != null)
        {
            OnBatAttacked();
        }
    }

    #endregion
}
