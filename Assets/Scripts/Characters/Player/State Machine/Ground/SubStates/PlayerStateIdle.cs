using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : PlayerStateGround
{
    //protected Player player;
    //protected PlayerData playerData;

    public PlayerStateIdle(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {

        //player = (Player)character;
        //characterData = ScriptableObject.CreateInstance<PlayerData>();
        //playerData = characterData as PlayerData;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetHorizontalVelocity(0f);
        player.playerInputHandler.OnWalkAction += TransitionToWalk;

    }

    public override void Exit()
    {
        base.Exit();
        player.playerInputHandler.OnWalkAction -= TransitionToWalk;
    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    public override void Tick()
    {
        base.Tick();


        ChangeIdleAnimation();


    }


    private void ChangeIdleAnimation()
    {
        player.Animator.SetFloat("idleBlendTree", player.InputHandler.VerticalInput);
    }

    private void TransitionToWalk()
    {
        stateMachine.ChangeState(player.WalkState);
    }




}
