using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewObstaclesStateDataChannel", menuName = "ScriptableObjects/Channels/ObstaclesState", order = 1)]
    public class ObstaclesStateChannel : ScriptableObject
    {

        public bool debug = true;
        public ObstaclesStateEvent channelEvent = new ObstaclesStateEvent();
    
        public ObstaclesState lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = new ObstaclesState();
        }

        public void Raise(ObstaclesState value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}