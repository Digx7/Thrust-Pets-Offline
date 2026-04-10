using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewFloatChannel", menuName = "ScriptableObjects/Channels/Float", order = 1)]
    public class FloatChannel : ScriptableObject
    {

        public bool debug = true;
        public FloatEvent channelEvent = new FloatEvent();
    
        public float lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = 0f;
        }

        public void Raise(float value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}