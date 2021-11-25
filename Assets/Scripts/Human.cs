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
        base.Start();
        gameObject.name = Enum.GetName(typeof(PLAYER_TYPE), PLAYER_TYPE.HUMAN);
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
            Velocity = 0;
            DuckDown();
        }
        else
        {
            SwitchToIdle();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 1, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -1, 0, Space.World);
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
