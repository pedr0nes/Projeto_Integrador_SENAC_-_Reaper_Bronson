using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateGunWalkShoot : PlayerStateGunWalk
{
    public static event StateAction OnWalkShotFinished;

    private float abilityCurrentTime;


    public PlayerStateGunWalkShoot(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
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
        abilityCurrentTime = Time.time;



        if (abilityCurrentTime - startTime > 0.1f)
        {
            if (OnWalkShotFinished != null)
            {
                OnWalkShotFinished();
            }
        }

    }



}
