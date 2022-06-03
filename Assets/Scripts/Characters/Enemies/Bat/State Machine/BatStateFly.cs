using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Bat State Fly class is one of Bats' possible states
  It Derives from the abstract base class State
  It is the class/state active when the bat is in the flying mode that prepares for attack mode. It manages the actions that happen during this time.
 */

public class BatStateFly : State
{
    //Variable Declaration
    protected Bat bat;
    protected BatData batData;
    private float stateCurrentTime;

    //Class Constructor
    public BatStateFly(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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

        //Resets state current time variable
        stateCurrentTime = 0;

        //Event Manager events attachment
        bat.batEventManager.OnPlayerGone += TransitionToIdle;
        bat.batEventManager.OnBatDead += TransitionToDead;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Event Manager events detachment
        bat.batEventManager.OnPlayerGone -= TransitionToIdle;
        bat.batEventManager.OnBatDead -= TransitionToDead;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        //Method Calls
        TimeTransitionToAttack();
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {

    }

    #endregion

    #region Bat State Fly Methods

    //Changes current state to Bat State Attack after a while
    private void TimeTransitionToAttack()
    {
        stateCurrentTime += Time.deltaTime;                 //Adds elapsed time to the state current time variable

        if (stateCurrentTime > batData.timeBetweenAttacks)
        {
            stateMachine.ChangeState(bat.AttackState);
        }
    }

    //Changes current state to Bat State Idle
    private void TransitionToIdle()
    {
        stateMachine.ChangeState(bat.IdleState);
    }

    //Changes current state to Bat State Dead
    private void TransitionToDead()
    {
        stateMachine.ChangeState(bat.DeadState);
    }

    #endregion
}
