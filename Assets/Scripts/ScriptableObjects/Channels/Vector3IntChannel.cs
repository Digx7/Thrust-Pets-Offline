using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewVector3IntChannel", menuName = "ScriptableObjects/Channels/Vector3Int", order = 1)]
    public class Vector3IntChannel : ScriptableObject
    {

        public bool debug = true;
        public Vector3IntEvent channelEvent = new Vector3IntEvent();
    
        public Vector3Int lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = Vector3Int.zero;
        }

        public void Raise(Vector3Int value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}