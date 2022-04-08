using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDead : State
{

    protected Player player;
    protected PlayerData playerData;

    public PlayerStateDead(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        player = (Player)character;
        playerData = characterData as PlayerData;
    }

    public override void Enter()
    {
        base.Enter();
        player.playerSFX.PlayDeathSFX();
        player.playerAnimationEvents.OnAnimationFinished += CallPlayerDeath;
    }

    public override void Exit()
    {
        base.Exit();
        player.playerAnimationEvents.OnAnimationFinished -= CallPlayerDeath;
    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {

    }


    private void CallPlayerDeath()
    {
        player.KillPlayer();
    }
}
