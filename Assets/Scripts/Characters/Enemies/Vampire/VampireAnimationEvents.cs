using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Vampire Animation Events Class
 * Class created to manage animation events related to the Zombie character in a Observer Pattern approach
 * All event names are self explanatory
 */

public class VampireAnimationEvents : MonoBehaviour
{
    //Script References
    [SerializeField] private Vampire vampire;
    [SerializeField] private VampireData vampireData;

    //Event declaration
    public delegate void AnimationEvent();
    public event AnimationEvent OnAnimationFinished;

    #region Unity Methods

    private void Awake()
    {
        vampire = GetComponentInParent<Vampire>();
    }

    #endregion

    #region Class Specific Methods

    //Notifies subscribed scripts when the current animation is finished, if needed
    public void AnimationFinished()
    {
        if (OnAnimationFinished != null)
        {
            OnAnimationFinished();
        }
    }

    //Notifies subscribed scripts when the vampire attack animation reached the frame that should cause damage
    public void CallAttackDamage()
    {
        vampire.BatAttack();
    }

    #endregion

}
