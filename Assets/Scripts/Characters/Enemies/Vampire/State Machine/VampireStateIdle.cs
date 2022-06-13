using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Vampire State Idle class is one of Vampire's State Machine possible states
  It Derives from the abstract base class State
  It is the class/state active when the vampire is idle. It manages the actions that happen during this time.
 */

public class VampireStateIdle : State
{
    //Variable Declaration
    protected Vampire vampire;
    protected VampireData vampireData;

    //Class Constructor
    public VampireStateIdle(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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

        //Calls Vampire script coroutine that manages the time this vampire will be idle
        vampire.CallIdlePeriodCoroutine();

        //Event Manager events attachment
        vampire.vampireEventManager.OnIdleEnded += TransitionToDisappear;
        vampire.vampireEventManager.OnVampireDead += TransitionToDead;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Event Manager events detachment
        vampire.vampireEventManager.OnIdleEnded -= TransitionToDisappear;
        vampire.vampireEventManager.OnVampireDead -= TransitionToDead;
    }


    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        //Calls method that makes the Vampire sprite be always looking towards the Player object
        vampire.LookAtPlayer();
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {

    }
    #endregion

    #region Vampire State Idle Methods

    //Changes current state to Vampire State Disappear
    private void TransitionToDisappear()
    {
        stateMachine.ChangeState(vampire.DisappearState);
    }

    //Changes current state to Vampire State Dead
    private void TransitionToDead()
    {
        stateMachine.ChangeState(vampire.DeadState);
    }

    #endregion
}
