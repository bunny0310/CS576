using System;
public class AIChaseBaseState : AIState
{
    public void Enter(AIAgent agent)
    {
        agent.WalkTowardsBase();
    }

    public void Exit(AIAgent agent)
    {
        
    }

    public AIStateId GetId()
    {
        return AIStateId.ChaseBase;
    }

    public void Update(AIAgent agent)
    {
        int count = agent.sensor.Filter(agent.buffer, "PlayerLayer");
        if (count > 0)
        {
            if (agent.buffer[0].GetComponent<PlayerConfiguration>().Team.TeamColor != agent.GetComponent<PlayerConfiguration>().Team.TeamColor)
            {
                agent.opponent = agent.buffer[0];
                agent.stateMachine.ChangeState(AIStateId.ChasePlayer);
            }
        }
    }
}

