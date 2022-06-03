using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Ghost State Fly class is one of character Ghost's possible states
  It Derives from the abstract base class State
  It is the class/state active when the ghost is flying towards the player and attacking it. It manages the actions that happen during this time.
 */

public class GhostStateFly : State
{
    //Variable Declaration
    protected Ghost ghost;
    protected GhostData ghostData;

    //Class Constructor
    public GhostStateFly(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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

        //Event Manager events Attachment
        ghost.ghostEventManager.OnGhostDead += TransitionToDead;

        //Animation Events Attachment
        ghost.ghostAnimationEvents.OnGhostAttacked += CallGhostAttack; 
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Event Manager events Detachment
        ghost.ghostEventManager.OnGhostDead -= TransitionToDead;

        //Animation Events Detachment
        ghost.ghostAnimationEvents.OnGhostAttacked -= CallGhostAttack;
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

    #region Ghost State Fly Methods

    //Changes current state to Ghost State Dead
    private void TransitionToDead()
    {
        stateMachine.ChangeState(ghost.DeadState);
    }

    //Call ghost script method that attacks the Player
    private void CallGhostAttack()
    {
        ghost.GhostAttack();
    }

    #endregion
}
