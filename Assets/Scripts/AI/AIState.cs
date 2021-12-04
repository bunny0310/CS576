using System;

public enum AIStateId
{
    ChasePlayer
}

public interface AIState
{
    AIStateId GetId();
    void Enter(AI agent);
    void Update(AI agent);
    void Exit(AI agent);
}
