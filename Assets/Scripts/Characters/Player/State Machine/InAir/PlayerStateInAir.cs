using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateInAir : State
{
    protected Player player;
    protected PlayerData playerData;

    private bool isGrounded;
    public static bool isInTheAir = false;



    public PlayerStateInAir(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        player = (Player)character;
        //characterData = ScriptableObject.CreateInstance<PlayerData>();
        playerData = characterData as PlayerData;
    }

    public override void Enter()
    {
        base.Enter();
        isInTheAir = true;
        player.playerInputHandler.OnFireAction += TransitionToMelee;
        player.playerInputHandler.OnGunOn += TransitionToGunMode;

        player.playerEventManager.OnPlayerHurt += TransitionToHurt;
        player.playerEventManager.OnPlayerDead += TransitionToDead;
        player.playerEventManager.OnPlayerWin += TransitionToWin;
    }

    public override void Exit()
    {
        base.Exit();
        player.playerInputHandler.OnFireAction -= TransitionToMelee;
        player.playerInputHandler.OnGunOn -= TransitionToGunMode;

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
        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else
        {
            player.CheckIfShouldFlip(Mathf.RoundToInt(player.InputHandler.HorizontalInput));
            player.SetHorizontalVelocity(playerData.airMovementVelocity * player.InputHandler.HorizontalInput);
        }


    }

    private void TransitionToWin()
    {
        stateMachine.ChangeState(player.WinState);
    }
    private void TransitionToMelee()
    {
        stateMachine.ChangeState(player.MeleeAttackState);
    }

    private void TransitionToGunMode()
    {

        stateMachine.ChangeState(player.GunIdleState);
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
