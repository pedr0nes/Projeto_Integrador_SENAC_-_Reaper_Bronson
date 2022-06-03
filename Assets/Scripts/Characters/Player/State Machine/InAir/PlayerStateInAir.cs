using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State In Air Class is one of Player's State Machine main states
  It Derives from the abstract base class State
  It becomes active everytime player game object is not touching ground and/or jump ability animation is finished
  Currently, it does not have any derived classes
 */

public class PlayerStateInAir : State
{
    //Variable Declaration
    protected Player player;
    protected PlayerData playerData;
    private bool isGrounded;
    public static bool isInTheAir = false;

    //State class constructor
    public PlayerStateInAir(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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

        //When entering this state, static boolean variable 'isInTheAir' receives value true
        isInTheAir = true;

        //Input Handler events attachment
        player.playerInputHandler.OnFireAction += TransitionToMelee;
        player.playerInputHandler.OnGunOn += TransitionToGunMode;

        //Event Manager events attachment
        player.playerEventManager.OnPlayerHurt += TransitionToHurt;
        player.playerEventManager.OnPlayerDead += TransitionToDead;
        player.playerEventManager.OnPlayerWin += TransitionToWin;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();
        //Input Handler events detachment
        player.playerInputHandler.OnFireAction -= TransitionToMelee;
        player.playerInputHandler.OnGunOn -= TransitionToGunMode;

        //Event Manager events detachment
        player.playerEventManager.OnPlayerHurt -= TransitionToHurt;
        player.playerEventManager.OnPlayerDead -= TransitionToDead;
        player.playerEventManager.OnPlayerWin -= TransitionToWin;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        //Calls ground check. If 'isGrounded' variable returns true and Player Object stops falling (y axis velocity is equal to zero) calls the state change to the Player Land State
        isGrounded = player.CheckIfGrounded();
        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        //If ground check returns false, checks the necessity to flip sprite left or right based on horizontal input
        //Also, it changes Player movement speed to the air movement velocity value
        else
        {
            player.CheckIfShouldFlip(Mathf.RoundToInt(player.InputHandler.HorizontalInput));
            player.SetHorizontalVelocity(playerData.airMovementVelocity * player.InputHandler.HorizontalInput);
        }
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {

    }

    #endregion

    #region Player State In Air Methods

    //Changes current state to Player State Win
    private void TransitionToWin()
    {
        stateMachine.ChangeState(player.WinState);
    }

    //Changes current state to Player State Melee
    private void TransitionToMelee()
    {
        stateMachine.ChangeState(player.MeleeAttackState);
    }

    //Changes current state to Player State Gun Idle
    private void TransitionToGunMode()
    {
        stateMachine.ChangeState(player.GunIdleState);
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
