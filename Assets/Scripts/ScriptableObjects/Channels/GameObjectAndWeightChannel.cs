using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewGameObjectAndWeightDataChannel", menuName = "ScriptableObjects/Channels/GameObjectAndWeight", order = 1)]
    public class GameObjectAndWeightChannel : ScriptableObject
    {

        public bool debug = true;
        public GameObjectAndWeightEvent channelEvent = new GameObjectAndWeightEvent();
    
        public GameObjectAndWeight lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = new GameObjectAndWeight();
        }

        public void Raise(GameObjectAndWeight value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}