using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateGunIdle : PlayerStateAbility
{
    protected static float holdAimTime;
    protected static float holdAimAnimationParameter;


    public PlayerStateGunIdle(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //holdAimAnimationParameter = 0f;

        player.playerInputHandler.OnFireAction += TransitionToGunShoot;
        //InputHandler.OnWalkAction += TransitionToGunWalk;


        if (!PlayerStateInAir.isInTheAir)
        {
            player.SetHorizontalVelocity(0f);
        }

    }

    public override void Exit()
    {
        base.Exit();

        player.playerInputHandler.OnFireAction -= TransitionToGunShoot;
        //InputHandler.OnWalkAction -= TransitionToGunWalk;
    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    public override void Tick()
    {
        base.Tick();
        
        if(holdAimAnimationParameter == 1f)
        {
            
            holdAimTime += Time.deltaTime;
            if (holdAimTime > 2f)
            {

                holdAimAnimationParameter = 0f;
                holdAimTime = 0f;
            }
        }


        //Debug.Log("Tempo: " + holdAimTime);

        ChangeGunIdleAnimation();

        if (player.InputHandler.HorizontalInput != 0f)
        {
            //stateMachine.ChangeState(player.GunWalkState);
        }

        player.CheckIfShouldFlip(Mathf.RoundToInt(player.InputHandler.HorizontalInput));
    }



    private void TransitionToGunShoot
        ()
    {
        stateMachine.ChangeState(player.GunShootState);
    }

    private void TransitionToGunWalk()
    {
        stateMachine.ChangeState(player.GunWalkState);
    }

    private void ChangeGunIdleAnimation()
    {
        player.Animator.SetFloat("gunIdleBlendTree", player.InputHandler.VerticalInput);
        //player.Animator.SetFloat("holdAimBlendTree", holdAimAnimationParameter);
    }


}
