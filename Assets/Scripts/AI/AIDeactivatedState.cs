using System;
public class AIDeactivatedState : AIState
{

    public void Enter(AIAgent agent)
    {
        
    }

    public void Exit(AIAgent agent)
    {
    }

    public AIStateId GetId()
    {
        return AIStateId.Deactivated;
    }

    public void Update(AIAgent agent)
    {
    }
}
