using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State Melee class is one of Player's special ability states
  It Derives from the Player State Ability class
  It is the class/state active when player is using his melee scythe attack.
 */

public class PlayerStateMelee : PlayerStateAbility
{
    //Variable declaration
    private float abilityCurrentTime;

    //States' events declaration. Type: StateAction
    public static event StateAction OnMeleeFinished;

    //Class constructor
    public PlayerStateMelee(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {

    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Forces player horizontal velocity to be 0(zero) when entering this state. Called to ensure any inertia movement will be cancelled. Player is NOT stopped if it continues to receive movement input.
        player.SetHorizontalVelocity(0f);

        //Play melee attack sound effect
        player.playerSFX.PlayScytheAttackSFX();

        //Animation events attachment
        player.playerAnimationEvents.OnAnimationFinished += TransitionToGroundOrAir; 
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Animation events detachment
        player.playerAnimationEvents.OnAnimationFinished -= TransitionToGroundOrAir;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        base.Tick();

        //Stores this state ability current time in a float variable for checks
        abilityCurrentTime = Time.time;

        //Checks need for change in attack animation and calls the proper animation
        ChangeScytheAnimation();

        //After a brief period of time, notifies script that melee state can be exited and next state should be called. Attack animation is not cancelled though and next animation starts after it is finished.
        //Not sure if it is the best way to do it, but it works. Might change it later.
        if (abilityCurrentTime - startTime > 0.1f)
        {
            if (OnMeleeFinished != null)
            {
                OnMeleeFinished();
            }
        }
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {
        base.PhysicsTick();

        //Activate horizontal movement
        player.SetHorizontalVelocity(playerData.movementVelocity * player.InputHandler.HorizontalInput);
    }

    #endregion

    #region Player State Melee Methods

    //Changes Attack animation based on vertical input (aiming up, down or sideways)
    private void ChangeScytheAnimation()
    {
        player.Animator.SetFloat("meleeBlendTree", player.InputHandler.VerticalInput);
    }

    //Changes current state to Player State Ground or Player State In Air, depending on the value of boolean static variable 'isInTheAir'
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

    #endregion
}
