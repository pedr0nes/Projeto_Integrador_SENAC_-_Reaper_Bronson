using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateFly : State
{
    protected Ghost ghost;
    protected GhostData ghostData;

    public GhostStateFly(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        ghost = (Ghost)character;
        ghostData = characterData as GhostData;
    }

    public override void Enter()
    {
        base.Enter();
        ghost.ghostEventManager.OnGhostDead += TransitionToDead;
        ghost.ghostAnimationEvents.OnGhostAttacked += CallGhostAttack; 
    }

    public override void Exit()
    {
        base.Exit();
        ghost.ghostEventManager.OnGhostDead -= TransitionToDead;
        ghost.ghostAnimationEvents.OnGhostAttacked -= CallGhostAttack;
    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {

    }

    private void TransitionToDead()
    {
        
        stateMachine.ChangeState(ghost.DeadState);
    }

    private void CallGhostAttack()
    {
        ghost.GhostAttack();
        
    }
}
