using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHurt : PlayerStateAbility
{
    public PlayerStateHurt(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.playerSFX.PlayTakeDamageSFX();

        player.playerAnimationEvents.OnAnimationFinished += TransitionToGroundOrAir;

        player.SetHorizontalVelocity(0f);
        player.IsImmune = true;
    }



    public override void Exit()
    {
        base.Exit();
        player.playerAnimationEvents.OnAnimationFinished -= TransitionToGroundOrAir;

        player.IsImmune = false;
    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    public override void Tick()
    {
        base.Tick();
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
