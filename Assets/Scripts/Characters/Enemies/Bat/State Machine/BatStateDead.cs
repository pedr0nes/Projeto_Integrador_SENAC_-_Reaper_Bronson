using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Bat State Dead class is one of Bats' possible states
  It Derives from the abstract base class State
  It is the class/state active when the bat's health is below 0 and it needs to die. It manages the actions that happen during this time.
 */

public class BatStateDead : State
{
    //Variable Declaration
    protected Bat bat;
    protected BatData batData;

    //Class Constructor
    public BatStateDead(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        //Forces constructor variable 'character' to be its derived type 'Bat'    
        bat = character as Bat;

        //Forces constructor variable 'characterData' to be its derived type 'BatData'
        batData = characterData as BatData;
    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Play bat die sound effect
        bat.batSFX.PlayBatDieSFX();

        //Set this bat's rigidbody to be non-kinematic (dynamic), so the game object can fall for gravity physics
        bat.Rigidbody2D.isKinematic = false;

        //Animation events attachment
        bat.batAnimationEvents.OnAnimationFinished += CallBatDeath;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Animation events detachment
        bat.batAnimationEvents.OnAnimationFinished -= CallBatDeath;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {

    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {

    }

    #endregion

    #region Bat State Dead Methods

    //Calls bat script method that destroys this bat game object
    private void CallBatDeath()
    {
        bat.KillBat();
    }

    #endregion
}
