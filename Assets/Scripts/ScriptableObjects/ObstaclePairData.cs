using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Digx7.ThrustPets;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewObstaclePairData", menuName = "ScriptableObjects/Data/ObstaclePairData", order = 1)]
    public class ObstaclePairData: ScriptableObject
    {
        public GameObject obstaclePrefab1;
        public GameObject obstaclePrefab2;
        public GameObject obstaclePrefab3;

        public ObstacleData obstacleData1;
        public ObstacleData obstacleData2;
        public ObstacleData obstacleData3;
    }
}