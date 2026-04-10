using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class GamePlay : GameMode
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] private UIWidgetData _pauseMenuWidgetData;
        
        // [Header("Incoming Channels")]

        [Header("Outgoing Events")]
        public UIWidgetDataEvent OnRequestLoadUIWidgetDataEvent;
        public UnityEvent OnRequestLoadSaveDataEvent;

        #endregion

        #region Setup ================================

        public override void Setup()
        {
            // add code here
            
            base.Setup();

            OnRequestLoadSaveDataEvent?.Invoke();
        }

        public override void Teardown()
        {
            // add code here
            
            base.Teardown();
        }

        #endregion

        #region Channel Responses ================================
        protected override void OnRecieve_OnOptionsMenuQuit()
        {
            OnRequestLoadUIWidgetDataEvent?.Invoke(_pauseMenuWidgetData);
        }

        #endregion

        #region Main Functions ================================

        #endregion
    }
}