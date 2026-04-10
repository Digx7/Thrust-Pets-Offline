using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewPlayerSpawnInfoDataChannel", menuName = "ScriptableObjects/Channels/PlayerSpawnInfo", order = 1)]
    public class PlayerSpawnInfoChannel : ScriptableObject
    {

        public bool debug = true;
        public PlayerSpawnInfoEvent channelEvent = new PlayerSpawnInfoEvent();
    
        public PlayerSpawnInfo lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = new PlayerSpawnInfo();
        }

        public void Raise(PlayerSpawnInfo value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}