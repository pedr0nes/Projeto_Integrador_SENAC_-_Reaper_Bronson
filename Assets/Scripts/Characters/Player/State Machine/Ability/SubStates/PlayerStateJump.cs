using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : PlayerStateAbility
{
    //protected Player player;
    //protected PlayerData playerData;

    public static event StateAction OnJumpFinished;

    private float abilityCurrentTime;
    private bool isGrounded;


    public PlayerStateJump(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        //player = (Player)character;
        //playerData = (PlayerData)characterData;
        //characterData = ScriptableObject.CreateInstance<PlayerData>();
        //playerData = characterData as PlayerData;

    }

    public override void Enter()
    {
        
        base.Enter();
        PlayerStateInAir.isInTheAir = true;
        player.playerSFX.PlayJumpSFX();

        player.playerInputHandler.OnJumpSustainAction += JumpHigher;
        player.playerInputHandler.OnFireAction += TransitionToMelee;
        player.playerInputHandler.OnGunOn += TransitionToShoot;

        player.playerAnimationEvents.OnAnimationFinished += TransitionToInAir;


        player.SetVerticalVelocity(playerData.jumpVelocity);
    }

    public override void Exit()
    {
        base.Exit();
        player.playerInputHandler.OnJumpSustainAction -= JumpHigher;
        player.playerInputHandler.OnFireAction -= TransitionToMelee;
        player.playerInputHandler.OnGunOn -= TransitionToShoot;

        player.playerAnimationEvents.OnAnimationFinished -= TransitionToInAir;
    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    public override void Tick()
    {
        base.Tick();

        player.CheckIfShouldFlip(Mathf.RoundToInt(player.InputHandler.HorizontalInput));
        player.SetHorizontalVelocity(playerData.airMovementVelocity * player.InputHandler.HorizontalInput);


        abilityCurrentTime = Time.time;

        if (abilityCurrentTime - startTime > 0.1f)
        {
            isGrounded = player.CheckIfGrounded();
            if (isGrounded && player.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.LandState);
            }
        }
    }



    private void JumpHigher()
    {
        if(abilityCurrentTime - startTime < playerData.jumpSustainTime)
        player.SetVerticalVelocity(playerData.jumpVelocity);
    }

    private void TransitionToInAir()
    {
        stateMachine.ChangeState(player.InAirState);
    }

    private void TransitionToMelee()
    {
        stateMachine.ChangeState(player.MeleeAttackState);
    }

    private void TransitionToShoot()
    {
        stateMachine.ChangeState(player.GunIdleState);
    }


}
