using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationEvents : MonoBehaviour
{
    [SerializeField] private Zombie zombie;
    [SerializeField] private ZombieData zombieData;
    
    public delegate void AnimationEvent();
    public event AnimationEvent OnAnimationFinished;

    private void Awake()
    {
        zombie = GetComponentInParent<Zombie>();
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
        zombie.MeleeAttack();
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(zombie.attackDetectionPoint.position, zombieData.attackDetectionRadius);
    }
}
