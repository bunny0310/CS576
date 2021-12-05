using System;
using UnityEngine;

public class AIChasePlayerState : AIState
{
    public float maxDistance = 1.0f;
    public float maxTime = 1.0f;
    float timer = 0.0f;

    public void Enter(AIAgent agent)
    {
        Debug.Log("Entering chase player state");
        agent.navMeshAgent.stoppingDistance = 5.0f;
        if (agent.targetTransform == null)
        {
            Team agentTeam = agent.GetComponent<PlayerConfiguration>().Team;
            var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            var oppTeam = agentTeam.TeamColor == Color.red ? gameManager.BlueTeam : gameManager.RedTeam;
            System.Random random = new System.Random();
            var oppPlayers = oppTeam.GetPlayers();
            Debug.Log(oppPlayers.Count);
            var randomIdx = random.Next(oppPlayers.Count);
            agent.targetTransform = oppPlayers[randomIdx].gameObject.transform;
        }
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
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                float sqDistance = (agent.targetTransform.position - agent.navMeshAgent.destination).sqrMagnitude;
                if (sqDistance > maxDistance * maxDistance)
                {
                    agent.navMeshAgent.destination = agent.targetTransform.position;
                }
                timer = maxTime;
            }
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
