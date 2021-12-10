using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Team
{
    public Color TeamColor { get; set; }
    private int Ceiling { get; set; }
    private List<Player> Players { get; set; }
    private long Score { get; set; }
    public string TeamName { get; set; }
    public Dictionary<string, long> PlayerScores { get; set; }
    public Team(int ceiling, Color teamColor, string TeamName)
    {
        this.Ceiling = ceiling;
        this.TeamColor = teamColor;
        this.TeamName = TeamName;
        Players = new List<Player>(ceiling);
        PlayerScores = new Dictionary<string, long>(ceiling);
    }

    public List<Player> GetPlayers()
    {
        return Players;
    }

    public bool AddPlayer(Player player) // Return false if the list has reached the cap size.
    {
        try
        {
            Players.Add(player);
            PlayerScores.Add(player.gameObject.name, 0);
            return true;
        } catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public long GetScore()
    {
        return Score;
    }

    public void UpdateTeamScore (Player player, long score, long updatedScore)
    {
        Debug.Log("Updating score");
        PlayerScores[player.gameObject.name] = updatedScore;
        Score += score;
    }
}
