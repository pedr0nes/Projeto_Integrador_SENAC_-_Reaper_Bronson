using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireStateAttack : State
{
    protected Vampire vampire;
    protected VampireData vampireData;
    public VampireStateAttack(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        vampire = (Vampire)character;
        vampireData = characterData as VampireData;
    }

    public override void Enter()
    {
        base.Enter();
        vampire.vampireSFX.PlayVampireAttackSFX();

        vampire.vampireAnimationEvents.OnAnimationFinished += TransitionToWalk;
        vampire.vampireEventManager.OnVampireDead += TransitionToDead;
    }



    public override void Exit()
    {
        base.Exit();
        vampire.vampireAnimationEvents.OnAnimationFinished -= TransitionToWalk;
        vampire.vampireEventManager.OnVampireDead -= TransitionToDead;
    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {


    }


    private void TransitionToWalk()
    {
        stateMachine.ChangeState(vampire.WalkState);
    }


    private void TransitionToDead()
    {
        stateMachine.ChangeState(vampire.DeadState);
    }
}
