using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Ghost State Dead class is one of character Ghost's possible states
  It Derives from the abstract base class State
  It is the class/state active when the ghost's health is below zero and it is dying. It manages the actions that happen during this time.
 */

public class GhostStateDead : State
{
    //Variable Declaration
    protected Ghost ghost;
    protected GhostData ghostData;

    //Class Constructor
    public GhostStateDead(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        //Forces constructor variable 'character' to be its derived type 'Ghost'    
        ghost = character as Ghost;

        //Forces constructor variable 'characterData' to be its derived type 'GhostData'
        ghostData = characterData as GhostData;
    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Play Ghost Die sound effect
        ghost.ghostSFX.PlayGhostDieSFX();

        //Animation Events Attachment
        ghost.ghostAnimationEvents.OnAnimationFinished += CallGhostDeath;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Animation Events Detachment
        ghost.ghostAnimationEvents.OnAnimationFinished -= CallGhostDeath;
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

    #region Ghost State Dead Methods

    //Call ghost script method that destroys this ghost's game object
    private void CallGhostDeath()
    {
        ghost.ghostAnimationEvents.OnAnimationFinished -= CallGhostDeath;
        ghost.KillGhost();
    }

    #endregion
}
