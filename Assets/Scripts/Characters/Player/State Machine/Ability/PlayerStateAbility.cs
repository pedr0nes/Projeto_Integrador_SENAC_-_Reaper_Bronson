using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAbility : State
{
    protected Player player;
    protected PlayerData playerData;

    protected bool isAbilityDone;
    
    private bool isGrounded;

    public PlayerStateAbility(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        player = (Player)character;
        //characterData = ScriptableObject.CreateInstance<PlayerData>();
        playerData = characterData as PlayerData;
    }

    public override void Enter()
    {
        
        base.Enter();
        PlayerStateJump.OnJumpFinished += TransitionToAir;
        //PlayerStateAttackMelee.OnMeleeFinished += TransitionToGround;
        PlayerStateGunShoot.OnIdleShotFinished += TransitionToGunIdle;
        //PlayerStateGunWalkShoot.OnWalkShotFinished += TransitionToGunWalk;

        player.playerInputHandler.OnGunOff += TransitionToGround;
        //PlayerAnimationEvents.OnAnimationFinished += TransitionToGround;
        //isAbilityDone = false;

        player.playerEventManager.OnPlayerHurt += TransitionToHurt;
        player.playerEventManager.OnPlayerDead += TransitionToDead;
        player.playerEventManager.OnPlayerWin += TransitionToWin;
    }

    public override void Exit()
    {
        base.Exit();
        PlayerStateJump.OnJumpFinished -= TransitionToAir;
        //PlayerStateAttackMelee.OnMeleeFinished -= TransitionToGround;
        PlayerStateGunShoot.OnIdleShotFinished -= TransitionToGunIdle;
        //PlayerStateGunWalkShoot.OnWalkShotFinished -= TransitionToGunWalk;

        player.playerInputHandler.OnGunOff -= TransitionToGround;

        player.playerEventManager.OnPlayerHurt -= TransitionToHurt;
        player.playerEventManager.OnPlayerDead -= TransitionToDead;
        player.playerEventManager.OnPlayerWin -= TransitionToWin;
        //PlayerAnimationEvents.OnAnimationFinished -= TransitionToGround;
    }

    public override void PhysicsTick()
    {
        
    }

    public override void Tick()
    {
        isGrounded = player.CheckIfGrounded();

        //if(isAbilityDone)
        //{
        //    //Debug.Log("ability was done");
        //    //if(isGrounded && player.CurrentVelocity.y == 0f)
        //    //{
        //    //    Debug.Log("going to idle state");
        //    //    stateMachine.ChangeState(player.IdleState);
        //    //}
        //    //else
        //    //{
        //    //    Debug.Log("goind to air state");
        //    //    stateMachine.ChangeState(player.InAirState);
        //    //}
        //}

    }

    private void TransitionToWin()
    {
        stateMachine.ChangeState(player.WinState);
    }
    private void TransitionToAir()
    {
            stateMachine.ChangeState(player.InAirState);
    }

    private void TransitionToGround()
    {
        stateMachine.ChangeState(player.LandState);
    }

    private void TransitionToGunIdle()
    {
        stateMachine.ChangeState(player.GunIdleState);
    }
    private void TransitionToGunWalk()
    {
        stateMachine.ChangeState(player.GunWalkState);
        
    }

    private void TransitionToHurt()
    {
        stateMachine.ChangeState(player.HurtState);
    }

    private void TransitionToDead()
    {
        stateMachine.ChangeState(player.DeadState);
    }
}
