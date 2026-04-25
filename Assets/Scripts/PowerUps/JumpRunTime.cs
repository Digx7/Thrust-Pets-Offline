using UnityEngine;
using System.Collections;

public class JumpRunTime : PowerUpRuntime 
{
    
    protected override void Start()
    {
        base.Start();

        // Jump player

        Debug.Log("Jump Powerup Started");
        StartCoroutine(PowerUpMain());
    }

    IEnumerator PowerUpMain()
    {
        LaneMovement laneMovement = GetComponentInParent<LaneMovement>();

        laneMovement.TryJump();

        yield return null;
        // new
    }
}