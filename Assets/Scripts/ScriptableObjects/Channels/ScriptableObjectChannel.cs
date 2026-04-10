using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewScriptableObjectChannel", menuName = "ScriptableObjects/Channels/ScriptableObject", order = 1)]
    public class ScriptableObjectChannel : ScriptableObject
    {

        public bool debug = true;
        public ScriptableObjectEvent channelEvent = new ScriptableObjectEvent();
    
        public ScriptableObject lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = null;
        }

        public void Raise(ScriptableObject value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}