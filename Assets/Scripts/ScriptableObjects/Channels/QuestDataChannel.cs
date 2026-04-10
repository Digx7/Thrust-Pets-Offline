using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewQuestDataChannel", menuName = "ScriptableObjects/Channels/QuestData", order = 1)]
    public class QuestDataChannel : ScriptableObject
    {

        public bool debug = true;
        public QuestDataEvent channelEvent = new QuestDataEvent();
    
        public QuestData lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = null;
        }

        public void Raise(QuestData value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}