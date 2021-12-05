using System;
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
        agent.SwitchToIdle();
        var isHuman = agent.opponent.name.Contains("HumanAgent");
        var isBase = agent.opponent.name.Contains("Base");
        var isAI = !isHuman && agent.opponent.name.Contains("Agent");
        if (isHuman)
        {
            shootObject = ShootObject.Player;
            player = agent.opponent.GetComponent<Human>();
        }
        else if (isAI)
        {
            shootObject = ShootObject.Base;
            player = agent.opponent.GetComponent<AIAgent>();
        }
        else if(isBase)
        {
            shootObject = ShootObject.Base;
            player = null;
        }
        player?.SwitchToIdle();
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

        if (player && !player.DeactivatedStatus())
        {
            agent.opponent = null;
            agent.stateMachine.ChangeState(AIStateId.ChaseBase);
            agent.Shoot(agent.opponent, player);
            return;
        }
    }
}

