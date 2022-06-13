using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Vampire State Attack class is one of Vampire's State Machine possible states
  It Derives from the abstract base class State
  It is the class/state active when the vampire is attacking the player object. It manages the actions that happen during this time.
 */

public class VampireStateAttack : State
{
    //Variable Declaration
    protected Vampire vampire;
    protected VampireData vampireData;

    //Class Constructor
    public VampireStateAttack(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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
        //Play vampire attack sound effect
        vampire.vampireSFX.PlayVampireAttackSFX();

        //Animation Events attachment
        vampire.vampireAnimationEvents.OnAnimationFinished += TransitionToWalk;

        //Event Manager events attachment
        vampire.vampireEventManager.OnVampireDead += TransitionToDead;
    }


    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Animation Events detachment
        vampire.vampireAnimationEvents.OnAnimationFinished -= TransitionToWalk;

        //Event Manager events detachment
        vampire.vampireEventManager.OnVampireDead -= TransitionToDead;
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

    #region Vampire State Attack Methods

    //Changes current state to Vampire State Walk
    private void TransitionToWalk()
    {
        stateMachine.ChangeState(vampire.WalkState);
    }

    //Changes current state to Vampire State Dead
    private void TransitionToDead()
    {
        stateMachine.ChangeState(vampire.DeadState);
    }

    #endregion
}
