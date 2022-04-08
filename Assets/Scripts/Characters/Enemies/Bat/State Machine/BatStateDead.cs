using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatStateDead : State
{
    protected Bat bat;
    protected BatData batData;
    public BatStateDead(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        bat = (Bat)character;
        batData = characterData as BatData;
    }

    public override void Enter()
    {
        base.Enter();
        bat.batSFX.PlayBatDieSFX();
        bat.Rigidbody2D.isKinematic = false;

        bat.batAnimationEvents.OnAnimationFinished += CallBatDeath;
    }

    public override void Exit()
    {
        base.Exit();
        bat.batAnimationEvents.OnAnimationFinished -= CallBatDeath;
    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {

    }

    private void CallBatDeath()
    {
        bat.KillBat();
    }
}
