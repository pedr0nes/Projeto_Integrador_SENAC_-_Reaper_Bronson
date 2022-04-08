using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatStateIdle : State
{
    protected Bat bat;
    protected BatData batData;

    public BatStateIdle(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        bat = (Bat)character;
        batData = characterData as BatData;
    }

    public override void Enter()
    {
        base.Enter();
        bat.batEventManager.OnPlayerFound += TransitionToFly;
        bat.batEventManager.OnBatDead += TransitionToDead;
    }

    public override void Exit()
    {
        base.Exit();
        bat.batEventManager.OnPlayerFound -= TransitionToFly;
        bat.batEventManager.OnBatDead -= TransitionToDead;
    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {

    }

    private void TransitionToFly()
    {
        stateMachine.ChangeState(bat.FlyState);
    }


    private void TransitionToDead()
    {
        stateMachine.ChangeState(bat.DeadState);
    }
}
