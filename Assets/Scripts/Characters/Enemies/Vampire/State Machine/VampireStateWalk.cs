using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireStateWalk : State
{
    protected Vampire vampire;
    protected VampireData vampireData;

    public static StateAction OnWalkStarted;

    public VampireStateWalk(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        vampire = (Vampire)character;
        vampireData = characterData as VampireData;

    }

    public override void Enter()
    {
        base.Enter();

        vampire.CallChasingPlayerCoroutine();

        vampire.vampireEventManager.OnPlayerFound += TransitionToAttack;
        vampire.vampireEventManager.OnChasingEnded += TransitionToIdle;
        vampire.vampireEventManager.OnVampireDead += TransitionToDead;

        if (OnWalkStarted != null)
        {
            OnWalkStarted();

        }
    }

    public override void Exit()
    {
        base.Exit();
        vampire.vampireEventManager.OnPlayerFound -= TransitionToAttack;
        vampire.vampireEventManager.OnChasingEnded -= TransitionToIdle;
        vampire.vampireEventManager.OnVampireDead -= TransitionToDead;
    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {
        vampire.FollowPlayer();
        vampire.LookAtPlayer();
    }

    private void TransitionToAttack()
    {
        stateMachine.ChangeState(vampire.AttackState);
    }

    private void TransitionToIdle()
    {
        stateMachine.ChangeState(vampire.IdleState);
    }

    private void TransitionToDead()
    {
        stateMachine.ChangeState(vampire.DeadState);
    }
}
