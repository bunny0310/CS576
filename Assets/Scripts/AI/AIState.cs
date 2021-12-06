using System;

public enum AIStateId
{
    ChasePlayer,
    ChaseBase,
    Deactivated,
    Idle,
    Shoot,
    FindTarget,
    ChargeStation
}

public interface AIState
{
    AIStateId GetId();
    void Enter(AIAgent agent);
    void Update(AIAgent agent);
    void Exit(AIAgent agent);
}
