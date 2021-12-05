using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject Human;
    GameObject HumanAgent;
    GameObject AIAgent;
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

        Human = (GameObject)Resources.Load("Prefabs/Human", typeof(GameObject));  // human prefab
        AIAgent = (GameObject)Resources.Load("Prefabs/AIAgent", typeof(GameObject));  // AI Agent prefab
        if (Human == null)
        {
            Debug.LogError("Error: could not find the human prefab in the project! Did you delete/move the prefab from your project?");
        }
        if (AIAgent == null)
        {
            Debug.LogError("Error: could not find the AIAgent prefab in the project! Did you delete/move the prefab from your project?");
        }
            

        BlueTeam = new Team(5, Color.blue);
        RedTeam = new Team(5, Color.red);

        try
        {
            // spawn blue team players
            var BlueTeamtartingPoint = GameObject.Find("BlueTeamPosition").transform.position;
            GameObject HumanObject = Instantiate(Human, BlueTeamtartingPoint, Quaternion.identity);
            BlueTeamtartingPoint = new Vector3(BlueTeamtartingPoint.x + 5, BlueTeamtartingPoint.y, BlueTeamtartingPoint.z);
            cameras.Add(HumanObject.GetComponentInChildren<Camera>());
            var HumanComponent = HumanObject.GetComponent<Human>();
            HumanObject.GetComponent<PlayerConfiguration>().Team = BlueTeam;
            freeLookCamera.LookAt = GameObject.Find($"{HumanComponent.gameObject.name}/bip").transform;
            freeLookCamera.Follow = GameObject.Find($"{HumanComponent.gameObject.name}/bip/bip Pelvis/bip Spine/bip Spine1/bip Neck/bip Head").transform;

            for (int i=1; i<5; ++i)
            {
                var humanAgent = Instantiate(AIAgent, BlueTeamtartingPoint, Quaternion.identity);
                humanAgent.GetComponent<PlayerConfiguration>().Team = BlueTeam;
                BlueTeam.AddPlayer(humanAgent.GetComponent<AIAgent>());
                BlueTeamtartingPoint = new Vector3(BlueTeamtartingPoint.x + 5, BlueTeamtartingPoint.y, BlueTeamtartingPoint.z);
            }

            // spawn red team players
            var RedTeamtartingPoint = GameObject.Find("RedTeamPosition").transform.position;
            for (int i = 0; i < 5; ++i)
            {
                var aiAgent = Instantiate(AIAgent, RedTeamtartingPoint, Quaternion.Euler(0,180,0));
                aiAgent.GetComponent<PlayerConfiguration>().Team = RedTeam;
                RedTeam.AddPlayer(aiAgent.GetComponent<AIAgent>());
                RedTeamtartingPoint = new Vector3(RedTeamtartingPoint.x + 5, RedTeamtartingPoint.y, RedTeamtartingPoint.z);
            }
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
