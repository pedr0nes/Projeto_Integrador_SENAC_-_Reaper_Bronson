using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State Land class is one of Player's possible states while touching ground
  It Derives from the Player State Ground class
  It is the class/state active when player touches ground after a period in air.
 */

public class PlayerStateLand : PlayerStateGround
{
    //Class constructor
    public PlayerStateLand(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
    }

    #region State Methods
    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Set isInTheAir static variable to false when entering this state
        PlayerStateInAir.isInTheAir = false;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        base.Tick();

        //Checks if player is on ground and moving. Calls Walk State if true.
        if (isGrounded && player.InputHandler.HorizontalInput != 0f)
        {
            TransitionToWalk();
        }
        //Checks if player is on ground and NOT moving. Calls Idle State if true.
        else if (isGrounded && player.InputHandler.HorizontalInput == 0f)
        {
            TransitionToIdle();
        }
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    #endregion

    #region Player State Land Methods

    //Changes current state to Player State Walk
    private void TransitionToWalk()
    {
        stateMachine.ChangeState(player.WalkState);
    }

    //Changes current state to Player State Idle
    private void TransitionToIdle()
    {
        stateMachine.ChangeState(player.IdleState);
    }
    #endregion
}
