using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.ThrustPets
{
    [CreateAssetMenu(fileName = "NewItemUnlockTypeDataChannel", menuName = "ScriptableObjects/Channels/ItemUnlockType", order = 1)]
    public class ItemUnlockTypeChannel : ScriptableObject
    {

        public bool debug = true;
        public ItemUnlockTypeEvent channelEvent = new ItemUnlockTypeEvent();
    
        public ItemUnlockType lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = 0;
        }

        public void Raise(ItemUnlockType value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}