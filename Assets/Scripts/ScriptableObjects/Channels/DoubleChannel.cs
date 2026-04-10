using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewDoubleChannel", menuName = "ScriptableObjects/Channels/Double", order = 1)]
    public class DoubleChannel : ScriptableObject
    {

        public bool debug = true;
        public DoubleEvent channelEvent = new DoubleEvent();
    
        public double lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = 0;
        }

        public void Raise(double value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}