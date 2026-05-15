using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewObstaclePairData", menuName = "ScriptableObjects/Data/ObstaclePairData", order = 1)]
    public class ObstaclePairData: ScriptableObject
    {
        public GameObject obstaclePrefab1;
        public GameObject obstaclePrefab2;
        public GameObject obstaclePrefab3;
    }
}