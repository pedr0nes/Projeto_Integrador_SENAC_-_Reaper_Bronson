using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireStateIdle : State
{
    protected Vampire vampire;
    protected VampireData vampireData;

    public static StateAction OnIdleStarted;


    public VampireStateIdle(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        vampire = (Vampire)character;
        vampireData = characterData as VampireData;
    }

    public override void Enter()
    {
        base.Enter();
        vampire.vampireEventManager.OnIdleEnded += TransitionToDisappear;
        vampire.vampireEventManager.OnVampireDead += TransitionToDead;


        if (OnIdleStarted != null)
        {
            OnIdleStarted();
        }
    }

    public override void Exit()
    {
        base.Exit();
        vampire.vampireEventManager.OnIdleEnded -= TransitionToDisappear;
        vampire.vampireEventManager.OnVampireDead -= TransitionToDead;
    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {
        vampire.LookAtPlayer();

    }


    private void TransitionToDisappear()
    {
        stateMachine.ChangeState(vampire.DisappearState);
    }

    private void TransitionToDead()
    {
        stateMachine.ChangeState(vampire.DeadState);
    }
}
