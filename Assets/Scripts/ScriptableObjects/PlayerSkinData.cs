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

        [SerializeField] Sprite _photoImage;
        public Sprite PhotoImage => _photoImage;
        [TextArea(3, 10)]
        [SerializeField] string _menuDescription;
        public string MenuDescription => _menuDescription;
        [SerializeField] List<AnimalFunFact> _funFacts;
        public AnimalFunFact GetRandomFunFact()
        {
            if (_funFacts.Count == 0)
                return null;
            int index = Random.Range(0, _funFacts.Count);
            return _funFacts[index];
        }
        [SerializeField] GameObject _runtimePrefab;
        public GameObject RuntimePrefab => _runtimePrefab;
    }

    [System.Serializable]
    public class AnimalFunFact
    {
        [TextArea(3, 10)]
        [SerializeField] string _fact;
        public string Fact => _fact;
    }
}