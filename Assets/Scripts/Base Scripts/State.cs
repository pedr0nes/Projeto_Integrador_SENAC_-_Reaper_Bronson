using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* State Base Class
 * This class will serve as base class to all state machine subclasses
 */
public abstract class State
{
    #region Variables
    //Standard state delegate declared to be used by substates when events or actions are needed
    public delegate void StateAction();

    //IEnumerator type delegate declared for tests. It is currently not being used.
    public delegate IEnumerator StateActionCoroutine();

    //Constructor variables
    protected Character character;
    protected StateMachine stateMachine;
    protected CharacterData characterData;
    private string animParameterName;

    //Varible declared to store the time when actual state begins
    protected float startTime;
    #endregion

    //Class Constructor
    public State(Character character, StateMachine stateMachine, CharacterData characterData, string animParameterName)
    {
        this.character = character;
        this.stateMachine = stateMachine;
        this.characterData = characterData;
        this.animParameterName = animParameterName;
    }

    #region State Methods
    //Enter method. Will be called everytime a state starts.
    public virtual void Enter()
    {
        //Sets animation parameter value to true in animator
        character.Animator.SetBool(animParameterName, true);
        
        //Stores the time current state starts for further checks
        startTime = Time.time;
        
    }

    //Exit method. Will be called everytime a state ends.
    public virtual void Exit()
    {
        //Sets animation parameter value to false in animator
        character.Animator.SetBool(animParameterName, false);
    }

    //Tick method. Will be called in Unity Updade
    public abstract void Tick();

    //Physics Tick method. Will be called in Unity FixedUpdate
    public abstract void PhysicsTick();
    #endregion
}
