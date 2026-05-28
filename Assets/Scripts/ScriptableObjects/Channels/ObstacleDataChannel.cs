using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.ThrustPets
{
    [CreateAssetMenu(fileName = "NewObstacleDataChannel", menuName = "ScriptableObjects/Channels/ObstacleData", order = 1)]
    public class ObstacleDataChannel : ScriptableObject
    {
        #region Variables ==============================================

        public bool debug = true;
        public ObstacleDataEvent channelEvent = new ObstacleDataEvent();
    
        public ObstacleData lastValue { get; private set; }
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

        public void Raise(ObstacleData value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }

        #endregion    
    }
}