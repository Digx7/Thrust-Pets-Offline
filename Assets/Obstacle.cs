using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleType type;

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
        // if(type == ObstacleType.SpiderWeb) 
        // {
        //     Player player = other.transform.root.GetComponent<Player>();

        //     if (player != null)
        //     {
        //         if (player.hasAuthority == true)
        //         {
        //             player.Stuck();
        //         }
        //     }
        // }
        // else 
        // {
        //     Player player = other.transform.root.GetComponent<Player>();

        //     if (player != null)
        //     {
        //         if (player.hasAuthority == true)
        //         {
        //             player.TakeDamage();
        //         }
        //     }
        // }
    }
}

[System.Serializable]
public enum ObstacleType 
{
    Simple,
    SpiderWeb
}