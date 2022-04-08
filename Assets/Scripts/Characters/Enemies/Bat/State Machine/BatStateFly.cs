using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatStateFly : State
{
    protected Bat bat;
    protected BatData batData;

    private float abilityCurrentTime;

    public BatStateFly(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        bat = (Bat)character;
        batData = characterData as BatData;
    }

    public override void Enter()
    {
        base.Enter();
        abilityCurrentTime = 0;
        bat.batEventManager.OnPlayerGone += TransitionToIdle;
        bat.batEventManager.OnBatDead += TransitionToDead;

        Debug.Log("entrou em voo");

        //bat.batEventManager.OnPlayerFound += TransitionToAttack;
    }

    public override void Exit()
    {
        base.Exit();
        bat.batEventManager.OnPlayerGone -= TransitionToIdle;
        bat.batEventManager.OnBatDead -= TransitionToDead;
    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {
        TimeTransitionToAttack();

    }


    private void TimeTransitionToAttack()
    {
        abilityCurrentTime += Time.deltaTime;


        if (abilityCurrentTime > batData.timeBetweenAttacks)
        {
            stateMachine.ChangeState(bat.AttackState);
        }
    }


    private void TransitionToIdle()
    {
        stateMachine.ChangeState(bat.IdleState);
    }

    private void TransitionToDead()
    {
        stateMachine.ChangeState(bat.DeadState);
    }
}
