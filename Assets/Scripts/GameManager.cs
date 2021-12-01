using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject Human;
    GameObject AI;
    Team BlueTeam;
    Team RedTeam;
    public Text TimeRemaining;
    public float TimeRemainingValue = 300.0f;
    public Cinemachine.CinemachineFreeLook freeLookCamera;
    public Camera TimeRemainingCamera;
    private List<Camera> cameras;
    private int currentCameraIndex = 0;
    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimeRemaining.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Start()
    {
        cameras = new List<Camera>();
        Human = (GameObject)Resources.Load("Prefabs/HUMAN", typeof(GameObject));  // human prefab
        if (Human == null)
            Debug.LogError("Error: could not find the human prefab in the project! Did you delete/move the prefab from your project?");
        AI = (GameObject)Resources.Load("Prefabs/AI", typeof(GameObject));  // human prefab
        if (AI == null)
            Debug.LogError("Error: could not find the AI prefab in the project! Did you delete/move the prefab from your project?");

        BlueTeam = new Team(1, Color.blue);
        RedTeam = new Team(1, Color.red);

        try
        {
            var BluePlayerStartPosition = GameObject.Find("BluePlayerPosition").transform.position;
            var RedPlayerStartPosition = GameObject.Find("RedPlayerPosition").transform.position;
            GameObject HumanObject = Instantiate(Human, BluePlayerStartPosition, Quaternion.identity);
            cameras.Add(HumanObject.GetComponentInChildren<Camera>());
            cameras.Add(TimeRemainingCamera);
            HumanObject.AddComponent<Human>();
            var HumanComponent = HumanObject.GetComponent<Human>();
            HumanComponent.Team = BlueTeam;
            freeLookCamera.LookAt = GameObject.Find($"{HumanComponent.gameObject.name}/bip").transform;
            freeLookCamera.Follow = GameObject.Find($"{HumanComponent.gameObject.name}/bip/bip Pelvis/bip Spine/bip Spine1/bip Neck/bip Head").transform;

            GameObject AIObject = Instantiate(AI, RedPlayerStartPosition, Quaternion.identity);
            var AIComponent = AIObject.GetComponent<AI>();
            AIComponent.Team = RedTeam;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    private void SwitchCamera()
    {
        for (int i = 0; i < cameras.Count; ++i)
        {
            if (i == currentCameraIndex)
            {
                cameras[i].enabled = true;
            }
            else
            {
                cameras[i].enabled = false;
            }
        }
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentCameraIndex++;
            if (currentCameraIndex == cameras.Count)
            {
                currentCameraIndex = 0;
            }
            SwitchCamera();
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
