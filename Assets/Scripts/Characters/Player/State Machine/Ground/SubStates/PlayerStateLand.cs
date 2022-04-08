using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateLand : PlayerStateGround
{
    public PlayerStateLand(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PlayerStateInAir.isInTheAir = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    public override void Tick()
    {
        base.Tick();
        if (isGrounded && player.InputHandler.HorizontalInput != 0f)
        {
            stateMachine.ChangeState(player.WalkState);
        }
        else if (isGrounded && player.InputHandler.HorizontalInput == 0f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
