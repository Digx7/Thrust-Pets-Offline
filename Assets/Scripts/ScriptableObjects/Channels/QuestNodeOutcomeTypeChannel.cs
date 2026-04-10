using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewQuestNodeOutcomeTypeDataChannel", menuName = "ScriptableObjects/Channels/QuestNodeOutcomeType", order = 1)]
    public class QuestNodeOutcomeTypeChannel : ScriptableObject
    {

        public bool debug = true;
        public QuestNodeOutcomeTypeEvent channelEvent = new QuestNodeOutcomeTypeEvent();
    
        public QuestNodeOutcomeType lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = 0;
        }

        public void Raise(QuestNodeOutcomeType value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}