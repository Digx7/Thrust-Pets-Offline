using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewTrippleObstaclePairDataChannel", menuName = "ScriptableObjects/Channels/TrippleObstaclePair", order = 1)]
    public class TrippleObstaclePairChannel : ScriptableObject
    {

        public bool debug = true;
        public TrippleObstaclePairEvent channelEvent = new TrippleObstaclePairEvent();
    
        public TrippleObstaclePair lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = new TrippleObstaclePair();
        }

        public void Raise(TrippleObstaclePair value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}