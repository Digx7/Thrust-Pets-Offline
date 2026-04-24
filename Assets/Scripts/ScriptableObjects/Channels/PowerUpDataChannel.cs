using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.ThrustPets
{
    [CreateAssetMenu(fileName = "NewPowerUpDataChannel", menuName = "ScriptableObjects/Channels/PowerUpData", order = 1)]
    public class PowerUpDataChannel : ScriptableObject
    {
        #region Variables ==============================================

        public bool debug = true;
        public PowerUpDataEvent channelEvent = new PowerUpDataEvent();
    
        public PowerUpData lastValue { get; private set; }
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

        public void Raise(PowerUpData value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }

        #endregion    
    }
}