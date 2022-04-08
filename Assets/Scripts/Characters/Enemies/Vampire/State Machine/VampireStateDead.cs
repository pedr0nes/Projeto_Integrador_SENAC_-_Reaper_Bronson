using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireStateDead : State
{
    protected Vampire vampire;
    protected VampireData vampireData;

    private float abilityCurrentTime;

    public VampireStateDead(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        vampire = (Vampire)character;
        vampireData = characterData as VampireData;
    }

    public override void Enter()
    {
        base.Enter();
        abilityCurrentTime = 0f;
        vampire.CallFlahsDeathEffect();
        vampire.vampireSFX.PlayVampireDieSFX();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {
        abilityCurrentTime += Time.deltaTime;

        if (abilityCurrentTime > 5f)
        {
            CallKillVampire();
        }

    }


    private void CallKillVampire()
    {
        vampire.KillVampire();
    }

}
