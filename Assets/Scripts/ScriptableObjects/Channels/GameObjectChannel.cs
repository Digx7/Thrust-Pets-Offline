using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewGameObjectChannel", menuName = "ScriptableObjects/Channels/GameObject", order = 1)]
    public class GameObjectChannel : ScriptableObject
    {

        public bool debug = true;
        public GameObjectEvent channelEvent = new GameObjectEvent();
    
        public GameObject lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = null;
        }

        public void Raise(GameObject value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}