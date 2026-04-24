using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.ThrustPets
{
    [CreateAssetMenu(fileName = "NewPowerUpData", menuName = "ScriptableObjects/Data/PowerUpData", order = 1)]
    public class PowerUpData: ScriptableObject
    {
        [SerializeField] string _displayName;
        public string DisplayName => _displayName;
        [SerializeField] int _itemID;
        public int ItemID => _itemID;
        [SerializeField] Sprite _menuImage;
        public Sprite MenuImage => _menuImage;
        [SerializeField] GameObject _runtimePrefab;
        public GameObject RuntimePrefab => _runtimePrefab;
    }
}