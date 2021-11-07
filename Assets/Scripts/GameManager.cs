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

        BlueTeam = new Team(1, TEAM_COLOR.BLUE);
        RedTeam = new Team(1, TEAM_COLOR.RED);
        GameObject HumanObject = Instantiate(Player, new Vector3(133, -4, 26), Quaternion.identity);
        GameObject AIObject = Instantiate(Player, new Vector3(133, -4, 226), Quaternion.identity);
        HumanObject.GetComponent<Human>().Team = RedTeam;
        Destroy(AIObject.GetComponent<Human>());
        Destroy(AIObject.GetComponentInChildren<Camera>());
        AIObject.AddComponent<AI>();
        AIObject.GetComponent<AI>().Team = BlueTeam;
        RedTeam.AddPlayer(HumanObject.GetComponent<Human>());
    }
}
