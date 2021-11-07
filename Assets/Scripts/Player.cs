using System;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected Animator AnimationController;
    protected Camera Camera;
    protected CharacterController CharacterController;
    protected int Energy { get; set; }
    protected int Id { get; set; }
    protected bool IsDeactivated { get; set; }
    protected LIGHT_TYPE LightType { get; set; }
    protected Vector3 MoveDirection { get; set; }
    protected string Name { get; set; }
    protected Vector3 Position { get; set; }
    protected int Pulses { get; set; }
    protected long Score { get; set; }
    public Team Team { get; set; }
    protected PLAYER_TYPE PlayerType { get; set; }
    protected float Velocity { get; set; }
    protected Light VestLightFront { get; set; }
    protected Light VestLightBack { get; set; }

    public void DecreaseEnergy()
    {
        // IMPLEMENT ME
    }

    public void DecreasePulses()
    {
        // IMPLEMENT ME
    }

    public void OmitLight()
    {
        // IMPLEMENT ME
    }

    protected bool OnChargeStation()
    {
        // IMPLEMENT ME
        return true;
    }

    public void Recharge()
    {
        // IMPLEMENT ME
    }

    public void Shoot()
    {
        // IMPLEMENT ME
    }

    public void Start()
    {
        AnimationController = GetComponent<Animator>();
        Camera = GetComponentInChildren<Camera>();
        CharacterController = GetComponent<CharacterController>();
        Energy = Constants.Energy;
        Pulses = Constants.Pulses;
        Score = 0;
        Velocity = 0.0f;
        VestLightFront = GameObject.Find($"/{gameObject.name}/VestLightFront").GetComponent<Light>();
        VestLightBack = GameObject.Find($"/{gameObject.name}/VestLightBack").GetComponent<Light>();
        var teamColor = Team.TeamColor;
        Color color = Color.black;
        if (teamColor == TEAM_COLOR.BLUE)
        {
            color = Color.blue;
        }
        else
        {
            color = Color.red;
        }
        VestLightBack.color = color;
    }

}

