using System;
using UnityEngine;

enum ShootObject
{
    Player,
    Base
}
public class AIShootState : AIState
{
    Player player;
    ShootObject shootObject;
    public void Enter(AIAgent agent)
    {
        agent.AnimationController.SetFloat("Speed", 0.0f);
    }

    public void Exit(AIAgent agent)
    {
        
    }

    public AIStateId GetId()
    {
        return AIStateId.Shoot;
    }

    public void Update(AIAgent agent)
    {
        if (!agent.targetSystem.HasTarget)
        {
            return;
        }
        var isPlayer = agent.targetSystem.Target.GetComponent<PlayerConfiguration>();
        Player player = null;
        if (isPlayer)
        {
            if (agent.targetSystem.Target.GetComponent<AIAgent>())
            {
                player = agent.targetSystem.Target.GetComponent<AIAgent>();
            } else if (agent.targetSystem.Target.GetComponent<Human>())
            {
                player = agent.targetSystem.Target.GetComponent<Human>();
            }
            Debug.Log($"Player is ? - {player}");
        }
        agent.weapon.SetTargetTransform(agent.targetSystem.Target.transform.Find("CenterTag").transform);
        agent.Shoot(agent.targetSystem.Target, player);
    }
}

