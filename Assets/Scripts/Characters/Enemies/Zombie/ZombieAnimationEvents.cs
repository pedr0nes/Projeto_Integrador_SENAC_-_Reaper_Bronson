using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Zombie Animation Events Class
 * Class created to manage animation events related to the Zombie character in a Observer Pattern approach
 * All event names are self explanatory
 */

public class ZombieAnimationEvents : MonoBehaviour
{
    //Script References
    [SerializeField] private Zombie zombie;
    [SerializeField] private ZombieData zombieData;

    //Event declaration
    public delegate void AnimationEvent();
    public event AnimationEvent OnAnimationFinished;


    #region Unity Methods
    private void Awake()
    {
        zombie = GetComponentInParent<Zombie>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(zombie.attackDetectionPoint.position, zombieData.attackDetectionRadius);
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

    //Notifies subscribed scripts when the zombie attack animation reached the frame that should cause damage
    public void CallAttackDamage()
    {
        zombie.MeleeAttack();
    }

    #endregion
}
