using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Zombie State Spawn class is one of Zombie's possible states
  It Derives from the abstract base class State
  It is the class/state active when the zombie is spawning. It manages the actions that happen during this time
 */

public class ZombieStateSpawn : State
{
    //Variable Declaration
    protected Zombie zombie;
    protected ZombieData zombieData;

    //Class Constructor
    public ZombieStateSpawn(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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

        //Play zombie spawn sound effect
        zombie.zombieSFX.PlayZombieSpawnSFX();

        //Zombie Animation events attachment
        zombie.zombieAnimationEvents.OnAnimationFinished += TransitionToIdle;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Zombie Animation events detachment
        zombie.zombieAnimationEvents.OnAnimationFinished -= TransitionToIdle;
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

    #region Zombie State Spawn Methods

    //Changes current state to Zombie State Idle
    private void TransitionToIdle()
    {
        stateMachine.ChangeState(zombie.IdleState);
    }

    #endregion


}
