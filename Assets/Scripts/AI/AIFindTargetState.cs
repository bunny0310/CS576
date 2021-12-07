using System;
using UnityEngine;

public class AIFindTargetState : AIState
{
    public float maxDistance = 1.0f;
    public float maxTime = 10.0f;
    float timer = 0.0f;

    GameObject floor = GameObject.Find("TrainingStage_Background/Floor");
    System.Random random = new System.Random();

    public void Enter(AIAgent agent)
    {
        Debug.Log("Entering find target state");
        agent.navMeshAgent.stoppingDistance = 15.0f;
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
                    if ((agent.transform.position - agent.navMeshAgent.destination).magnitude <= agent.navMeshAgent.stoppingDistance)
                    {
                        var newx = (float)random.Next(30);
                        var newy = 0.5f;
                        var newz = (float)random.Next(44);
                        var randomPosition = new Vector3(newx, newy, newz);
                        Debug.Log(randomPosition);
                        agent.navMeshAgent.destination = randomPosition;
                    }
                timer = maxTime;
            }
            if (agent.targetSystem.HasTarget)
            {
                if (agent.targetSystem.Target.name.Contains("Base")
                    || Configuration.RetreivePlayer(agent.gameObject).GetComponent<PlayerConfiguration>().Team.TeamColor != Configuration.RetreivePlayer(agent.targetSystem.Target).GetComponent<PlayerConfiguration>().Team.TeamColor)
                agent.stateMachine.ChangeState(AIStateId.Shoot);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
