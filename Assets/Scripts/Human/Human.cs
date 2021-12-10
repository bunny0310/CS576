using System;
using UnityEngine;

public class Human : Player
{
    GameObject SpineBone;
    Quaternion SpineBoneOriginalRotation;
    int[] movements = { 0, 0 }; // left, right & up, down
    GameObject SpineBoneControllerTarget;
    public new void Start()
    {
        gameObject.name = $"HumanAgent{Configuration.GenerateId()}";
        base.Start();
    }

    public new void Shoot(GameObject shootObject, Player player = null)
    {
        base.Shoot(shootObject, player);
        if (IsDeactivated)
        {
            return;
        }
    }

    public new void Update()
    {
        base.Update();
        // IMPLEMENT ME - Ishaan & Dhruv

        // MOVEMENT - ARROW KEYS
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            WalkForwards();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            DuckDown();
        }
        else
        {
            SwitchToIdle();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 6, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -6, 0, Space.World);
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var origin = GameObject.Find($"{gameObject.name}/bip/bip Pelvis/bip Spine/bip Spine1/bip R Clavicle/GunPivot/ShootPoint/WeaponRaycast");
            var shootOut = Physics.Raycast(origin.transform.position, origin.transform.forward, out hit, 100);
            Debug.DrawLine(origin.transform.position, origin.transform.forward, Color.black, 100);
            if (shootOut && hit.collider != null)
            {
                Shoot(hit.collider.gameObject, Configuration.RetreivePlayer(hit.collider.gameObject));
            }
        }

        try
        {
            RaycastHit hit;
            var scan = Physics.Raycast(transform.Find("bip/bip Pelvis/bip Spine/bip Spine1/bip Neck/bip Head").position, transform.forward, out hit, 5.0f);
            if (scan && hit.collider != null)
            {
                if (hit.collider.gameObject.name.Equals(ChargeStation.name))
                {
                    var rechargeAudioSource = GameObject.Find("RechargeAudioSource").GetComponent<AudioSource>();
                    if (!rechargeAudioSource.isPlaying)
                        rechargeAudioSource.PlayOneShot(RechargeClip);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }

}
