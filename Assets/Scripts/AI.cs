using System;
using UnityEngine;

public class AI : Player
{
    public new void Start()
    {
        base.Start();
        gameObject.name = Enum.GetName(typeof(PLAYER_TYPE), PLAYER_TYPE.AI);
    }

    public new void Update()
    {
        base.Update();
        // IMPLEMENT ME - MAK
    }
}
