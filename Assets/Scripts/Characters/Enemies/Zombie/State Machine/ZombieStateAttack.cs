using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateAttack : State
{
    protected Zombie zombie;
    protected ZombieData zombieData;
    public ZombieStateAttack(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        zombie = (Zombie)character;
        zombieData = characterData as ZombieData;
    }

    public override void Enter()
    {
        base.Enter();
        zombie.zombieSFX.PlayZombieAttackSFX();

        zombie.SetHorizontalVelocity(0f);
        zombie.zombieEventManager.OnPlayerGone += TransitionToIdle;
        zombie.zombieEventManager.OnZombieDead += TransitionToDead;
    }

    public override void Exit()
    {
        base.Exit();
        zombie.zombieEventManager.OnPlayerGone -= TransitionToIdle;
        zombie.zombieEventManager.OnZombieDead -= TransitionToDead;
    }

    public override void PhysicsTick()
    {
        
    }

    public override void Tick()
    {
        
    }

    private void TransitionToIdle()
    {
        stateMachine.ChangeState(zombie.IdleState);
    }

    private void TransitionToDead()
    {
        stateMachine.ChangeState(zombie.DeadState);
    }

}
