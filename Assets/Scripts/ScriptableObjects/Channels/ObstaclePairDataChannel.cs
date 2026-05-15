using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewObstaclePairDataChannel", menuName = "ScriptableObjects/Channels/ObstaclePairData", order = 1)]
    public class ObstaclePairDataChannel : ScriptableObject
    {
        #region Variables ==============================================

        public bool debug = true;
        public ObstaclePairDataEvent channelEvent = new ObstaclePairDataEvent();
    
        public ObstaclePairData lastValue { get; private set; }
        #endregion

        #region Setup ==============================================
    
        private void OnEnable()
        {
            ResetLastValue();
        }
    
        public void ResetLastValue()
        {
            lastValue = null;
        }

        #endregion

        #region Main Functions ==============================================

        public void Raise(ObstaclePairData value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }

        #endregion    
    }
}