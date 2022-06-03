using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Player State Hurt class is one of Player's special ability states
  It Derives from the Player State Ability class
  It is the class/state active when player takes damage and plays its respective animation.
 */

public class PlayerStateHurt : PlayerStateAbility
{
    //Class constructor
    public PlayerStateHurt(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {

    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Animation events attachment
        player.playerAnimationEvents.OnAnimationFinished += TransitionToGroundOrAir;

        //Play player take damage sound effect
        player.playerSFX.PlayTakeDamageSFX();

        //Forces player horizontal velocity to be 0(zero) when entering this state. Called to ensure any inertia movement will be cancelled.
        player.SetHorizontalVelocity(0f);

        //Set player to be immune to damage
        player.IsImmune = true;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Animation events detachment
        player.playerAnimationEvents.OnAnimationFinished -= TransitionToGroundOrAir;

        //Set player to be vulnerable to damage again
        player.IsImmune = false;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        base.Tick();
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    #endregion

    #region Player State Hurt Methods

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
