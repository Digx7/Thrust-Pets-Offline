using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.ThrustPets
{
    [CreateAssetMenu(fileName = "NewObstacleData", menuName = "ScriptableObjects/Data/ObstacleData", order = 1)]
    public class ObstacleData: ScriptableObject
    {
        [SerializeField] GameObject _prefab;
        public GameObject Prefab => _prefab;

        [Header("MetaData")]
        [SerializeField] float _length = 2f;
        public float Length => _length;

        [SerializeField] int _numberOfLanesOcupied = 1;
        public int NumberOfLanesOcupied => _numberOfLanesOcupied;
    }
}