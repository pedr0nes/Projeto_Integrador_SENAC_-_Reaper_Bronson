using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State Ability Class is one of Player's State Machine main states
  It Derives from the abstract base class State
  It is the base class to the substates concerning player special habilities or modes.
  This class substates are:
        - Player State Gun Idle
            - Player State Gun Shoot
        - Player State Gun Walk
        - Player State Hurt
        - Player State Jump
        - Player State Melee
 */

public class PlayerStateAbility : State
{
    //Variable Declaration
    protected Player player;
    protected PlayerData playerData;

    //State class constructor
    public PlayerStateAbility(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        //Forces constructor variable 'character' to be its derived type 'player'      
        player = character as Player;

        //Forces constructor variable 'characterData' to be its derived type 'playerData'
        playerData = characterData as PlayerData;
    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Input Handler events attachment
        player.playerInputHandler.OnGunOff += TransitionToGround;

        //Event Manager events attachment
        player.playerEventManager.OnPlayerHurt += TransitionToHurt;
        player.playerEventManager.OnPlayerDead += TransitionToDead;
        player.playerEventManager.OnPlayerWin += TransitionToWin;

        //Derived Classes events attachment
        PlayerStateJump.OnJumpFinished += TransitionToAir;
        PlayerStateGunShoot.OnIdleShotFinished += TransitionToGunIdle;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();
        //Input Handler events detachment
        player.playerInputHandler.OnGunOff -= TransitionToGround;

        //Event Manager events detachment
        player.playerEventManager.OnPlayerHurt -= TransitionToHurt;
        player.playerEventManager.OnPlayerDead -= TransitionToDead;
        player.playerEventManager.OnPlayerWin -= TransitionToWin;

        //Derived Classes events attachment
        PlayerStateJump.OnJumpFinished -= TransitionToAir;
        PlayerStateGunShoot.OnIdleShotFinished -= TransitionToGunIdle;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {

      /*   #### TEST   ###### WILL DELETE LATER #####
        isGrounded = player.CheckIfGrounded();

        if(isAbilityDone)
        {
            Debug.Log("ability was done");
            if(isGrounded && player.CurrentVelocity.y == 0f)
            {
                Debug.Log("going to idle state");
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                Debug.Log("goind to air state");
                stateMachine.ChangeState(player.InAirState);
            }
        }
       */
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {

    }

    #endregion

    #region Player State Ability Methods

    //Changes current state to Player State Win
    private void TransitionToWin()
    {
        stateMachine.ChangeState(player.WinState);
    }

    //Changes current state to Player State In Air
    private void TransitionToAir()
    {
            stateMachine.ChangeState(player.InAirState);
    }

    //Changes current state to Player State Land
    private void TransitionToGround()
    {
        stateMachine.ChangeState(player.LandState);
    }

    //Changes current state to Player State Gun Idle
    private void TransitionToGunIdle()
    {
        stateMachine.ChangeState(player.GunIdleState);
    }

    //Changes current state to Player State Gun Walk. #### OBS: Delete later if not prove itself useful. Player State Gun Idle is already doing this transition ###
    private void TransitionToGunWalk()
    {
        stateMachine.ChangeState(player.GunWalkState);
    }

    //Changes current state to Player State Hurt
    private void TransitionToHurt()
    {
        stateMachine.ChangeState(player.HurtState);
    }

    //Changes current state to Player State Dead
    private void TransitionToDead()
    {
        stateMachine.ChangeState(player.DeadState);
    }

    #endregion

}
