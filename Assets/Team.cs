using System;
using System.Collections.Generic;

public class Team
{
    private TEAM_COLOR TeamColor { get; set; }
    private int Ceiling { get; set; }
    private List<Player> Players { get; set; }
    private long Score { get; set; }

    public Team(int ceiling, TEAM_COLOR teamColor)
    {
        this.Ceiling = ceiling;
        this.TeamColor = teamColor;
    }

    public bool AddPlayer(Player player) // Return false if the list has reached the cap size.
    {
        // IMPLEMENT ME
        return true;
    }

    public void UpdateTeamScore (long score)
    {
        // IMPLEMENT ME
    }
}
