using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Vampire State Dead class is one of Vampire's State Machine possible states
  It Derives from the abstract base class State
  It is the class/state active when the vampire's health is below zero and it needs to play his death animation. It manages the actions that happen during this time.
 */

public class VampireStateDead : State
{
    //Variable Declaration
    protected Vampire vampire;
    protected VampireData vampireData;
    private float abilityCurrentTime;

    //Class Constructor
    public VampireStateDead(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        //Forces constructor variable 'character' to be its derived type 'Vampire'    
        vampire = character as Vampire;

        //Forces constructor variable 'characterData' to be its derived type 'EnemyData'
        vampireData = characterData as VampireData;
    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Resets variable that keeps track of the duration of the current state
        abilityCurrentTime = 0f;

        //Calls a Vampire script coroutine that makes this vampire sprite flash
        vampire.CallFlahsDeathEffectCoroutine();

        //Play vampire die sound effect
        vampire.vampireSFX.PlayVampireDieSFX();
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        //Adds elapsed time in this state in a variable for checks
        abilityCurrentTime += Time.deltaTime;

        //When the time counter reaches 5 seconds, calls the method that destroys this vampire game object
        if (abilityCurrentTime > 5f)
        {
            CallKillVampire();
        }
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {

    }

    #endregion

    #region Vampire State Dead Methods

    //Calls the Vampire script method that destroys this vampire game object
    private void CallKillVampire()
    {
        vampire.KillVampire();
    }

    #endregion

}
