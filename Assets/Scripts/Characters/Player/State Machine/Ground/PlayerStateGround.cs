using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateGround : State
{
    
    protected Player player;
    protected PlayerData playerData;
    protected bool isGrounded;

    public static event StateAction OnChangeToAir;


    public PlayerStateGround(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        player = (Player)character;
        //characterData = ScriptableObject.CreateInstance<PlayerData>();
        playerData = characterData as PlayerData;
    }

    public override void Enter()
    {
        base.Enter();
        player.playerInputHandler.OnJumpAction += TransitionToJump;
        player.playerInputHandler.OnFireAction += TransitionToMeleeAttack;
        player.playerInputHandler.OnGunOn += TransitionToGunMode;
        OnChangeToAir += TransitionToInAir;

        player.playerEventManager.OnPlayerHurt += TransitionToHurt;
        player.playerEventManager.OnPlayerDead += TransitionToDead;
        player.playerEventManager.OnPlayerWin += TransitionToWin;
    }

    public override void Exit()
    {
        base.Exit();
        player.playerInputHandler.OnJumpAction -= TransitionToJump;
        player.playerInputHandler.OnFireAction -= TransitionToMeleeAttack;
        player.playerInputHandler.OnGunOn -= TransitionToGunMode;
        OnChangeToAir -= TransitionToInAir;

        player.playerEventManager.OnPlayerHurt -= TransitionToHurt;
        player.playerEventManager.OnPlayerDead -= TransitionToDead;
        player.playerEventManager.OnPlayerWin -= TransitionToWin;
    }

    public override void PhysicsTick()
    {
        
    }

    public override void Tick()
    {
        isGrounded = player.CheckIfGrounded();
        if (!isGrounded)
        {
            if (OnChangeToAir != null)
            {
                OnChangeToAir();
            }
        }
        


        //if (player.InputHandler.IsJumping && isGrounded)
        //{
        //    //Debug.Log("jump button pressed at state " + stateMachine.CurrentState);
        //    player.InputHandler.ResetJumpCondition();
        //    stateMachine.ChangeState(player.JumpState);
        //}
    }


    private void TransitionToWin()
    {
        stateMachine.ChangeState(player.WinState);
    }

    private void TransitionToJump()
    {
        stateMachine.ChangeState(player.JumpState);
    }

    private void TransitionToMeleeAttack()
    {
        stateMachine.ChangeState(player.MeleeAttackState);
    }

    private void TransitionToGunMode()
    {
        if(player.InputHandler.HorizontalInput == 0f)
        {
            stateMachine.ChangeState(player.GunIdleState);
        }
        else if (player.InputHandler.HorizontalInput != 0f)
        {
            stateMachine.ChangeState(player.GunWalkState);
        }
    }


    private void TransitionToInAir()
    {
        stateMachine.ChangeState(player.InAirState);
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
