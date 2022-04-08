using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateGunWalk : PlayerStateAbility
{
    public PlayerStateGunWalk(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.playerInputHandler.OnFireAction += TransitionToIdleShoot;
    }

    public override void Exit()
    {
        base.Exit();
        player.playerInputHandler.OnFireAction -= TransitionToIdleShoot;
    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
        player.SetHorizontalVelocity(playerData.movementVelocity * player.InputHandler.HorizontalInput);

    }

    public override void Tick()
    {
        base.Tick();

        if (player.InputHandler.HorizontalInput == 0f)
        {
            stateMachine.ChangeState(player.GunIdleState);
        }

        player.CheckIfShouldFlip(Mathf.RoundToInt(player.InputHandler.HorizontalInput));
    }



    private void TransitionToIdleShoot()
    {
        
        stateMachine.ChangeState(player.GunShootState);

    }

}
