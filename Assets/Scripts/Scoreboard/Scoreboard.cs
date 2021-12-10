using System;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public Text text;

    public void Start()
    {
        try
        {
            var redTeam = Constants.RedTeam;
            var blueTeam = Constants.BlueTeam;
            text.text = $"High score winners - {(redTeam.GetScore() > blueTeam.GetScore() ? "Red Team" : "Blue Team")} \n \n";
            text.text += "RED TEAM \n";
            foreach (var playerObj in redTeam.PlayerScores)
            {
                text.text += $"{playerObj.Key} : {playerObj.Value.ToString()} \n";
            }

        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

}
