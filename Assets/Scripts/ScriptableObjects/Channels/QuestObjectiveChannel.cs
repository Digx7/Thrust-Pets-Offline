using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewQuestObjectiveChannel", menuName = "ScriptableObjects/Channels/QuestObjective", order = 1)]
    public class QuestObjectiveChannel : ScriptableObject
    {

        public bool debug = true;
        public QuestObjectiveEvent channelEvent = new QuestObjectiveEvent();
    
        public QuestObjective lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = null;
        }

        public void Raise(QuestObjective value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}