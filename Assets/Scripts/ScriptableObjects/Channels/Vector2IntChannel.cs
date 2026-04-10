using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewVector2IntChannel", menuName = "ScriptableObjects/Channels/Vector2Int", order = 1)]
    public class Vector2IntChannel : ScriptableObject
    {

        public bool debug = true;
        public Vector2IntEvent channelEvent = new Vector2IntEvent();
    
        public Vector2Int lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = Vector2Int.zero;
        }

        public void Raise(Vector2Int value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}