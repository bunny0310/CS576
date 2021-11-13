using System;
using System.Collections;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected Animator AnimationController;
    protected Camera Camera;
    protected CharacterController CharacterController;
    protected int Energy { get; set; }
    protected GameObject GunPoint { get; set; }
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
    protected Color VestColor { get; set; }
    private bool VestPutOn { get; set; }
    public Light VestLightFront { get; set; }
    public Light VestLightBack { get; set; }

    public void ChangeVestColor(Color color)
    {
        VestColor = color;
        VestLightBack.color = VestColor;
        VestLightFront.color = VestColor;
    }
    public void DecreaseEnergy()
    {

        // IMPLEMENT ME
    }

    public void DecreasePulses()
    {
        // IMPLEMENT ME
    }

    public void IncreaseScore(int score)
    {
        Score += score;
        Team.UpdateTeamScore(score);
    }

    public IEnumerator OmitLight()
    {
        // IMPLEMENT ME
        ChangeVestColor(Constants.ShotColor);
        yield return new WaitForSeconds(0.5f);
        ChangeVestColor(Team.TeamColor);
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
        // TO DO: Fix raycast, always pointing upwards
        if (IsDeactivated)
        {
            return;
        }
        RaycastHit hit;
        Physics.Raycast(GunPoint.transform.position, GunPoint.transform.forward, out hit, Mathf.Infinity);
        Debug.DrawRay(GunPoint.transform.position, (GunPoint.transform.forward) * 100, Color.green, 100, false);
        Debug.Log(hit.collider.gameObject.name); ;
        if (hit.collider != null && hit.collider.gameObject.name.Equals(Enum.GetName(typeof(PLAYER_TYPE), PLAYER_TYPE.AI))) {
            var AIObject = hit.collider.gameObject.GetComponent<AI>();
            Time.timeScale = 1.0f;
            StartCoroutine(AIObject.OmitLight());
            AIObject.DecreasePulses();

        }
    }

    public void Start()
    {
        AnimationController = GetComponent<Animator>();
        Camera = GetComponentInChildren<Camera>();
        CharacterController = GetComponent<CharacterController>();
        Energy = Constants.Energy;
        GunPoint = GameObject.Find($"/{gameObject.name}/GunPoint");
        Debug.Log(GunPoint.transform.position);
        Pulses = Constants.Pulses;
        Score = 0;
        Velocity = 0.0f;
        VestLightFront = GameObject.Find($"/{gameObject.name}/VestLightFront").GetComponent<Light>();
        VestLightBack = GameObject.Find($"/{gameObject.name}/VestLightBack").GetComponent<Light>();
        VestPutOn = false;
    }

    public void Update()
    {
        if (Team != null && !VestPutOn)
        {
            ChangeVestColor(Team.TeamColor);
            VestPutOn = true;
        }
    }

}

