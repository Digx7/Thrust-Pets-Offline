using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewGameEndResultDataChannel", menuName = "ScriptableObjects/Channels/GameEndResult", order = 1)]
    public class GameEndResultChannel : ScriptableObject
    {

        public bool debug = true;
        public GameEndResultEvent channelEvent = new GameEndResultEvent();
    
        public GameEndResult lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = new GameEndResult();
        }

        public void Raise(GameEndResult value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}