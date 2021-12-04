using System;
public class AIAgent : AI
{
    public new void Start()
    {
        gameObject.name = $"Agent{Configuration.GenerateId()}";
        base.Start();
    }

    public new void Update()
    {
        base.Update();
    }
}
