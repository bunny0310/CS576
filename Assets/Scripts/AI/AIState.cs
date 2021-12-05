using System;

public enum AIStateId
{
    ChasePlayer,
    Deactivated,
    Idle
}

public interface AIState
{
    AIStateId GetId();
    void Enter(AIAgent agent);
    void Update(AIAgent agent);
    void Exit(AIAgent agent);
}
