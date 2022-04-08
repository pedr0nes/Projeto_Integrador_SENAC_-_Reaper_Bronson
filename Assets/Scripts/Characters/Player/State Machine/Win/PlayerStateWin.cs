using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWin : State
{
    protected Player player;
    protected PlayerData playerData;

    public PlayerStateWin(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        player = (Player)character;

        playerData = characterData as PlayerData;


    }

    public override void Enter()
    {
        base.Enter();
        player.IsImmune = true;
        player.SetHorizontalVelocity(0f);
        player.playerSFX.PlayWinSFX();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsTick()
    {

    }

    

    public override void Tick()
    {

    }
}
