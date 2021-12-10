using System;
using UnityEngine;

public class AIChasePlayerState : AIState
{
    public float maxDistance = 1.0f;
    public float maxTime = 1.0f;
    float timer = 0.0f;

    public void Enter(AIAgent agent)
    {
        if (!agent.targetSystem.TargetPlayerInSight)
            agent.stateMachine.ChangeState(AIStateId.FindTarget);
        agent.navMeshAgent.destination = agent.targetSystem.TargetPlayer.transform.position;
        agent.transform.LookAt(agent.targetSystem.TargetPlayer.transform.Find("CenterTag"));
        agent.weapon.SetTargetTransform(agent.targetSystem.TargetPlayer.transform.Find("CenterTag"));
        agent.navMeshAgent.stoppingDistance = 5.0f;
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
        try
        {
            if (!agent.targetSystem.TargetPlayerInSight)
                agent.stateMachine.ChangeState(AIStateId.FindTarget);
            agent.stateMachine.ChangeState(AIStateId.Shoot);
            //float distance = (agent.targetTransform.position - agent.navMeshAgent.destination).magnitude;
            //if (distance <= agent.navMeshAgent.stoppingDistance)
            //{

            //}
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
