using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireStateAppear : State
{
    protected Vampire vampire;
    protected VampireData vampireData;

    


    public VampireStateAppear(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        vampire = (Vampire)character;
        vampireData = characterData as VampireData;
    }

    public override void Enter()
    {
        base.Enter();
        vampire.vampireSFX.PlayVampireSpawnSFX();
        
        if(!vampire.GetComponentInChildren<SpriteRenderer>().enabled)
        {
            vampire.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }

        vampire.vampireAnimationEvents.OnAnimationFinished += TransitionToWalk;
    }

    public override void Exit()
    {
        base.Exit();
        vampire.vampireAnimationEvents.OnAnimationFinished -= TransitionToWalk;

    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {
        vampire.LookAtPlayer();

    }

    private void TransitionToWalk()
    {
        stateMachine.ChangeState(vampire.WalkState);
    }

}
