using System;
using UnityEngine;

public class StateMachine<CoreType>
    where CoreType : MonoBehaviour
{
    private CoreType owner;

    public State<CoreType> CurrentState { get; private set; }
    public State<CoreType> PreviousState { get; private set; }

    public StateMachine(CoreType core)
    {
        CurrentState = null;
        PreviousState = null;
    }

    public void InitialState(State<CoreType> init)
    {
        CurrentState = init;
    }

    public void Execute()
    {
        CurrentState.Execute();
    }

    public void FixedExecute()
    {
        CurrentState.FixedExecute();
    }

    public void ChangeState(State<CoreType> newState)
    {
        CurrentState.Exit();
        PreviousState = CurrentState;
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void RevertState()
    {
        if(PreviousState != null)
            ChangeState(PreviousState);
    }
}
