using System;
using UnityEngine;

public class AIAgent : Player
{
    public AIStateMachine stateMachine;
    public AIStateId initialState;
    public GameObject opponent;
    public int shootingDistance = 2;
    public AISensor sensor;
    public GameObject[] buffer = new GameObject[1];

    public new void Start()
    {
        gameObject.name = $"Agent{Configuration.GenerateId()}";
        base.Start();
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterAIState(new AIChasePlayerState());
        stateMachine.RegisterAIState(new AIChaseBaseState());
        stateMachine.RegisterAIState(new AIDeactivatedState());
        stateMachine.RegisterAIState(new AIIdleState());
        stateMachine.RegisterAIState(new AIShootState());
        stateMachine.ChangeState(initialState);
        opponent = null;
        sensor = GetComponent<AISensor>();
    }

    public new void Update()
    {
        base.Update();
        stateMachine.Update();
    }

    public void WalkTowardsTargetPlayer()
    {
        WalkTowards(opponent);
    }

    public void WalkTowardsBase()
    {
        var teamColor = GetComponent<PlayerConfiguration>().Team.TeamColor;
        GameObject[] bases = new GameObject[2];
        if (teamColor == Color.red)
        {
            bases[0] = GameObject.Find("ArenaObjects/BaseBlue-L");
            bases[1] = GameObject.Find("ArenaObjects/BaseBlue-R");
        } else if (teamColor == Color.blue)
        {
            bases[0] = GameObject.Find("ArenaObjects/BaseRed-L");
            bases[1] = GameObject.Find("ArenaObjects/BaseRed-R");
        }
        System.Random random = new System.Random();
        WalkTowards(bases[random.Next(2)]);
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
            WalkTowards(GameObject.Find("ArenaObjects/RedChargeStation"));
        }
    }
}
