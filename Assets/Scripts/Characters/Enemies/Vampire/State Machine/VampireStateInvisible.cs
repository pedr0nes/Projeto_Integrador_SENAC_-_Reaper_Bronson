using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireStateInvisible : State
{
    protected Vampire vampire;
    protected VampireData vampireData;
    public static StateAction OnInvisibleStarted;

    public VampireStateInvisible(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName) : base(character, stateMachine, characterData, animParameterName)
    {
        vampire = (Vampire)character;
        vampireData = characterData as VampireData;
    }
    public override void Enter()
    {
        base.Enter();
        vampire.vampireEventManager.OnInvisibleEnded += TransitionToAppear;

        vampire.GetComponentInChildren<SpriteRenderer>().enabled = false;
        vampire.transform.position = vampire.transform.position + new Vector3(0, 50f, 0);

        if (OnInvisibleStarted != null)
        {
            OnInvisibleStarted();
        }
    }

    public override void Exit()
    {
        base.Exit();
        vampire.vampireEventManager.OnInvisibleEnded -= TransitionToAppear;
        vampire.transform.position = vampire.transform.position - new Vector3(0, 50f, 0);

    }

    public override void PhysicsTick()
    {

    }

    public override void Tick()
    {


    }

    public void TransitionToAppear()
    {
        stateMachine.ChangeState(vampire.AppearState);
    }



}
