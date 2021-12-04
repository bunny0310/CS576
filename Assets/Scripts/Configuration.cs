using System;
using System.Collections.Generic;

public static class Configuration
{
    private static HashSet<int> IdsInUse { get; set; }
    private static readonly Random _random = new Random();
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
}
