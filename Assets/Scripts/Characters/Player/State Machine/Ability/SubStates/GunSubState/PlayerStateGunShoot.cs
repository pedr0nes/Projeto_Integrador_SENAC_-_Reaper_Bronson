using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateGunShoot : PlayerStateGunIdle
{
    public static event StateAction OnIdleShotFinished;

    private float abilityCurrentTime;
    

    public PlayerStateGunShoot(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.playerAnimationEvents.OnGunShot += CallShootGun;




        holdAimAnimationParameter = 1f;
        holdAimTime = 0f;
        player.SetRecoilImpulse(10f);
    }

    public override void Exit()
    {
        base.Exit();
        player.playerAnimationEvents.OnGunShot -= CallShootGun;
    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    public override void Tick()
    {
        base.Tick();
        abilityCurrentTime = Time.time;




        if (abilityCurrentTime - startTime > 0.1f)
        {
            stateMachine.ChangeState(player.GunIdleState);

            /* if (OnIdleShotFinished != null)
            {
                OnIdleShotFinished();
            }*/
        }

        ChangeGunIdleShootAnimation();

    }


    private void ChangeGunIdleShootAnimation()
    {

        player.Animator.SetFloat("gunShootBlendTree", player.InputHandler.VerticalInput);
    }

    private void CallShootGun(int aux)
    {
        player.ShootGun(aux);
    }

}
