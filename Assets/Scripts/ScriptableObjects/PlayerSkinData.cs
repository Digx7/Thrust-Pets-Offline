using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.ThrustPets
{
    [CreateAssetMenu(fileName = "NewPlayerSkinData", menuName = "ScriptableObjects/Data/PlayerSkinData", order = 1)]
    public class PlayerSkinData: ScriptableObject
    {
        [SerializeField] string _displayName;
        public string DisplayName => _displayName;
        [SerializeField] int _itemID;
        public int ItemID => _itemID;
        [SerializeField] ItemUnlockType _unlockType = ItemUnlockType.InAppPurchase;
        public ItemUnlockType UnlockType => _unlockType;
        [SerializeField] Sprite _menuImage;
        public Sprite MenuImage => _menuImage;
        [SerializeField] GameObject _runtimePrefab;
        public GameObject RuntimePrefab => _runtimePrefab;
    }
}