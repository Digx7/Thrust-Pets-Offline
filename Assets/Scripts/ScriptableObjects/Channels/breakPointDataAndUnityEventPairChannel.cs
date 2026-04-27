using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewbreakPointDataAndUnityEventPairDataChannel", menuName = "ScriptableObjects/Channels/breakPointDataAndUnityEventPair", order = 1)]
    public class breakPointDataAndUnityEventPairChannel : ScriptableObject
    {

        public bool debug = true;
        public breakPointDataAndUnityEventPairEvent channelEvent = new breakPointDataAndUnityEventPairEvent();
    
        public breakPointDataAndUnityEventPair lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = new breakPointDataAndUnityEventPair();
        }

        public void Raise(breakPointDataAndUnityEventPair value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}