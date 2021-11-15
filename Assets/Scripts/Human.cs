using System;
using UnityEngine;

public class Human : Player
{
    GameObject SpineTarget;
    Vector3 SpineTargetOriginalPosition;
    public new void Start()
    {
        base.Start();
        gameObject.name = Enum.GetName(typeof(PLAYER_TYPE), PLAYER_TYPE.HUMAN);
        SpineTarget = GameObject.Find("HeadTarget");
        SpineTargetOriginalPosition = SpineTarget.transform.position;
    }

    public new void Update()
    {
        base.Update();
        // IMPLEMENT ME - Ishaan & Dhruv
        var walkForwardsIncrement = Mathf.Abs(Velocity) < 300 ? 30f : 0f;
        var mousePosition = Input.mousePosition;
        //mousePosition.z = 30;
        //Vector3 worldPosition = Camera.ScreenToWorldPoint(mousePosition);
        //worldPosition.z = 30;
        //HeadTarget.transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        // GunPoint.transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Velocity += walkForwardsIncrement;
            AnimationController.SetInteger("state", (int)ANIMATION.WalkForward);
        } else
        {
            Velocity = 0;
            AnimationController.SetInteger("state", (int)ANIMATION.Idle);
        }

        // CONTROL GUN DIRECTION - ASWD

        if (Input.GetKey(KeyCode.S))
        {
            if (!(SpineTarget.transform.position.y < SpineTargetOriginalPosition.y - 300))
            {
                SpineTarget.transform.position = new Vector3(SpineTarget.transform.position.x, SpineTarget.transform.position.y - 10, SpineTarget.transform.position.z);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (!(SpineTarget.transform.position.y > SpineTargetOriginalPosition.y + 300))
            {
                SpineTarget.transform.position = new Vector3(SpineTarget.transform.position.x, SpineTarget.transform.position.y + 10, SpineTarget.transform.position.z);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (!(SpineTarget.transform.position.x < SpineTargetOriginalPosition.x - 600))
            {
                SpineTarget.transform.position = new Vector3(SpineTarget.transform.position.x - 10, SpineTarget.transform.position.y, SpineTarget.transform.position.z);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!(SpineTarget.transform.position.x > SpineTargetOriginalPosition.x + 600))
            {
                SpineTarget.transform.position = new Vector3(SpineTarget.transform.position.x + 10, SpineTarget.transform.position.y, SpineTarget.transform.position.z);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        GetComponent<CapsuleCollider>().center = new Vector3(GetComponent<CapsuleCollider>().center.x, 0.9f, GetComponent<CapsuleCollider>().center.z);
        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        MoveDirection = new Vector3(xdirection, 0.0f, zdirection);
        CharacterController.Move(MoveDirection * Velocity * Time.deltaTime);

    }

}
