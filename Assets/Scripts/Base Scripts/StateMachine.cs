using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * State Machine Base Class
 * The purpose of this class is to initialize states and manage which state is active at the moment
 * Only one state needs to be active at the time
 */
public class StateMachine
{
    //Variable declaration
    #region Variables
    //public State type variable to store which state is the current one
    public State CurrentState { get; private set; }

    #endregion

    //Methods
    #region State Machine Management Methods

    //Calls State Machine initialization. Needs a starting State as parameter.
    public void Initialize(State startingState)
    {
        //Current is set to be the given starting State
        CurrentState = startingState;

        //Current State Enter method is called
        CurrentState.Enter();
    }

    //Changes State Machine Current State. Needs a new State as parameter.
    public void ChangeState(State newState)
    {
        //Calls the Exit method of Current State
        CurrentState.Exit();

        //Changes the Current State value to the New State provided
        CurrentState = newState;

        //Calls the Enter method of the New State (that now is the Current State)
        CurrentState.Enter();
    }
    #endregion
}
