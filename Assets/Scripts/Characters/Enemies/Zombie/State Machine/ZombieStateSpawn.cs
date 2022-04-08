using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateSpawn : State
{
    protected Zombie zombie;
    protected ZombieData zombieData;


    public ZombieStateSpawn(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        zombie = (Zombie)character;
        zombieData = characterData as ZombieData;


    }

    public override void Enter()
    {
        base.Enter();
        zombie.zombieSFX.PlayZombieSpawnSFX();
        zombie.zombieAnimationEvents.OnAnimationFinished += TransitionToIdle;
    }

    public override void Exit()
    {
        base.Exit();
        zombie.zombieAnimationEvents.OnAnimationFinished -= TransitionToIdle;
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

}
