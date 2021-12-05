using System;
using UnityEngine;

public class AIChasePlayerState : AIState
{

    public void Enter(AIAgent agent)
    {
        agent.WalkTowardsTargetPlayer();
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
        var direction = agent.opponent.transform.position - agent.transform.position;
        var distance = direction.magnitude;
        var dotProduct = Vector3.Dot(direction.normalized, agent.transform.forward);
        Debug.Log("chasing player");
        if (distance <= agent.shootingDistance)
        {
            Debug.Log("going into shoot state");
            agent.stateMachine.ChangeState(AIStateId.Shoot);
        }
    }
}
