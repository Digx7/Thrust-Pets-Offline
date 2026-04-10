using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewSceneDataDataChannel", menuName = "ScriptableObjects/Channels/SceneData", order = 1)]
    public class SceneDataChannel : ScriptableObject
    {

        public bool debug = true;
        public SceneDataEvent channelEvent = new SceneDataEvent();
    
        public SceneData lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = new SceneData();
        }

        public void Raise(SceneData value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}