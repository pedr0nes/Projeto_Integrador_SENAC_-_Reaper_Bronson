using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State Gun Idle class is one of Player's special ability states
  It Derives from the Player State Ability class
  It is the class/state active when player is idle and carrying the gun.
  It is also the base class for the Gun Shoot state.
 */

public class PlayerStateGunIdle : PlayerStateAbility
{
    //Variable declaration
    protected static float holdAimTime;
    protected static float holdAimAnimationParameter;

    //Class constructor
    public PlayerStateGunIdle(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {

    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Input Handler events attachment
        player.playerInputHandler.OnFireAction += TransitionToGunShoot;

        //Cancel any inertia movement in horizontal axis if player is not in the air
        if (!PlayerStateInAir.isInTheAir)
        {
            player.SetHorizontalVelocity(0f);
        }

    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Input Handler events detachment
        player.playerInputHandler.OnFireAction -= TransitionToGunShoot;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        base.Tick();

        //Flip sprite check based on horizontal input
        player.CheckIfShouldFlip(Mathf.RoundToInt(player.InputHandler.HorizontalInput));

        //Method calls
        HoldAimDelay(playerData.afterShootDelay);
        ChangeGunIdleAnimation();
        GunWalkCheck();
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }


    #endregion

    #region Player State Gun Idle Methods

    //Changes current state to Player State Gun Shoot
    private void TransitionToGunShoot()
    {
        stateMachine.ChangeState(player.GunShootState);
    }

    //Changes current state to Player State Gun Walk
    private void TransitionToGunWalk()
    {
        stateMachine.ChangeState(player.GunWalkState);
    }

    //Checks necessity to transition to Gun Walk State.     NOTE: This call does not follow the observer pattern yet. It will be changed later.
    private void GunWalkCheck()
    {
        if (player.InputHandler.HorizontalInput != 0f && holdAimAnimationParameter == 0f)
        {
            TransitionToGunWalk();
        }
    }

    //Forces player to stay in gun idle for a while after shooting
    private void HoldAimDelay(float delayTime)
    {
        if (holdAimAnimationParameter == 1f)
        {
            holdAimTime += Time.deltaTime;
            if (holdAimTime > 0.3f)
            {
                holdAimAnimationParameter = 0f;
                holdAimTime = 0f;
            }
        }
    }

    //Changes Gun Idle animation based on vertical input (looking up, down or sideways)
    private void ChangeGunIdleAnimation()
    {
        player.Animator.SetFloat("gunIdleBlendTree", player.InputHandler.VerticalInput);
    }

    #endregion
}
