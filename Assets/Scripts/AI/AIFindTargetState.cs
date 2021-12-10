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
        agent.AnimationController.SetBool("DuckingDown", false);
        agent.weapon.FixRotation();
        agent.navMeshAgent.stoppingDistance = 5.0f;
        // agent.navMeshAgent.destination = GameObject.Find("SpotSkywalk").transform.position;
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
                if ((agent.transform.position - agent.navMeshAgent.destination).magnitude <= agent.navMeshAgent.stoppingDistance
                && !agent.targetSystem.TargetPlayerInSight)
                {
                    var newx = (float)random.Next(-22, 22);
                    var newy = 0.5f;
                    var newz = (float)random.Next(-45, 45);
                    var randomPosition = new Vector3(newx, newy, newz);
                    agent.navMeshAgent.destination = randomPosition;
                }
                timer = maxTime;
            }

            if (agent.targetSystem.TargetPlayerInSight)
            {
                agent.stateMachine.ChangeState(AIStateId.ChasePlayer);
            }
            //if (agent.attacker)
            //{
            //    var choice = random.Next(2);
            //    switch (choice)
            //    {
            //        case 0:
            //            agent.DuckDown();
            //            break;
            //        case 1:
            //            agent.stateMachine.ChangeState(AIStateId.Shoot);
            //            break;
            //        default:
            //            break;
            //    }
            //} else
            //{
            //    agent.SwitchToBlendTree();
            //}

        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
