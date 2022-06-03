using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State Idle class is one of Player's possible states while touching ground
  It Derives from the PlayerStateGround class
  It is the class/state active when player is touching ground and NOT moving. It manages the actions that happen during this time
 */

public class PlayerStateIdle : PlayerStateGround
{
    //Class constructor
    public PlayerStateIdle(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {

    }

    #region State Methods
    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();
        //Forces player horizontal velocity to be 0(zero) when entering this state. Called to ensure any inertia movement will be cancelled. Player state is transitioned to Walk State if it continues to receive movement input.
        player.SetHorizontalVelocity(0f);

        //Input Handler events attachment
        player.playerInputHandler.OnWalkAction += TransitionToWalk;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Input Handler events detachment
        player.playerInputHandler.OnWalkAction -= TransitionToWalk;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        base.Tick();

        //Checks need for change in idle animation and calls the proper animation
        ChangeIdleAnimation();
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    #endregion

    #region Player State Idle Methods

    //Changes Idle animation based on vertical input (looking up, down or sideways)
    private void ChangeIdleAnimation()
    {
        player.Animator.SetFloat("idleBlendTree", player.InputHandler.VerticalInput);
    }

    //Changes current state to Player State Walk
    private void TransitionToWalk()
    {
        stateMachine.ChangeState(player.WalkState);
    }

    #endregion

}
