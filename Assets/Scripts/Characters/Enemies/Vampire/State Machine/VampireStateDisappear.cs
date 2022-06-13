using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Vampire State Disappear class is one of Vampire's State Machine possible states
  It Derives from the abstract base class State
  It is the class/state active when the vampire is turning invisible after being visible. It manages the actions that happen during this time.
 */

public class VampireStateDisappear : State
{
    //Variable Declaration
    protected Vampire vampire;
    protected VampireData vampireData;

    //Class Constructor
    public VampireStateDisappear(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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

        //Animation events attachment
        vampire.vampireAnimationEvents.OnAnimationFinished += TransitionToInvisible;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Animation events detachment
        vampire.vampireAnimationEvents.OnAnimationFinished -= TransitionToInvisible;
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

    #region Vampire State Disappear Methods

    //Changes current state to Vampire State Invisible
    private void TransitionToInvisible()
    {
        stateMachine.ChangeState(vampire.InvisibleState);
    }
    #endregion
}
