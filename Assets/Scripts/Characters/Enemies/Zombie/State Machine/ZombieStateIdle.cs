using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Zombie State Idle class is one of Zombie's possible states
  It Derives from the abstract base class State
  It is the class/state active when the zombie is idle. It manages the actions that happen during this time
 */

public class ZombieStateIdle : State
{
    //Variable Declaration
    protected Zombie zombie;
    protected ZombieData zombieData;
    private float stateCurrentTime;

    //Class Constructor
    public ZombieStateIdle(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        //Forces constructor variable 'character' to be its derived type 'zombie'    
        zombie = character as Zombie;

        //Forces constructor variable 'characterData' to be its derived type 'ZombieData'
        zombieData = characterData as ZombieData;
    }

    #region State Methods
    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Resets state current time variable
        stateCurrentTime = 0;

        //Forces zombie horizontal velocity to be 0(zero) when entering this state. Called to ensure any inertia movement from other states will be cancelled.
        zombie.SetHorizontalVelocity(0f);

        //Event Manager events attachment
        zombie.zombieEventManager.OnPlayerFound += TransitionToAttack;
        zombie.zombieEventManager.OnZombieDead += TransitionToDead;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Event Manager events detachment
        zombie.zombieEventManager.OnPlayerFound -= TransitionToAttack;
        zombie.zombieEventManager.OnZombieDead -= TransitionToDead;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        //Method Calls
        TimeTransitionToWalk();
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {

    }

    #endregion

    #region Zombie State Idle Methods

    //Changes current state to Zombie State Walk after a while
    private void TimeTransitionToWalk()
    {
        stateCurrentTime += Time.deltaTime;             //Adds elapsed time to the state current time variable

        if (stateCurrentTime > Random.Range(zombieData.minIdleTime, zombieData.maxIdleTime))
        {
            stateMachine.ChangeState(zombie.WalkState);
        }
    }

    //Changes current state to Zombie State Attack
    private void TransitionToAttack()
    {
        stateMachine.ChangeState(zombie.AttackState);
    }

    //Changes current state to Zombie State Dead
    private void TransitionToDead()
    {
        stateMachine.ChangeState(zombie.DeadState);
    }

    #endregion

}
