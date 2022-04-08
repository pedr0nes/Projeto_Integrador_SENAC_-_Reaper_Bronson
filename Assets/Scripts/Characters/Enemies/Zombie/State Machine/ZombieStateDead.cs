using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateDead : State
{
    protected Zombie zombie;
    protected ZombieData zombieData;

    public ZombieStateDead(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        zombie = (Zombie)character;
        zombieData = characterData as ZombieData;

    }

    public override void Enter()
    {
        base.Enter();
        zombie.zombieSFX.PlayZombieDieSFX();

        zombie.SetHorizontalVelocity(0f);

        zombie.zombieAnimationEvents.OnAnimationFinished += CallZombieDeath;
    }


    public override void Exit()
    {
        base.Exit();
        zombie.zombieAnimationEvents.OnAnimationFinished -= CallZombieDeath;
    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {

    }

    private void CallZombieDeath()
    {
        zombie.KillZombie();
    }



}
