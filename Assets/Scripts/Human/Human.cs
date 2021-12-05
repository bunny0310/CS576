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
            transform.Rotate(0, 3, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -3, 0, Space.World);
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var origin = GameObject.Find($"{gameObject.name}/bip/bip Pelvis/bip Spine/bip Spine1/bip R Clavicle/GunPivot/ShootPoint/WeaponRaycast");
            var shootOut = Physics.Raycast(origin.transform.position, origin.transform.forward, out hit, 100);
            if (hit.collider != null)
            {
                Shoot(hit.collider.gameObject);
            }
        }

    }

}
