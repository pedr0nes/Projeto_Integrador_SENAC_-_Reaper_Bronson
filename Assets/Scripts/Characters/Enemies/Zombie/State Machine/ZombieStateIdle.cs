using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateIdle : State
{
    protected Zombie zombie;
    protected ZombieData zombieData;

    private float abilityCurrentTime;

    public ZombieStateIdle(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        zombie = (Zombie)character;
        zombieData = characterData as ZombieData;
    }

    public override void Enter()
    {
        base.Enter();
        abilityCurrentTime = 0;

        zombie.SetHorizontalVelocity(0f);
        zombie.zombieEventManager.OnPlayerFound += TransitionToAttack;
        zombie.zombieEventManager.OnZombieDead += TransitionToDead;


    }
    public override void Exit()
    {
        base.Exit();
        zombie.zombieEventManager.OnPlayerFound -= TransitionToAttack;
        zombie.zombieEventManager.OnZombieDead -= TransitionToDead;
    }
    public override void PhysicsTick()
    {

    }
    public override void Tick()
    {
        TimeTransitionToWalk();

        //Debug.Log("Tempo desde o inicio: " + (abilityCurrentTime - startTime));
    }


    private void TimeTransitionToWalk()
    {
        abilityCurrentTime += Time.deltaTime;

        if (abilityCurrentTime > Random.Range(zombieData.minIdleTime, zombieData.maxIdleTime))
        {
            stateMachine.ChangeState(zombie.WalkState);
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
