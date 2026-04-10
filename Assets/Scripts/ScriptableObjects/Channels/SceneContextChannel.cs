using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewSceneContextDataChannel", menuName = "ScriptableObjects/Channels/SceneContext", order = 1)]
    public class SceneContextChannel : ScriptableObject
    {

        public bool debug = true;
        public SceneContextEvent channelEvent = new SceneContextEvent();
    
        public SceneContext lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = new SceneContext();
        }

        public void Raise(SceneContext value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}