using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerConfiguration playerConfiguration;
    public Animator AnimationController;
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
    protected PLAYER_TYPE PlayerType { get; set; }
    public float Velocity { get; set; }
    protected Color VestColor { get; set; }
    private bool VestPutOn { get; set; }
    public Light VestLightFront { get; set; }
    public Light VestLightBack { get; set; }
    private bool justShot = false;
    public GameObject ChargeStation;
    public AudioClip RechargeClip;
    public AudioClip ShootClip;

    public void ChangeVestColor(Color color)
    {
        VestColor = color;
        VestLightBack.color = VestColor;
        VestLightFront.color = VestColor;
    }
    public void DecreaseEnergy()
    {
        if (Energy > 0)
            Energy--;
        else
        {
            Deactivate();
        }
    }

    public void DecreasePulses()
    {
        if (Pulses > 0)
            Pulses--;
        else
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        Pulses = 0;
        Energy = 0;
        IsDeactivated = true;
        ChangeVestColor(Color.yellow);
    }

    public void DuckDown()
    {
        AnimationController.SetInteger("state", (int)ANIMATION.DuckDown);
    }

    public void IncreaseScore(int score)
    {
        Score += score;
        playerConfiguration.Team.UpdateTeamScore(score);
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

    protected void OnChargeStation()
    {
    }

    public bool DeactivatedStatus()
    {
        return IsDeactivated;
    }

    public void Recharge()
    {
        Pulses = Constants.Pulses;
        Energy = Constants.Energy;
        IsDeactivated = false;
        ChangeVestColor(playerConfiguration.Team.TeamColor);
    }

    public void Shoot(GameObject shootObject, Player player = null)
    {
        if (IsDeactivated)
        {
            return;
        }
        if (justShot)
        {
            return;
        }
        var rechargeAudioSource = GameObject.Find("RechargeAudioSource").GetComponent<AudioSource>();
        if (!rechargeAudioSource.isPlaying)
            rechargeAudioSource.PlayOneShot(ShootClip);
        try
        {
            Debug.Log($"Shooting... \n Player attacked - {player != null}");
            justShot = true;
            if (player != null)
            {
                Debug.Log("Great Tag!");
                player.DecreaseEnergy();
                Debug.Log($"Energy reamining - {player.Energy}");
                Debug.Log($"Deactivated status - {player.IsDeactivated}");
                player.GetComponent<PlayerConfiguration>().Team.UpdateTeamScore(-150);
                GetComponent<PlayerConfiguration>().Team.UpdateTeamScore(250);
                StartCoroutine(this.OmitLight(shootObject.transform.Find("CenterTag").gameObject, shootObject.transform.Find("CenterTag").position));
            }
            DecreasePulses();
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void Start()
    {
        playerConfiguration = GetComponent<PlayerConfiguration>();
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
        if (playerConfiguration.Team != null && !VestPutOn)
        {
            ChangeVestColor(playerConfiguration.Team.TeamColor);
            VestPutOn = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        //if (transform.position.y > 0.51f)
        //{
        //    transform.position = new Vector3(transform.position.x, 0.51f, transform.position.z);
        //}

        GetComponent<CapsuleCollider>().center = new Vector3(GetComponent<CapsuleCollider>().center.x, 0.9f, GetComponent<CapsuleCollider>().center.z);
        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        MoveDirection = new Vector3(xdirection, 0.0f, zdirection);
        CharacterController.Move(MoveDirection * Velocity * Time.deltaTime);
    }

    protected void WalkForwards()
    {
        var walkForwardsIncrement = Mathf.Abs(Velocity) < 6f ? 1.5f : 0f;
        Velocity += walkForwardsIncrement;
        AnimationController.SetInteger("state", (int)ANIMATION.WalkForward);
    }

}

