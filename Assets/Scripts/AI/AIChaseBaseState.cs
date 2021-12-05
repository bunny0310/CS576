using System;
using UnityEngine;

public class AIChaseBaseState : AIState
{
    GameObject baseObj;
    public void Enter(AIAgent agent)
    {
        baseObj = agent.SelectBase();
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
        agent.WalkTowardsBase(baseObj);
        int count = agent.sensor.Filter(agent.buffer, "PlayerLayer");
        if (count > 0)
        {
            if (agent.buffer[0].GetComponent<PlayerConfiguration>().Team.TeamColor != agent.GetComponent<PlayerConfiguration>().Team.TeamColor)
            {
                agent.opponent = agent.buffer[0].transform.Find("CenterTag").gameObject;
                agent.stateMachine.ChangeState(AIStateId.ChasePlayer);
            }
            return;
        }

        count = agent.sensor.Filter(agent.buffer, "BaseLayer");
        if (count > 0)
        {
            Debug.Log("attacking the base now");
            Debug.Log(agent.buffer[0]);
            agent.opponent = agent.buffer[0];
            agent.stateMachine.ChangeState(AIStateId.Shoot);
        }
    }
}

