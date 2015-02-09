using System;
using UnityEngine;

public abstract class State<CoreType> where CoreType : MonoBehaviour
{
    protected CoreType Owner { get; private set; }
    protected StateMachine<CoreType> FSM { get; private set; }

    public State(CoreType owner, StateMachine<CoreType> sm)
    {
        Owner = owner;
        FSM = sm;
    }

    abstract public void Enter();
    abstract public void Execute();
    abstract public void FixedExecute();
    abstract public void Exit();
}