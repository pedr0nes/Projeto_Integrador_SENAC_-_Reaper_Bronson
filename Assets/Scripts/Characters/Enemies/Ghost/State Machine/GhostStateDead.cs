using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateDead : State
{
    protected Ghost ghost;
    protected GhostData ghostData;


    public GhostStateDead(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        ghost = (Ghost)character;
        ghostData = characterData as GhostData;

    }

    public override void Enter()
    {
        base.Enter();
        ghost.ghostSFX.PlayGhostDieSFX();
        ghost.ghostAnimationEvents.OnAnimationFinished += CallGhostDeath;

    }

    public override void Exit()
    {
        base.Exit();
        ghost.ghostAnimationEvents.OnAnimationFinished -= CallGhostDeath;
    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {

    }

    private void CallGhostDeath()
    {
        ghost.ghostAnimationEvents.OnAnimationFinished -= CallGhostDeath;
        ghost.KillGhost();
    }
}
