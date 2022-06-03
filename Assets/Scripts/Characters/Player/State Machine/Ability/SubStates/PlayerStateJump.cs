using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State Jump class is one of Player's special ability states
  It Derives from the Player State Ability class
  It is the class/state active when player inputs for jumping.
 */

public class PlayerStateJump : PlayerStateAbility
{
    //Variable declaration
    private float abilityCurrentTime;
    private bool isGrounded;

    //States' events declaration. Type: StateAction
    public static event StateAction OnJumpFinished;

    //Class constructor
    public PlayerStateJump(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {

    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //When entering this state, static boolean variable 'isInTheAir' receives value true
        PlayerStateInAir.isInTheAir = true;

        //Play jump sound effect
        player.playerSFX.PlayJumpSFX();

        //Input Handler events attachment
        player.playerInputHandler.OnJumpSustainAction += JumpHigher;
        player.playerInputHandler.OnFireAction += TransitionToMelee;
        player.playerInputHandler.OnGunOn += TransitionToShoot;

        //Animation events attachment
        player.playerAnimationEvents.OnAnimationFinished += TransitionToInAir;

        //Adds a vertical velocity to player game object (jump)
        player.SetVerticalVelocity(playerData.jumpVelocity);
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Input Handler events detachment
        player.playerInputHandler.OnJumpSustainAction -= JumpHigher;
        player.playerInputHandler.OnFireAction -= TransitionToMelee;
        player.playerInputHandler.OnGunOn -= TransitionToShoot;

        //Animation events detachment
        player.playerAnimationEvents.OnAnimationFinished -= TransitionToInAir;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        base.Tick();

        //Flip sprite check based on horizontal input
        player.CheckIfShouldFlip(Mathf.RoundToInt(player.InputHandler.HorizontalInput));

        //Stores this state ability current time in a float variable for checks
        abilityCurrentTime = Time.time;

        //After a brief period of time in this state, check if player is grounded again and stopped moving. If true, transitions to Land State
        //Not sure if it is the best way to do it, but it works. Might change it later.
        if (abilityCurrentTime - startTime > 0.1f)
        {
            isGrounded = player.CheckIfGrounded();
            if (isGrounded && player.CurrentVelocity.y < 0.01f)
            {
                TransitionToLand();
            }
        }
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {
        base.PhysicsTick();

        //Activate horizontal movement while jumping
        player.SetHorizontalVelocity(playerData.airMovementVelocity * player.InputHandler.HorizontalInput);
    }

    #endregion

    #region Player State Jump Methods

    //Keeps adding velocity to player jump for some time if player keeps holding jump button
    private void JumpHigher()
    {
        if(abilityCurrentTime - startTime < playerData.jumpSustainTime)
        player.SetVerticalVelocity(playerData.jumpVelocity);
    }

    //Changes current state to Player State Land
    private void TransitionToLand()
    {
        stateMachine.ChangeState(player.LandState);
    }

    //Changes current state to Player State In Air
    private void TransitionToInAir()
    {
        stateMachine.ChangeState(player.InAirState);
    }

    //Changes current state to Player State Melee
    private void TransitionToMelee()
    {
        stateMachine.ChangeState(player.MeleeAttackState);
    }

    //Changes current state to Player State Gun Idle
    private void TransitionToShoot()
    {
        stateMachine.ChangeState(player.GunIdleState);
    }

    #endregion

}
