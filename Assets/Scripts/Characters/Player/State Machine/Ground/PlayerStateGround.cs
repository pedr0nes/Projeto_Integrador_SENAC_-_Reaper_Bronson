using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State Ground Class is one of Player's State Machine main states
  It Derives from the abstract base class State
  It is the base class to the substates concerning ground movement that are not related to player special habilities.
  This class substates are:
        - Player State Idle
        - Player State Land
        - Player State Walk
 */
public class PlayerStateGround : State
{
    //Variable Declaration
    protected Player player;
    protected PlayerData playerData;
    protected bool isGrounded;

    //States' events declaration. Type: StateAction
    public static event StateAction OnChangeToAir;

    //State class constructor
    public PlayerStateGround(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        //Forces constructor variable 'character' to be its derived type 'player'      
        player = character as Player;

        //Forces constructor variable 'characterData' to be its derived type 'playerData'
        playerData = characterData as PlayerData;

        /*Could do it via casting: player = (Player)character;
        *Casting in playerData seems not to work sometimes. Maybe because it is a scriptable object. Will try to understand later.
        *characterData = ScriptableObject.CreateInstance<PlayerData>();
        *playerData = (PlayerData)characterData;
        */
    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Input Handler events attachment
        player.playerInputHandler.OnJumpAction += TransitionToJump;
        player.playerInputHandler.OnFireAction += TransitionToMeleeAttack;
        player.playerInputHandler.OnGunOn += TransitionToGunMode;

        //Event Manager events attachment
        player.playerEventManager.OnPlayerHurt += TransitionToHurt;
        player.playerEventManager.OnPlayerDead += TransitionToDead;
        player.playerEventManager.OnPlayerWin += TransitionToWin;

        //State Action event attachment
        OnChangeToAir += TransitionToInAir;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();
        //Input Handler events detachment
        player.playerInputHandler.OnJumpAction -= TransitionToJump;
        player.playerInputHandler.OnFireAction -= TransitionToMeleeAttack;
        player.playerInputHandler.OnGunOn -= TransitionToGunMode;

        //Event Manager events detachment
        player.playerEventManager.OnPlayerHurt -= TransitionToHurt;
        player.playerEventManager.OnPlayerDead -= TransitionToDead;
        player.playerEventManager.OnPlayerWin -= TransitionToWin;

        //State Action event detachment
        OnChangeToAir -= TransitionToInAir;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {
        //Checks if player is touching ground. If not, calls the transition to the In Air State
        isGrounded = player.CheckIfGrounded();
        if (!isGrounded)
        {
            if (OnChangeToAir != null)
            {
                OnChangeToAir();
            }
        }

       /*                    #####TEST####
        if (player.InputHandler.IsJumping && isGrounded)
        {
            //Debug.Log("jump button pressed at state " + stateMachine.CurrentState);
            player.InputHandler.ResetJumpCondition();
            stateMachine.ChangeState(player.JumpState);
        }
        */
    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {

    }
    #endregion

    #region Player State Ground Methods

    //Changes current state to Player State Win
    private void TransitionToWin()
    {
        stateMachine.ChangeState(player.WinState);
    }

    //Changes current state to Player State Jump
    private void TransitionToJump()
    {
        stateMachine.ChangeState(player.JumpState);
    }

    //Changes current state to Player State Melee
    private void TransitionToMeleeAttack()
    {
        stateMachine.ChangeState(player.MeleeAttackState);
    }

    //Changes current state to Player State Gun Idle or Player State Gun Walk, depending on horizontal input value
    private void TransitionToGunMode()
    {
        if(player.InputHandler.HorizontalInput == 0f)
        {
            stateMachine.ChangeState(player.GunIdleState);
        }
        else if (player.InputHandler.HorizontalInput != 0f)
        {
            stateMachine.ChangeState(player.GunWalkState);
        }
    }

    //Changes current state to Player State In Air
    private void TransitionToInAir()
    {
        stateMachine.ChangeState(player.InAirState);
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
