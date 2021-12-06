using System;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : Player
{
    public AIStateMachine stateMachine;
    public AIStateId initialState = AIStateId.FindTarget;
    public GameObject opponent;
    public int shootingDistance = 2;
    public AISensor sensor;
    public NavMeshAgent navMeshAgent;
    public WeaponIK weapon;
    public AITargetSystem targetSystem;
    public GameObject[] buffer = new GameObject[1];
    public Transform targetTransform;

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
        stateMachine.RegisterAIState(new AIFindTargetState());
        stateMachine.RegisterAIState(new AIChargeStationState());
        stateMachine.ChangeState(initialState);
        opponent = null;
        sensor = GetComponent<AISensor>();
        weapon = GetComponent<WeaponIK>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetSystem = GetComponent<AITargetSystem>();
    }

    public new void Update()
    {
        base.Update();
        stateMachine.Update();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log($"{gameObject.name} in state {stateMachine.currentState}");
        }
    }

    public new void Shoot(GameObject shootObject, Player player = null)
    {
        base.Shoot(shootObject, player);
        if (IsDeactivated)
        {
            stateMachine.ChangeState(AIStateId.ChargeStation);
        }
    }

    public GameObject SelectBase()
    {
        var teamColor = GetComponent<PlayerConfiguration>().Team.TeamColor;
        GameObject[] bases = new GameObject[2];
        if (teamColor == Color.red)
        {
            bases[0] = GameObject.Find("ArenaObjects/BaseBlue-L/origin");
            bases[1] = GameObject.Find("ArenaObjects/BaseBlue-R/origin");
        }
        else if (teamColor == Color.blue)
        {
            bases[0] = GameObject.Find("ArenaObjects/BaseRed-L/origin");
            bases[1] = GameObject.Find("ArenaObjects/BaseRed-R/origin");
        }
        System.Random random = new System.Random();
        var randomBase = bases[random.Next(2)];
        return randomBase;
    }
}
