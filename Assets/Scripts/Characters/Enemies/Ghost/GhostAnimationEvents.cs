using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ghost Animation Events Class
 * Class created to manage animation events related to the Ghost character in a Observer Pattern approach
 * All event names are self explanatory
 */

public class GhostAnimationEvents : MonoBehaviour
{
    //Event Declaration
    public delegate void AnimationEvent();
    public event AnimationEvent OnAnimationFinished;
    public event AnimationEvent OnGhostAttacked;

    #region Class Specific Methods

    //Notifies subscribed scripts when the current animation is finished, if needed
    public void AnimationFinished()
    {
        if (OnAnimationFinished != null)
        {
            OnAnimationFinished();
        }
    }

    //Notifies subscribed scripts when this Ghost character attacked
    public void GhostAttacked()
    {
        if (OnGhostAttacked != null)
        {
            OnGhostAttacked();
        }
    }

    #endregion
}
