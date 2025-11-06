using System;
using System.Collections.Generic;
public class StateMachine<T> where T : BaseState
{
    private Dictionary<Type, T> _states = new();
    public T CurrentState { get; private set; }

    public void Initialize(Dictionary<Type, T> states)
    {
        _states = states;
    }

    public void ChangeState(Type newStateType)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }
        if (_states.TryGetValue(newStateType, out T newState))
        {
            CurrentState = newState;
            CurrentState.Enter();
        }
        else
        {
            throw new Exception($"State of type {newStateType} not found in the state machine.");
        }
    }
}
