using System;
using System.Collections.Generic;
using UnityEngine;

public static class Configuration
{
    private static HashSet<int> IdsInUse { get; set; }
    private static readonly System.Random _random = new System.Random();
    public static int GenerateId()
    {
        IdsInUse = new HashSet<int>();
        int id;
        do
        {
            id = _random.Next(100);
        } while (IdsInUse.Contains(id));
        IdsInUse.Add(id);
        return id;
    }

    public static bool IsPlayer(GameObject obj)
    {
        return obj.GetComponent<PlayerConfiguration>();
    }

    public static Player RetreivePlayer(GameObject obj)
    {
        if (!IsPlayer(obj))
        {
            return null;
        }
        Player player = null;
        player = obj.GetComponent<AIAgent>();
        if (player == null)
        {
            player = obj.GetComponent<Human>();
        }
        return player;
    }
}
