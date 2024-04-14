using System;
using System.Collections.Generic;

public class Fsm
{
    private FsmState currentStateClass;
    private GameState currentState;
    public Dictionary<GameState, FsmState> states = new Dictionary<GameState, FsmState>();

    public void AddState(GameState state, FsmState stateClass)
    {
        if (states.ContainsValue(stateClass) == false)
            states.Add(state, stateClass);
        else
            throw new Exception("State already exists");
    }

    public void SetState(GameState state)
    {
        if (state == currentState)
            return;

        if (states.TryGetValue(state, out FsmState newState))
        {
            currentStateClass?.Exit();
            currentStateClass = newState;
            currentState = state;
            currentStateClass?.Enter();
        }
        else
            throw new Exception("No such state");
    }

}
