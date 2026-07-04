using UnityEngine;
using UnityEngine.Events;
using Digx7.Zygote;
using Digx7.ThrustPets;
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

        [SerializeField] Sprite _menuPhoto;
        public Sprite MenuPhoto => _menuPhoto;

        [SerializeField] string _menuDescription;
        public string MenuDescription => _menuDescription;
        [SerializeField] GameObject _runtimePrefab;
        public GameObject RuntimePrefab => _runtimePrefab;

        [SerializeField] float _cooldownTime;
        public float CooldownTime => _cooldownTime;

        private float _lastUsedTime;

        [Header("")]
        public BooleanEvent OnTryToUse;
        public UnityEvent OnSuccessfullyUse;
        public UnityEvent OnFailToUse;

        private void OnEnable() 
        {
            _lastUsedTime = -_cooldownTime;
        }

        public bool TryUse(out GameObject runtimePrefab)
        {
            if(TryUse())
            {
                runtimePrefab = RuntimePrefab;
                return true;
            }
            else
            {
                runtimePrefab = null;
                return false;
            }
        }

        public bool TryUse()
        {
            
            if(Time.time - _lastUsedTime >= _cooldownTime)
            {
                _lastUsedTime = Time.time;
                OnTryToUse.Invoke(true);
                OnSuccessfullyUse.Invoke();
                return true;
            }
            else
            {
                OnTryToUse.Invoke(false);
                OnFailToUse.Invoke();
                return false;
            }
        }
    }
}