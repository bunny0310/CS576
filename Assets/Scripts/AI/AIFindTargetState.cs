using System;
using UnityEngine;

public class AIFindTargetState : AIState
{
    public float maxDistance = 1.0f;
    public float maxTime = 1.0f;
    float timer = 0.0f;

    GameObject floor = GameObject.Find("TrainingStage_Background/Floor");
    System.Random random = new System.Random();

    public void Enter(AIAgent agent)
    {
        Debug.Log("Entering find target state");
        agent.navMeshAgent.stoppingDistance = 20.0f;
    }

    public void Exit(AIAgent agent)
    {
    }

    public AIStateId GetId()
    {
        return AIStateId.FindTarget;
    }

    public void Update(AIAgent agent)
    {
        try
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                    var newx = (float)random.NextDouble() * (float)random.NextDouble() * floor.transform.localScale.x;
                    var newy = 0.5f;
                    var newz = (float)random.NextDouble() * floor.transform.localScale.z;
                    var randomPosition = new Vector3(newx, newy, newz);
                    Debug.Log(randomPosition);
                    agent.navMeshAgent.destination = randomPosition;
                timer = maxTime;
            }
            if (agent.targetSystem.HasTarget)
            {
                agent.stateMachine.ChangeState(AIStateId.Shoot);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
