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
        Player player = Configuration.RetreivePlayer(agent.targetSystem.Target.transform.parent.gameObject);
        GameObject target = agent.targetSystem.Target;
        agent.transform.LookAt(target.transform);
        agent.weapon.SetTargetTransform(target.transform.parent.gameObject.name.Contains("Agent") ? target.transform : target.transform.Find("origin").transform);
        agent.Shoot(agent.targetSystem.Target, player);
    }
}

