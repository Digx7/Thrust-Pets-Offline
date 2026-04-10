using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewSongDataChannel", menuName = "ScriptableObjects/Channels/SongData", order = 1)]
    public class SongDataChannel : ScriptableObject
    {

        public bool debug = true;
        public SongDataEvent channelEvent = new SongDataEvent();
    
        public SongData lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = null;
        }

        public void Raise(SongData value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}