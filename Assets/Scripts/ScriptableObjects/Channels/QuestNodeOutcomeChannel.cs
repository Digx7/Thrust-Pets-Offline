using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewQuestNodeOutcomeDataChannel", menuName = "ScriptableObjects/Channels/QuestNodeOutcome", order = 1)]
    public class QuestNodeOutcomeChannel : ScriptableObject
    {

        public bool debug = true;
        public QuestNodeOutcomeEvent channelEvent = new QuestNodeOutcomeEvent();
    
        public QuestNodeOutcome lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = new QuestNodeOutcome();
        }

        public void Raise(QuestNodeOutcome value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}