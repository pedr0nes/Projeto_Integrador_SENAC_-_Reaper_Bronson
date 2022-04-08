using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWalk : PlayerStateGround
{
    protected float playerHorizontalInput;
    //protected Player player;
    //protected PlayerData playerData;


    public PlayerStateWalk(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        //player = (Player)character;
        //characterData = ScriptableObject.CreateInstance<PlayerData>();
        //playerData = characterData as PlayerData;

    }

    public override void Enter()
    {
        base.Enter();
        player.playerInputHandler.OnStopAction += TransitionToIdle;
    }

    public override void Exit()
    {
        base.Exit();
        player.playerInputHandler.OnStopAction -= TransitionToIdle;
    }

    public override void PhysicsTick()
    {

        base.PhysicsTick();
        player.SetHorizontalVelocity(playerData.movementVelocity * playerHorizontalInput);

    }

    public override void Tick()
    {
        base.Tick();
        playerHorizontalInput = player.InputHandler.HorizontalInput;
        player.CheckIfShouldFlip(Mathf.RoundToInt(player.InputHandler.HorizontalInput));

    }

    private void TransitionToIdle()
    {
        stateMachine.ChangeState(player.IdleState);
    }

}
