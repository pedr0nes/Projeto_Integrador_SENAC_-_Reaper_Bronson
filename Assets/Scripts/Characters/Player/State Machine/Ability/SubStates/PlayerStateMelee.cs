using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMelee : PlayerStateAbility
{
    public static event StateAction OnMeleeFinished;

    private float abilityCurrentTime;

    public PlayerStateMelee(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {


    }

    public override void Enter()
    {
        
        base.Enter();
        player.SetHorizontalVelocity(0f);
        player.playerSFX.PlayScytheAttackSFX();

        player.playerAnimationEvents.OnAnimationFinished += TransitionToGroundOrAir; 
        
    }

    public override void Exit()
    {
        base.Exit();
        player.playerAnimationEvents.OnAnimationFinished -= TransitionToGroundOrAir;
    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
        player.SetHorizontalVelocity(playerData.movementVelocity * player.InputHandler.HorizontalInput);
    }

    public override void Tick()
    {
        base.Tick();
        abilityCurrentTime = Time.time;

        ChangeScytheAnimation();


        if (abilityCurrentTime - startTime > 0.1f)
        {
            if (OnMeleeFinished != null)
            {
                OnMeleeFinished();
            }
        }
    }

    private void ChangeScytheAnimation()
    {
        player.Animator.SetFloat("meleeBlendTree", player.InputHandler.VerticalInput);
    }

    private void TransitionToGroundOrAir()
    {
        if(PlayerStateInAir.isInTheAir)
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else
        {
            stateMachine.ChangeState(player.LandState);
        }
        

    }
}
