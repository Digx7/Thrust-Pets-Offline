using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class SFXManager : Singleton<SFXManager>
    {
        #region Variables ================================
        
        // [Header("Variables")]

        // [Header("Incoming Channels")]

        // [Header("Outgoing Events")]

        #endregion

        #region Setup ================================

        public override void SafeOnEnable()
        {
            SetupChannels();
        }

        public override void SafeOnDisable()
        {
            TearDownChannels();
        }

        private void SetupChannels()
        {
            
        }

        private void TearDownChannels()
        {
            
        }

        #endregion

        #region Channel Responses ================================

        

        #endregion

        #region Main Functions ================================

        

        #endregion
    }
}