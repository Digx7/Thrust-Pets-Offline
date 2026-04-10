using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewLongChannel", menuName = "ScriptableObjects/Channels/Long", order = 1)]
    public class LongChannel : ScriptableObject
    {

        public bool debug = true;
        public LongEvent channelEvent = new LongEvent();
    
        public long lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = 0;
        }

        public void Raise(long value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}