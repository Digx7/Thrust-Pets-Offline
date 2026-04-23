using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.ThrustPets
{
    [CreateAssetMenu(fileName = "NewPlayerSkinDataChannel", menuName = "ScriptableObjects/Channels/PlayerSkinData", order = 1)]
    public class PlayerSkinDataChannel : ScriptableObject
    {
        #region Variables ==============================================

        public bool debug = true;
        public PlayerSkinDataEvent channelEvent = new PlayerSkinDataEvent();
    
        public PlayerSkinData lastValue { get; private set; }
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

        public void Raise(PlayerSkinData value)
        {
            if (debug) Debug.Log("Raised Channel: " + this.name + " with value " + value);
        
            lastValue = value;
            channelEvent.Invoke(value);
        }

        #endregion    
    }
}