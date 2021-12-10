
using UnityEngine;

public static class Constants
{
    public static readonly int Pulses = 150;
    public static readonly int Energy = 20;
    public static readonly int ShootingPoints = 200;
    public static Color ShotColor = Color.white;
    public static Team RedTeam = GameObject.Find("GameManager").GetComponent<GameManager>().RedTeam;
    public static Team BlueTeam = GameObject.Find("GameManager").GetComponent<GameManager>().BlueTeam;
}
