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
        SpineBone = GameObject.Find($"/{gameObject.name}/rig_CharRoot/bip/bipPelvis/bipSpine/bipSpine1");
        Debug.Log(SpineBone);
        SpineBoneOriginalRotation = SpineBone.transform.rotation;
        SpineBoneControllerTarget = GameObject.Find($"/{gameObject.name}/GunMovementRig/Target");
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

        // CONTROL GUN DIRECTION - ASWD

        if (Input.GetAxis("Mouse X") < 0)
        {
            if (movements[0] > -10)
            {
                SpineBoneControllerTarget.transform.position += Vector3.left * 2.5f;
                movements[0]--;
            }
        }

        if (Input.GetAxis("Mouse X") > 0)
        {
            if (movements[0] < 10)
            {
                SpineBoneControllerTarget.transform.position += Vector3.right * 2.5f;
                movements[0]++;
            }
        }

        if (Input.GetAxis("Mouse Y") > 0)
        {
            if (movements[1] > -10)
            {
                SpineBoneControllerTarget.transform.position += Vector3.up * 2.5f;
                movements[1]--;
            }
        }

        if (Input.GetAxis("Mouse Y") < 0)
        {
            if (movements[1] < 10)
            {
                SpineBoneControllerTarget.transform.position += Vector3.down * 2.5f;
                movements[1]++;
            }
        }
        //if (Input.GetKey(KeyCode.S))
        //{
        //    if (SpineBone.transform.rotation.eulerAngles.z > SpineBoneOriginalRotation.eulerAngles.z - 30)
        //    {
        //        SpineBone.transform.Rotate(0, 0, -10f, Space.World);
        //    }
        //}

        //if (Input.GetKey(KeyCode.W))
        //{
        //    if (SpineBone.transform.rotation.eulerAngles.z < SpineBoneOriginalRotation.eulerAngles.z + 30)
        //    {
        //        SpineBone.transform.Rotate(0, 0, 10f, Space.World);
        //    }
        //}

        if (Input.GetKey(KeyCode.A))
        {
            if (SpineBone.transform.rotation.eulerAngles.y > SpineBoneOriginalRotation.eulerAngles.y - 30)
            {
                SpineBone.transform.Rotate(0, -10f, 0, Space.World);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (SpineBone.transform.rotation.eulerAngles.y < SpineBoneOriginalRotation.eulerAngles.y + 30)
            {
                SpineBone.transform.Rotate(0, 10f, 0, Space.World);
            }
        }


        //if (Input.GetKey(KeyCode.D))
        //{
        //    if (!(SpineTarget.transform.position.x > SpineTargetOriginalPosition.x + 600))
        //    {
        //        SpineTarget.transform.Translate(10 * Vector3.right);
        //    }
        //}

        if (Input.GetMouseButton(0))
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
