using System;
using UnityEngine;

public class Human : Player
{
    GameObject HeadTarget;
    public new void Start()
    {
        base.Start();
        gameObject.name = Enum.GetName(typeof(PLAYER_TYPE), PLAYER_TYPE.HUMAN);
        HeadTarget = GameObject.Find("HeadTarget");
    }

    public new void Update()
    {
        base.Update();
        // IMPLEMENT ME - Ishaan & Dhruv
        var walkForwardsIncrement = Mathf.Abs(Velocity) < 3 ? 0.3f : 0f;
        var mousePosition = Input.mousePosition;
        mousePosition.z = 30;
        Vector3 worldPosition = Camera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 30;
        HeadTarget.transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        // GunPoint.transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            AnimationController.SetInteger("state", (int)ANIMATION.WalkForward);
            Velocity += walkForwardsIncrement;
        } else
        {
            AnimationController.SetInteger("state", (int)ANIMATION.Idle);
            Velocity = 0;
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
