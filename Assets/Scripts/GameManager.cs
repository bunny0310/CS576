using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject Player;
    Team BlueTeam;
    Team RedTeam;
    public void Start()
    {
        Player = (GameObject)Resources.Load("Prefabs/Player", typeof(GameObject));  // player prefab
        if (Player == null)
            Debug.LogError("Error: could not find the apple prefab in the project! Did you delete/move the prefab from your project?");

        BlueTeam = new Team(1, Color.blue);
        RedTeam = new Team(1, Color.red);
        GameObject HumanObject = Instantiate(Player, new Vector3(133, -4, 26), Quaternion.identity);
        HumanObject.AddComponent<Human>();
        HumanObject.GetComponent<Human>().Team = BlueTeam;
        GameObject AIObject = Instantiate(Player, new Vector3(133, -4, 200), Quaternion.identity);
        AIObject.AddComponent<AI>();
        AIObject.GetComponent<AI>().Team = RedTeam;
        //GameObject AIObject = Instantiate(Player, new Vector3(133, -4, 226), Quaternion.identity);
        // HumanObject.GetComponent<Human>().VestLightFront.color = Color.blue;
        //Destroy(AIObject.GetComponent<Human>());
        //Destroy(AIObject.GetComponentInChildren<Camera>());
        //AIObject.AddComponent<AI>();
        //AIObject.GetComponent<AI>().Team = RedTeam;
        //AIObject.GetComponent<AI>().ChangeVestColor(RedTeam.TeamColor);
    }
}
