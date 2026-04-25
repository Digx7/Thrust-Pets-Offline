using UnityEngine;
using System.Collections;

public class InvincibilityRunTime : PowerUpRuntime 
{
    
    [Range(0.1f, 4f)]
    public float invincibleDuration = 0.9f;

    protected override void Start()
    {
        base.Start();

        // Make invincible

        Debug.Log("Invincible Powerup Started");
        StartCoroutine(PowerUpMain());
    }

    IEnumerator PowerUpMain()
    {
        PlayerCharacter_EndlessRunner playerCharacter = GetComponentInParent<PlayerCharacter_EndlessRunner>();

        playerCharacter.IsInvincible = true;

        yield return new WaitForSeconds(invincibleDuration);

        playerCharacter.IsInvincible = false;
    }
}