using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State Gun Walk class is one of Player's special ability states
  It Derives from the Player State Ability class
  It is the class/state active when player is walking with the gun.
 */

public class PlayerStateGunWalk : PlayerStateAbility
{
    //Class constructor
    public PlayerStateGunWalk(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {

    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Input Handler events attachment
        player.playerInputHandler.OnFireAction += TransitionToGunShoot;
        player.playerInputHandler.OnStopAction += TransitionToGunIdle;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Input Handler events detachment
        player.playerInputHandler.OnFireAction -= TransitionToGunShoot;
        player.playerInputHandler.OnStopAction -= TransitionToGunIdle;
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

    #region Player State Gun Walk Methods

    //Changes current state to Player State Gun Shoot
    private void TransitionToGunShoot()
    {
        stateMachine.ChangeState(player.GunShootState);
    }

    //Changes current state to Player State Gun Idle
    private void TransitionToGunIdle()
    {
        stateMachine.ChangeState(player.GunIdleState);
    }

    #endregion
}
