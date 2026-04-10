using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewUIWidgetDataChannel", menuName = "ScriptableObjects/Channels/UIWidgetData", order = 1)]
    public class UIWidgetDataChannel : ScriptableObject
    {

        public bool debug = true;
        public UIWidgetDataEvent channelEvent = new UIWidgetDataEvent();
    
        public UIWidgetData lastValue { get; private set; }
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = null;
        }

        public void Raise(UIWidgetData value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }    
    }
}