using UnityEngine;
using Digx7.Zygote;
using Digx7.ThrustPets;

public abstract class PowerUpRuntime : MonoBehaviour 
{
    public float lifeTime = 1f;

    protected virtual void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }
}