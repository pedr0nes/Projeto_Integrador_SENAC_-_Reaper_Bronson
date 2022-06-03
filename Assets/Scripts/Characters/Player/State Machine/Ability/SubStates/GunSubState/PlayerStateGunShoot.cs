using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State Gun Shoot class is one of Player's special ability states
  It Derives from the Player State Gun Idle class
  It is the class/state active when player is using his gun attack.
 */

public class PlayerStateGunShoot : PlayerStateGunIdle
{
    //Variable declaration
    private float abilityCurrentTime;

    //States' events declaration. Type: StateAction
    public static event StateAction OnIdleShotFinished;

    //Class constructor
    public PlayerStateGunShoot(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {

    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Animation Events attachment
        player.playerAnimationEvents.OnGunShot += CallShootGun;

        //Sets hold aim parameter to 1 when entering this state
        holdAimAnimationParameter = 1f;

        //Resets hold aim time when entering this state
        holdAimTime = 0f;

        //Calls player method that causes a pushback to its object everytime it shoots.      NOTE: There is space for improvement here
        player.SetRecoilImpulse(playerData.gunRecoilPushback);
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Animation Events detachment
        player.playerAnimationEvents.OnGunShot -= CallShootGun;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        base.Tick();

        //Store abillity current time for further checks
        abilityCurrentTime = Time.time;

        //Method Calls
        GunIdleCheck(0.1f);
        ChangeGunIdleShootAnimation();
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    #endregion

    #region Player State Gun Shoot Methods

    //Changes current state to Player State Gun Idle
    private void TransitionToGunIdle()
    {
        stateMachine.ChangeState(player.GunIdleState);
    }

    //Checks necessity to transition to Gun Idle State.     NOTE: This call does not follow the observer pattern yet. It will be changed later.
    private void GunIdleCheck(float stateDuration)
    {
        if (abilityCurrentTime - startTime > stateDuration)
        {
            TransitionToGunIdle();
        }
    }

    //Changes Gun Idle animation based on vertical input (looking up, down or sideways)
    private void ChangeGunIdleShootAnimation()
    {
        player.Animator.SetFloat("gunShootBlendTree", player.InputHandler.VerticalInput);
    }

    //Calls player script method that makes player shoot gun and instantiates bullet prefab in the direction it is aiming. An animation event will set the aux variable.
    private void CallShootGun(int aux)
    {
        player.ShootGun(aux);
    }

    #endregion
}
