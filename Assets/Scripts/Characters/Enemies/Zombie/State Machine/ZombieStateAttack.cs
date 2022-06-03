using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Zombie State Attack class is one of Zombie's possible states
  It Derives from the abstract base class State
  It is the class/state active when the zombie is attacking the Player. It manages the actions that happen during this time
 */

public class ZombieStateAttack : State
{
    //Variable Declaration
    protected Zombie zombie;
    protected ZombieData zombieData;

    //Class Constructor
    public ZombieStateAttack(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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

        //Play zombie attack sound effect
        zombie.zombieSFX.PlayZombieAttackSFX();

        //Forces zombie horizontal velocity to be 0(zero) when entering this state. Called to ensure any inertia movement from other states will be cancelled.
        zombie.SetHorizontalVelocity(0f);

        //Event Manager events attachment
        zombie.zombieEventManager.OnPlayerGone += TransitionToIdle;
        zombie.zombieEventManager.OnZombieDead += TransitionToDead;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Event Manager events detachment
        zombie.zombieEventManager.OnPlayerGone -= TransitionToIdle;
        zombie.zombieEventManager.OnZombieDead -= TransitionToDead;
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

    #region Zombie State Attack Methods

    //Changes current state to Zombie State Idle
    private void TransitionToIdle()
    {
        stateMachine.ChangeState(zombie.IdleState);
    }

    //Changes current state to Zombie State Dead
    private void TransitionToDead()
    {
        stateMachine.ChangeState(zombie.DeadState);
    }

    #endregion

}
