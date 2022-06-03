using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State Win Class is one of Player's State Machine main states
  It Derives from the abstract base class State
  It becomes active everytime player game object reaches victory condition in game
  Currently, it does not have any derived classes
 */

public class PlayerStateWin : State
{
    //Variable Declaration
    protected Player player;
    protected PlayerData playerData;

    //State class constructor
    public PlayerStateWin(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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
        //Player property 'IsImmune' receives value true
        player.IsImmune = true;

        //Player Horizontal Velocity is set to 0 (zero)
        player.SetHorizontalVelocity(0f);

        //Play victory sound effect
        player.playerSFX.PlayWinSFX();
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();
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

    #region Player State Win Methods

    //No methods yet

    #endregion

}
