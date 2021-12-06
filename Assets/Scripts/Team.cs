using System;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public Color TeamColor { get; set; }
    private int Ceiling { get; set; }
    private List<Player> Players { get; set; }
    private long Score { get; set; }

    public Team(int ceiling, Color teamColor)
    {
        this.Ceiling = ceiling;
        this.TeamColor = teamColor;
        Players = new List<Player>(ceiling);
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

    public void UpdateTeamScore (long score)
    {
        Debug.Log("Updating score");
        Score += score;
    }
}
