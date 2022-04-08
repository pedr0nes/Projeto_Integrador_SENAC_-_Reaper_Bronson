using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireStateDisappear : State
{
    protected Vampire vampire;
    protected VampireData vampireData;
    

    public VampireStateDisappear(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        vampire = (Vampire)character;
        vampireData = characterData as VampireData;
    }
    public override void Enter()
    {
        base.Enter();
        vampire.vampireAnimationEvents.OnAnimationFinished += TransitionToInvisible;
    }

    public override void Exit()
    {
        base.Exit();
        vampire.vampireAnimationEvents.OnAnimationFinished -= TransitionToInvisible;
    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {
        vampire.LookAtPlayer();

    }

    private void TransitionToInvisible()
    {
        stateMachine.ChangeState(vampire.InvisibleState);
    }

}
