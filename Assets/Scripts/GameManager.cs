using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject Player;
    Team BlueTeam;
    Team RedTeam;
    public Text TimeRemaining;
    public float TimeRemainingValue = 300.0f;
    public Cinemachine.CinemachineFreeLook freeLookCamera;

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimeRemaining.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Start()
    {
        Player = (GameObject)Resources.Load("Prefabs/Player", typeof(GameObject));  // player prefab
        if (Player == null)
            Debug.LogError("Error: could not find the apple prefab in the project! Did you delete/move the prefab from your project?");

        BlueTeam = new Team(1, Color.blue);
        RedTeam = new Team(1, Color.red);

        var BluePlayerStartPosition = GameObject.Find("BluePlayerPosition").transform.position;
        var RedPlayerStartPosition = GameObject.Find("RedPlayerPosition").transform.position;
        GameObject HumanObject = Instantiate(Player, BluePlayerStartPosition, Quaternion.identity);
        HumanObject.AddComponent<Human>();
        HumanObject.GetComponent<Human>().Team = BlueTeam;
        GameObject AIObject = Instantiate(Player, RedPlayerStartPosition, Quaternion.identity);
        AIObject.AddComponent<AI>();
        AIObject.GetComponent<AI>().Team = RedTeam;
        freeLookCamera.LookAt = GameObject.Find($"{HumanObject.name}/rig_CharRoot").transform;
        freeLookCamera.Follow = GameObject.Find($"{HumanObject.name}/rig_CharRoot/bip/bipPelvis/bipSpine/bipSpine1/bipNeck/bipHead").transform;
    }  
    public void Update()
    {
        if (TimeRemainingValue > 0)
        {
            TimeRemainingValue -= Time.deltaTime;
            DisplayTime(TimeRemainingValue);
        }

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            DeactivateCursor();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateCursor();
        }

    }

    private void DeactivateCursor()
    {
        // Make cursor invisible
        Cursor.visible = false;
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ActivateCursor()
    {
        // Make cursor invisible
        Cursor.visible = true;
        // Lock cursor
        Cursor.lockState = CursorLockMode.None;
    }
}
