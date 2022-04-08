using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateWalk : State
{
    protected Zombie zombie;
    protected ZombieData zombieData;

    private float abilityCurrentTime;

    public ZombieStateWalk(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        zombie = (Zombie)character;
        zombieData = characterData as ZombieData;
    }

    public override void Enter()
    {
        base.Enter();
        zombie.zombieEventManager.OnObstacleFound += zombie.FlipSprite;
        zombie.zombieEventManager.OnPlayerFound += TransitionToAttack;

        zombie.zombieEventManager.OnZombieDead += TransitionToDead;

    }



    public override void Exit()
    {
        base.Exit();
        zombie.zombieEventManager.OnObstacleFound -= zombie.FlipSprite;
        zombie.zombieEventManager.OnPlayerFound -= TransitionToAttack;
        zombie.zombieEventManager.OnZombieDead -= TransitionToDead;
    }

    public override void PhysicsTick()
    {
        zombie.SetHorizontalVelocity(zombieData.movementVelocity * zombie.FacingDirection);
    }

    public override void Tick()
    {
        TransitionToIdle();
    }


    private void TransitionToIdle()
    {
        abilityCurrentTime = Time.time;

        if (abilityCurrentTime - startTime > Random.Range(zombieData.minWalkTime, zombieData.maxWalkTime))
        {
            stateMachine.ChangeState(zombie.IdleState);
        }
    }

    private void TransitionToAttack()
    {
        stateMachine.ChangeState(zombie.AttackState);
    }

    private void TransitionToDead()
    {
        stateMachine.ChangeState(zombie.DeadState);
    }
}
