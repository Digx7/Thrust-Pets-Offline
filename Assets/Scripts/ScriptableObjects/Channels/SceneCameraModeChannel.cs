using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewSceneCameraModeDataChannel", menuName = "ScriptableObjects/Channels/SceneCameraMode", order = 1)]
    public class SceneCameraModeChannel : ScriptableObject
    {

        public bool debug = true;
        public SceneCameraModeEvent channelEvent = new SceneCameraModeEvent();
    
        public SceneCameraMode lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = 0;
        }

        public void Raise(SceneCameraMode value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}