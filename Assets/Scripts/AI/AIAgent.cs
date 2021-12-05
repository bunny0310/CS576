using System;
using UnityEngine;

public class AIAgent : Player
{
    public AIStateMachine stateMachine;
    public AIStateId initialState;
    private Transform opponentTransform;
    private AISensor sensor;
    private GameObject[] buffer = new GameObject[1];

    public new void Start()
    {
        gameObject.name = $"Agent{Configuration.GenerateId()}";
        base.Start();
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterAIState(new AIChasePlayerState());
        stateMachine.RegisterAIState(new AIDeactivatedState());
        stateMachine.RegisterAIState(new AIIdleState());
        stateMachine.ChangeState(initialState);
        opponentTransform = null;
        sensor = GetComponent<AISensor>();
    }

    public new void Update()
    {
        base.Update();
        stateMachine.Update();
        int count = sensor.Filter(buffer, "PlayerLayer");
        if (count > 0)
        {
            if (buffer[0].GetComponent<PlayerConfiguration>().Team.TeamColor != GetComponent<PlayerConfiguration>().Team.TeamColor)
            {
                stateMachine.ChangeState(AIStateId.ChasePlayer);
            }
        }
    }

    void SetAimAndShoot(GameObject detected)
    {
        if (detected == null)
        {
            return;
        }
        gameObject.GetComponent<WeaponIK>().SetTargetTransform(detected.transform);
        Shoot(detected);
        Debug.Log(Pulses);
        if (IsDeactivated)
        {
            GetComponent<WeaponIK>().FixRotation();
            GetComponent<WeaponIK>().targetTransform = null;
            transform.LookAt(GameObject.Find("ArenaObjects/RedChargeStation").transform);
            WalkForwards();
        }
    }
}
