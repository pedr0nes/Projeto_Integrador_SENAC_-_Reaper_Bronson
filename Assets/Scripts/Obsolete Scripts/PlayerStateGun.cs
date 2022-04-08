using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateGun : PlayerStateAbility
{
    public PlayerStateGun(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        

    }


    public override void Exit()
    {
        base.Exit();

    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    public override void Tick()
    {
        base.Tick();


    }



    private void TransitionToGunIdle()
    {

    }

    private void TransitionToGunWalk()
    {

    }

}
