using System;
public class AIStateMachine
{
    public AIState[] states;
    public AIAgent agent;
    public AIStateId currentState;

    public AIStateMachine(AIAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AIStateId)).Length;
        this.states = new AIState[numStates];
    }

    public void RegisterAIState(AIState State)
    {
        int index = (int)State.GetId();
        this.states[index] = State;
    }

    public AIState GetState(AIStateId stateId)
    {
        return this.states[(int)stateId];
    }

    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }

    public void ChangeState(AIStateId newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
