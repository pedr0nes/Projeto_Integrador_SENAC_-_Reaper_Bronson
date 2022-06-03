using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Bat State Attack class is one of Bats' possible states
  It Derives from the abstract base class State
  It is the class/state active when the bat is spawning its attacks. It manages the actions that happen during this time.
 */

public class BatStateAttack : State
{
    //Variable Declaration
    protected Bat bat;
    protected BatData batData;

    //Class Constructor
    public BatStateAttack(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        //Forces constructor variable 'character' to be its derived type 'Bat'    
        bat = character as Bat;

        //Forces constructor variable 'characterData' to be its derived type 'BatData'
        batData = characterData as BatData;
    }

    #region State Methods

    //Enter method. Will be called everytime a state starts.
    public override void Enter()
    {
        base.Enter();

        //Play bat attack sound effect
        bat.batSFX.PlayBatAttackSFX();

        //Animation events attachment
        bat.batAnimationEvents.OnAnimationFinished += TransitionToFly;
        bat.batAnimationEvents.OnBatAttacked += CallBatAttack;

        //Event Manager events attachment
        bat.batEventManager.OnBatDead += TransitionToDead;
    }

    //Exit method. Will be called everytime a state ends.
    public override void Exit()
    {
        base.Exit();

        //Animation events detachment
        bat.batAnimationEvents.OnAnimationFinished -= TransitionToFly;
        bat.batAnimationEvents.OnBatAttacked -= CallBatAttack;

        //Event Manager events detachment
        bat.batEventManager.OnBatDead -= TransitionToDead;
    }

    //Tick method. Will be called in Unity Updade
    public override void Tick()
    {

    }

    //Physics Tick method. Will be called in Unity FixedUpdate
    public override void PhysicsTick()
    {

    }

    #endregion

    #region Bat State Attack Methods

    //Changes current state to Bat State Fly
    private void TransitionToFly()
    {
        stateMachine.ChangeState(bat.FlyState);
    }

    //Changes current state to Bat State Dead
    private void TransitionToDead()
    {
        stateMachine.ChangeState(bat.DeadState);
    }

    //Calls bat script method that spawns attacks (sonic waves)
    private void CallBatAttack()
    {
        bat.SonicWaveSpawn();
    }

    #endregion
}
