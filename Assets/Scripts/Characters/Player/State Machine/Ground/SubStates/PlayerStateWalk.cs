using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State Walk class is one of Player's possible states while touching ground
  It Derives from the PlayerStateGround class
  It is the class/state active when player is touching ground and moving. It manages the actions that happen during this time
 */


public class PlayerStateWalk : PlayerStateGround
{
    //Class constructor
    public PlayerStateWalk(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {

    }

    #region State Methods
    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Input Handler events attachment
        player.playerInputHandler.OnStopAction += TransitionToIdle;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Input Handler events detachment
        player.playerInputHandler.OnStopAction -= TransitionToIdle;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        base.Tick();

        //Flip sprite check based on horizontal input
        player.CheckIfShouldFlip(Mathf.RoundToInt(player.InputHandler.HorizontalInput));
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {
        base.PhysicsTick();

        //Activate horizontal movement
        player.SetHorizontalVelocity(playerData.movementVelocity * player.InputHandler.HorizontalInput);
    }

    #endregion

    #region Player State Walk Methods

    //Changes current state to Player State Idle
    private void TransitionToIdle()
    {
        stateMachine.ChangeState(player.IdleState);
    }
    #endregion
}
