using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Vampire State Invisible class is one of Vampire's State Machine possible states
  It Derives from the abstract base class State
  It is the class/state active when the vampire is invisible. It manages the actions that happen during this time.
 */

public class VampireStateInvisible : State
{
    //Variable Declaration
    protected Vampire vampire;
    protected VampireData vampireData;

    //Class Constructor
    public VampireStateInvisible(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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

        //Calls Vampire script coroutine that manages the time this vampire will be invisible
        vampire.CallInvisiblePeriodCoroutine();

        //Disable sprite renderer so it becomes invisible
        vampire.GetComponentInChildren<SpriteRenderer>().enabled = false;

        //Changes vampire 'y' axis position to one way above the player, so it cannot interact with it
        vampire.transform.position = vampire.transform.position + new Vector3(0, 50f, 0);

        //Event Manager events attachment
        vampire.vampireEventManager.OnInvisibleEnded += TransitionToAppear;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Brings back the vampire to ground level position before exiting the invisible state
        vampire.transform.position = vampire.transform.position - new Vector3(0, 50f, 0);

        //Event Manager events attachment
        vampire.vampireEventManager.OnInvisibleEnded -= TransitionToAppear;
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

    #region Vampire State Invisible Methods

    //Changes current state to Vampire State Appear
    public void TransitionToAppear()
    {
        stateMachine.ChangeState(vampire.AppearState);
    }

    #endregion

}
