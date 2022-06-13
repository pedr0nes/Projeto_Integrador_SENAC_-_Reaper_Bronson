using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Vampire State Appear class is one of Vampire's State Machine possible states
  It Derives from the abstract base class State
  It is the class/state active when the vampire is appearing after being invisible. It manages the actions that happen during this time.
 */

public class VampireStateAppear : State
{
    //Variable Declaration
    protected Vampire vampire;
    protected VampireData vampireData;

    //Class Constructor
    public VampireStateAppear(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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

        //Play Vampire spawn sound effect
        vampire.vampireSFX.PlayVampireSpawnSFX();

        //Activate Sprite Renderer
        SpriteRendererActivationCheck();

        //Animation events attachment
        vampire.vampireAnimationEvents.OnAnimationFinished += TransitionToWalk;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Animation events detachment
        vampire.vampireAnimationEvents.OnAnimationFinished -= TransitionToWalk;
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

    #region Vampire State Appear Methods

    //Changes current state to Vampire State Walk
    private void TransitionToWalk()
    {
        stateMachine.ChangeState(vampire.WalkState);
    }

    //Enables sprite renderer if it is not enabled yet
    private void SpriteRendererActivationCheck()
    {
        if (!vampire.GetComponentInChildren<SpriteRenderer>().enabled)
        {
            vampire.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }

    #endregion
}
