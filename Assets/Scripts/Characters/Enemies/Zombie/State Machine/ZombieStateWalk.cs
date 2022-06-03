using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Zombie State Walk class is one of Zombie's possible states while walking on the ground
  It Derives from the abstract base class State
  It is the class/state active when the zombie is touching ground and moving. It manages the actions that happen during this time
 */

public class ZombieStateWalk : State
{
    //Variable Declaration
    protected Zombie zombie;
    protected ZombieData zombieData;
    private float abilityCurrentTime;

    //Class Constructor
    public ZombieStateWalk(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        //Forces constructor variable 'character' to be its derived type 'zombie'    
        zombie = character as Zombie;

        //Forces constructor variable 'characterData' to be its derived type 'playerData'
        zombieData = characterData as ZombieData;
    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Event Manager events attachment
        zombie.zombieEventManager.OnObstacleFound += zombie.FlipSprite;
        zombie.zombieEventManager.OnPlayerFound += TransitionToAttack;
        zombie.zombieEventManager.OnZombieDead += TransitionToDead;
    }


    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Event Manager events detachment
        zombie.zombieEventManager.OnObstacleFound -= zombie.FlipSprite;
        zombie.zombieEventManager.OnPlayerFound -= TransitionToAttack;
        zombie.zombieEventManager.OnZombieDead -= TransitionToDead;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        //Method Calls
        TransitionToIdle();
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {
        //Enable horizontal movement
        zombie.SetHorizontalVelocity(zombieData.movementVelocity * zombie.FacingDirection);
    }

    #endregion

    #region Zombie State Walk Methods

    //Changes current state to Zombie State Idle after a while
    private void TransitionToIdle()
    {
        abilityCurrentTime = Time.time;
        if (abilityCurrentTime - startTime > Random.Range(zombieData.minWalkTime, zombieData.maxWalkTime))
        {
            stateMachine.ChangeState(zombie.IdleState);
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
