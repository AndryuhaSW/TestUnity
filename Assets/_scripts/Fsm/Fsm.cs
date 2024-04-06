using System;
using System.Collections.Generic;

public class Fsm
{

    private FsmState currentState;
    private string currentStateName;
    private Dictionary<string, FsmState> states = new Dictionary<string, FsmState>();

    public void Initialize(string stateName)
    {
        SetState(stateName);
    }

    public void AddState(string stateName, FsmState state)
    {
        if (states.ContainsValue(state) == false)
            states.Add(stateName, state);
        else
            throw new Exception("Invalid state exception");
    }

    public void SetState(string stateName)
    {
        if (stateName == currentStateName)
            return;

        if (states.TryGetValue(stateName, out FsmState newState))
        {
            currentState?.Exit();
            currentState = newState;
            currentStateName = stateName;
            currentState?.Enter();
        }
        else
            throw new Exception("No such state");
    }

}
