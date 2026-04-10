using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewConversationChannel", menuName = "ScriptableObjects/Channels/Conversation", order = 1)]
    public class ConversationChannel : ScriptableObject
    {

        public bool debug = true;
        public ConversationEvent channelEvent = new ConversationEvent();
    
        public Conversation lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = null;
        }

        public void Raise(Conversation value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}