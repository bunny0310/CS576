using System;

public class AIChasePlayerState : AIState
{

    public void Enter(AIAgent agent)
    {
        agent.WalkForwards();
    }

    public void Exit(AIAgent agent)
    {

    }

    public AIStateId GetId()
    {
        return AIStateId.ChasePlayer;
    }

    public void Update(AIAgent agent)
    {

    }
}
