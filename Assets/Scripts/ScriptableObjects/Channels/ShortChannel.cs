using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewShortChannel", menuName = "ScriptableObjects/Channels/Short", order = 1)]
    public class ShortChannel : ScriptableObject
    {

        public bool debug = true;
        public ShortEvent channelEvent = new ShortEvent();
    
        public short lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = 0;
        }

        public void Raise(short value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}