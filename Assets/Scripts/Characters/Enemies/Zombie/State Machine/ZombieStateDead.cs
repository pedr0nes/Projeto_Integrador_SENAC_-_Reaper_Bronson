using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Zombie State Dead class is one of Zombie's possible states
  It Derives from the abstract base class State
  It is the class/state active when the zombie is dying and its object will be destroyed afther the respective animation. It manages the actions that happen during this time
 */

public class ZombieStateDead : State
{
    //Variable Declaration
    protected Zombie zombie;
    protected ZombieData zombieData;

    //Class Constructor
    public ZombieStateDead(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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

        //Play zombie die sound effect
        zombie.zombieSFX.PlayZombieDieSFX();

        //Forces zombie horizontal velocity to be 0(zero) when entering this state. Called to ensure any inertia movement from other states will be cancelled.
        zombie.SetHorizontalVelocity(0f);

        //Zombie Animation events attachment
        zombie.zombieAnimationEvents.OnAnimationFinished += CallZombieDeath;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Zombie Animation events attachment
        zombie.zombieAnimationEvents.OnAnimationFinished -= CallZombieDeath;
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

    #region Zombie State Dead Methods

    //Calls zombie script method that destroys this zombie game object
    private void CallZombieDeath()
    {
        zombie.KillZombie();
    }

    #endregion

}
