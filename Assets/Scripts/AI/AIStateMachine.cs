using System;
public class AIStateMachine
{
    public AIState[] States;
    public AI Agent;
    public AIStateId CurrentState;

    public AIStateMachine(AI Agent)
    {
        this.Agent = Agent;
        int numStates = System.Enum.GetNames(typeof(AIStateId)).Length;
        this.States = new AIState[numStates];
    }

    public void RegisterAIState(AIState State)
    {
        int index = (int)State.GetId();
        this.States[index] = State;
    }

    public AIState GetState(AIStateId StateId)
    {
        return this.States[(int)StateId];
    }

    public void Update()
    {
        GetState(CurrentState)?.Update(Agent);
    }
}
