using System;
using System.Collections;
using System.Threading;
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
    public float Velocity { get; set; }
    protected Color VestColor { get; set; }
    private bool VestPutOn { get; set; }
    public Light VestLightFront { get; set; }
    public Light VestLightBack { get; set; }
    private bool justShot = false;

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
        if (Pulses > 0)
            Pulses--;
        else
        {
            Pulses = 0;
            Energy = 0;
            IsDeactivated = true;
        }
    }

    public void DuckDown()
    {
        AnimationController.SetInteger("state", (int)ANIMATION.DuckDown);
    }

    public void IncreaseScore(int score)
    {
        Score += score;
        Team.UpdateTeamScore(score);
    }

    public IEnumerator OmitLight(GameObject shootObject, Vector3 location)
    {
        var pointLightObject = new GameObject("ShotLight");
        pointLightObject.transform.parent = shootObject.transform;
        var pointLight = pointLightObject.AddComponent<Light>();
        pointLight.type = UnityEngine.LightType.Point;
        pointLight.color = Color.white;
        pointLight.intensity = 20;
        pointLight.range = 1f;
        pointLightObject.transform.position = location;
        Quaternion rotation = Quaternion.LookRotation(GunPoint.transform.forward, Vector3.up);
        pointLightObject.transform.rotation = rotation;
        yield return new WaitForSeconds(0.5f);
        Destroy(pointLightObject);
        justShot = false;
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

    public void Shoot(GameObject shootObject)
    {
        if (justShot)
        {
            return;
        }
        if (IsDeactivated)
        {
            return;
        }
        try
        {
            justShot = true;
            DecreasePulses();
            StartCoroutine(this.OmitLight(shootObject, shootObject.transform.position));
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void Start()
    {
        AnimationController = GetComponent<Animator>();
        Camera = GetComponentInChildren<Camera>();
        CharacterController = GetComponent<CharacterController>();
        Energy = Constants.Energy;
        GunPoint = GameObject.Find($"/{gameObject.name}/bip/bip Pelvis/bip Spine/bip Spine1/bip R Clavicle/GunPivot/ShootPoint");
        // Debug.Log(GunPoint.transform.position);
        Pulses = Constants.Pulses;
        Score = 0;
        Velocity = 0.0f;
        VestLightFront = GameObject.Find($"{gameObject.name}/VestLightFront").GetComponent<Light>();
        VestLightBack = GameObject.Find($"{gameObject.name}/VestLightBack").GetComponent<Light>();
        VestPutOn = false;
    }

    public void SwitchToIdle()
    {
        Velocity = 0;
        AnimationController.SetInteger("state", (int)ANIMATION.Idle);
    }

    public void Update()
    {
        if (Team != null && !VestPutOn)
        {
            ChangeVestColor(Team.TeamColor);
            VestPutOn = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        if (transform.position.y > 0.51f)
        {
            transform.position = new Vector3(transform.position.x, 0.51f, transform.position.z);
        }

        GetComponent<CapsuleCollider>().center = new Vector3(GetComponent<CapsuleCollider>().center.x, 0.9f, GetComponent<CapsuleCollider>().center.z);
        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        MoveDirection = new Vector3(xdirection, 0.0f, zdirection);
        CharacterController.Move(MoveDirection * Velocity * Time.deltaTime);
    }

    public void WalkForwards()
    {
        var walkForwardsIncrement = Mathf.Abs(Velocity) < 3f ? 1f : 0f;
        Velocity += walkForwardsIncrement;
        AnimationController.SetInteger("state", (int)ANIMATION.WalkForward);
    }

}

