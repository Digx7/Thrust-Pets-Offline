using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewStringChannel", menuName = "ScriptableObjects/Channels/String", order = 1)]
    public class StringChannel : ScriptableObject
    {

        public bool debug = true;
        public StringEvent channelEvent = new StringEvent();
    
        public string lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = null;
        }

        public void Raise(string value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}