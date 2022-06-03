using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Player State Dead Class is one of Player's State Machine main states
  It Derives from the abstract base class State
  It becomes active everytime player game object reaches defeat condition in game (dies by loosing all lives)
  Currently, it does not have any derived classes
 */

public class PlayerStateDead : State
{
    //Variable Declaration
    protected Player player;
    protected PlayerData playerData;

    //State class constructor
    public PlayerStateDead(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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

        //Animation events attachment
        player.playerAnimationEvents.OnAnimationFinished += CallPlayerDeath;

        //Play death sound effect
        player.playerSFX.PlayDeathSFX();
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Animation events detachment
        player.playerAnimationEvents.OnAnimationFinished -= CallPlayerDeath;
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

    #region Player State Dead Methods

    //Call Player method 'Kill Player', which destroys player game object.
    private void CallPlayerDeath()
    {
        player.KillPlayer();
    }

    #endregion

}
