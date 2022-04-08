using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerData playerData;
    
    public delegate void AnimationEvent();
    public delegate void AnimationEventInt(int aux);

    public event AnimationEvent OnAnimationFinished;
    public event AnimationEventInt OnGunShot;

    private void Awake()
    {
        player = GetComponentInParent<Player>();

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
        player.MeleeAttack();
    }

    public void CallPushback()
    {
        player.MeleeDownPushback(20f);
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
}
