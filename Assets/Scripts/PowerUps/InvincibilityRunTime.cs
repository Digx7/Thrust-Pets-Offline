using UnityEngine;

public class InvincibilityRunTime : PowerUpRuntime 
{
    
    protected override void Start()
    {
        base.Start();

        // Make invincible

        Debug.Log("Invincible Powerup Started");
    }
}