using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewQuestNodeChannel", menuName = "ScriptableObjects/Channels/QuestNode", order = 1)]
    public class QuestNodeChannel : ScriptableObject
    {

        public bool debug = true;
        public QuestNodeEvent channelEvent = new QuestNodeEvent();
    
        public QuestNode lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = null;
        }

        public void Raise(QuestNode value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}