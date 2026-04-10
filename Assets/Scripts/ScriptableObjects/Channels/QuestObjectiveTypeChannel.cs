using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewQuestObjectiveTypeDataChannel", menuName = "ScriptableObjects/Channels/QuestObjectiveType", order = 1)]
    public class QuestObjectiveTypeChannel : ScriptableObject
    {

        public bool debug = true;
        public QuestObjectiveTypeEvent channelEvent = new QuestObjectiveTypeEvent();
    
        public QuestObjectiveType lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = 0;
        }

        public void Raise(QuestObjectiveType value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}