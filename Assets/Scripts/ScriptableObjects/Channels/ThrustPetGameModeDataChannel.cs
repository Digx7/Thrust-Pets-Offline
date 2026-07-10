using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.ThrustPets
{
    [CreateAssetMenu(fileName = "NewThrustPetGameModeDataChannel", menuName = "ScriptableObjects/Channels/ThrustPetGameModeData", order = 1)]
    public class ThrustPetGameModeDataChannel : ScriptableObject
    {
        #region Variables ==============================================

        public bool debug = true;
        public ThrustPetGameModeDataEvent channelEvent = new ThrustPetGameModeDataEvent();
    
        public ThrustPetGameModeData lastValue { get; private set; }
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

        public void Raise(ThrustPetGameModeData value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }

        #endregion    
    }
}