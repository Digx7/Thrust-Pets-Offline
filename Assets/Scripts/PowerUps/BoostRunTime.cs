using UnityEngine;
using System.Collections;

public class BoostRunTime : PowerUpRuntime 
{
    [Range(0.1f, 4f)]
    public float boostDuration = 0.5f;
    public float boostMultiplier = 3f;

    protected override void Start()
    {
        base.Start();

        // Give boost

        Debug.Log("Boost Powerup Started");
        StartCoroutine(PowerUpMain());
    }

    IEnumerator PowerUpMain()
    {
        LaneMovement laneMovement = GetComponentInParent<LaneMovement>();

        float ogSpeed = laneMovement.forwardSpeed;

        laneMovement.forwardSpeed = ogSpeed * boostMultiplier;

        yield return new WaitForSeconds(boostDuration);

        laneMovement.forwardSpeed = ogSpeed;
    }
}