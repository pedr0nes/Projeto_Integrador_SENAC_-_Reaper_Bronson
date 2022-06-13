using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Vampire State Walk class is one of Vampire's State Machine possible states.
  It Derives from the abstract base class State
  It is the class/state active when the vampire is touching ground and moving. It manages the actions that happen during this time.
 */

public class VampireStateWalk : State
{
    //Variable Declaration
    protected Vampire vampire;
    protected VampireData vampireData;
        
    //Class Constructor
    public VampireStateWalk(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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

        //Call Vamipre script coroutine that manages the time this vampire will be following the Player
        vampire.CallChasingPlayerCoroutine();

        //Animation events attachment
        vampire.vampireEventManager.OnPlayerFound += TransitionToAttack;
        vampire.vampireEventManager.OnChasingEnded += TransitionToIdle;
        vampire.vampireEventManager.OnVampireDead += TransitionToDead;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Animation events detachment
        vampire.vampireEventManager.OnPlayerFound -= TransitionToAttack;
        vampire.vampireEventManager.OnChasingEnded -= TransitionToIdle;
        vampire.vampireEventManager.OnVampireDead -= TransitionToDead;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        //Calls method that makes the Vampire follow the Player object
        vampire.FollowPlayer();

        //Calls method that makes the Vampire sprite be always looking towards the Player object
        vampire.LookAtPlayer();
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {

    }

    #endregion

    #region Vampire State Walk Methods

    //Changes current state to Vampire State Attack
    private void TransitionToAttack()
    {
        stateMachine.ChangeState(vampire.AttackState);
    }

    //Changes current state to Vampire State Idle
    private void TransitionToIdle()
    {
        stateMachine.ChangeState(vampire.IdleState);
    }

    //Changes current state to Vampire State Dead
    private void TransitionToDead()
    {
        stateMachine.ChangeState(vampire.DeadState);
    }

    #endregion
}
