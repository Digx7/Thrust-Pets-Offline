using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleType type;
    public bool canSlideUnder = false;

    public GameObject destroyEffect;
    public GameObject parentObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(type == ObstacleType.SpiderWeb)
        {
            PlayerCharacter_EndlessRunner player = other.transform.root.GetComponent<PlayerCharacter_EndlessRunner>();

            if (player != null)
            {
                player.Stuck();
            }
        }
        else
        {
            PlayerCharacter_EndlessRunner player = other.transform.root.GetComponent<PlayerCharacter_EndlessRunner>();

            if (player != null)
            {
                if(canSlideUnder)
                {
                    LaneMovement laneMovement = player.gameObject.GetComponent<LaneMovement>();
                    if(!laneMovement.IsSliding)
                    {
                       player.TakeDamage(); 
                       DestroyObstacle();
                    }    
                }
                else
                {
                    player.TakeDamage();
                    DestroyObstacle();
                }
                
            }
        }
    }

    void DestroyObstacle()
    {
        if(parentObject == null)
        {
            Debug.LogWarning("Obstacle " + gameObject.name + " tried to destroy itself but has no parent object.\nMake sure to assign the parent object in the inspector.");
            return;
        }
        
        if(destroyEffect != null)
        {
            Instantiate(destroyEffect, parentObject.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Obstacle " + gameObject.name + " tried to instantiate a destroy effect but has no destroy effect assigned.\nMake sure to assign the destroy effect in the inspector.");
        }
        
        Destroy(parentObject);
    }
}

[System.Serializable]
public enum ObstacleType 
{
    Simple,
    SpiderWeb
}