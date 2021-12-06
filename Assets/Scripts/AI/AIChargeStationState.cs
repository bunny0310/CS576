using System;
using UnityEngine;

public class AIChargeStationState : AIState
{
    public void Enter(AIAgent agent)
    {
        Debug.Log("Charging now");
        agent.navMeshAgent.stoppingDistance = 2.0f;
        agent.navMeshAgent.destination = agent.ChargeStation.transform.position;
    }

    public void Exit(AIAgent agent)
    {
    }

    public AIStateId GetId()
    {
        return AIStateId.ChargeStation;
    }

    public void Update(AIAgent agent)
    {
        if ((agent.navMeshAgent.transform.position - agent.navMeshAgent.destination).magnitude <= agent.navMeshAgent.stoppingDistance)
        {
            Debug.Log("Switching to find target state");
            agent.Recharge();
            agent.stateMachine.ChangeState(AIStateId.FindTarget);
        }
    }
}
