using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewStringAndGameObjectDataChannel", menuName = "ScriptableObjects/Channels/StringAndGameObject", order = 1)]
    public class StringAndGameObjectChannel : ScriptableObject
    {

        public bool debug = true;
        public StringAndGameObjectEvent channelEvent = new StringAndGameObjectEvent();
    
        public StringAndGameObject lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = new StringAndGameObject();
        }

        public void Raise(StringAndGameObject value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}