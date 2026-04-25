using UnityEngine;

public class BoostRunTime : PowerUpRuntime 
{
    

    protected override void Start()
    {
        base.Start();

        // Give boost

        Debug.Log("Boost Powerup Started");
    }
}