using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewGameEndConditionDataChannel", menuName = "ScriptableObjects/Channels/GameEndCondition", order = 1)]
    public class GameEndConditionChannel : ScriptableObject
    {

        public bool debug = true;
        public GameEndConditionEvent channelEvent = new GameEndConditionEvent();
    
        public GameEndCondition lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = 0;
        }

        public void Raise(GameEndCondition value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}