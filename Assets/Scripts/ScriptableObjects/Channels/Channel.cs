using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewChannel", menuName = "ScriptableObjects/Channels/Default", order = 1)]
    public class Channel : ScriptableObject
    {

        public bool debug = true;
        public UnityEvent channelEvent = new UnityEvent();

        public void Raise()
        {
            if (debug) Debug.Log("Raised Channel: " + this.name);
        
            channelEvent.Invoke();
        }    
    }
}