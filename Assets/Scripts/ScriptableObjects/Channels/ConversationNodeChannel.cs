using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewConversationNodeDataChannel", menuName = "ScriptableObjects/Channels/ConversationNode", order = 1)]
    public class ConversationNodeChannel : ScriptableObject
    {

        public bool debug = true;
        public ConversationNodeEvent channelEvent = new ConversationNodeEvent();
    
        public ConversationNode lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = new ConversationNode();
        }

        public void Raise(ConversationNode value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}