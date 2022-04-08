using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public delegate void StateAction();
    public delegate IEnumerator StateActionCoroutine();

    protected Character character;
    protected StateMachine stateMachine;
    protected CharacterData characterData;
    private string animParameterName;

    protected float startTime;

    public State(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName)
    {
        this.character = character;
        this.stateMachine = stateMachine;
        this.characterData = characterData;
        this.animParameterName = animParameterName;
    }

    public virtual void Enter()
    {
        character.Animator.SetBool(animParameterName, true);
        startTime = Time.time;
        
    }

    public virtual void Exit()
    {
        character.Animator.SetBool(animParameterName, false);
    }

    public abstract void Tick();
    public abstract void PhysicsTick();

}
