using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Player Animation Events Class
 * Class created to manage animation events related to the Player character in a Observer Pattern approach
 * All event names are self explanatory
 */

public class PlayerAnimationEvents : MonoBehaviour
{
    //Script References
    [SerializeField] Player player;
    [SerializeField] PlayerData playerData;
    
    //Event declaration
    public delegate void AnimationEvent();
    public delegate void AnimationEventInt(int aux);

    public event AnimationEvent OnAnimationFinished;
    public event AnimationEventInt OnGunShot;

    private void Awake()
    {
        //Player reference attribution
        player = GetComponentInParent<Player>();
    }

    #region Methods

    public void AnimationFinished()
    {
        if (OnAnimationFinished != null)
        {
            OnAnimationFinished();
        }
    }

    

    public void CallAttackDamage()
    {
        player.MeleeAttack();
    }

    public void CallPushback()
    {
        player.MeleeDownPushback(playerData.meleePushback);
    }

    public void GunShot(int aux)
    {
        if(OnGunShot != null)
        {
            OnGunShot(aux);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(player.meleeAttackPoint.position, playerData.meleeAttackDetectionRadius);
    }


    #endregion
}
