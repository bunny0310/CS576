using System;
using UnityEngine;

enum ShootObject
{
    Player,
    Base
}
public class AIShootState : AIState
{
    //Player player;
    ShootObject shootObject;
    public void Enter(AIAgent agent)
    {
        //agent.AnimationController.SetFloat("Speed", 0.0f);
        if (!agent.targetSystem.TargetPlayerInSight)
            agent.stateMachine.ChangeState(AIStateId.FindTarget);
        agent.weapon.SetTargetTransform(agent.targetSystem.TargetPlayer.transform);
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
        if (!agent.targetSystem.TargetPlayerInSight)
            agent.stateMachine.ChangeState(AIStateId.FindTarget);
        agent.Shoot(agent.targetSystem.TargetPlayer, Configuration.RetreivePlayer(agent.targetSystem.TargetPlayer));
    }
}

