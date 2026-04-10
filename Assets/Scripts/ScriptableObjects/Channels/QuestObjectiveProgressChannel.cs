using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewQuestObjectiveProgressDataChannel", menuName = "ScriptableObjects/Channels/QuestObjectiveProgress", order = 1)]
    public class QuestObjectiveProgressChannel : ScriptableObject
    {

        public bool debug = true;
        public QuestObjectiveProgressEvent channelEvent = new QuestObjectiveProgressEvent();
    
        public QuestObjectiveProgress lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = new QuestObjectiveProgress();
        }

        public void Raise(QuestObjectiveProgress value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}