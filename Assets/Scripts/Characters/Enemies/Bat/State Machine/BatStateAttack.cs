using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatStateAttack : State
{
    protected Bat bat;
    protected BatData batData;
    public BatStateAttack(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        bat = (Bat)character;
        batData = characterData as BatData;
    }


    public override void Enter()
    {
        base.Enter();
        bat.batSFX.PlayBatAttackSFX();
        bat.batAnimationEvents.OnAnimationFinished += TransitionToFly;
        bat.batAnimationEvents.OnBatAttacked += CallBatAttack;

        bat.batEventManager.OnBatDead += TransitionToDead;
    }

    public override void Exit()
    {
        base.Exit();

        bat.batAnimationEvents.OnAnimationFinished -= TransitionToFly;
        bat.batAnimationEvents.OnBatAttacked -= CallBatAttack;

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

    private void CallBatAttack()
    {
        bat.SonicWaveSpawn();
    }

}
