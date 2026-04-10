using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewObjectChannel", menuName = "ScriptableObjects/Channels/Object", order = 1)]
    public class ObjectChannel : ScriptableObject
    {

        public bool debug = true;
        public ObjectEvent channelEvent = new ObjectEvent();
    
        public Object lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = null;
        }

        public void Raise(Object value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}