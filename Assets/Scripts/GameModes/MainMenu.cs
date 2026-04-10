using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class MainMenu : GameMode
    {
        #region Variables ================================
        
        [Header("Variables")]
        [SerializeField] private UIWidgetData mainMenuWidgetData;
        [SerializeField] private UIWidgetData splashScreenWidgetData;

        // [Header("Incoming Channels")]
        
        [Header("Outgoing Events")]
        public UIWidgetDataEvent OnRequestLoadUIWidgetDataEvent;
        public UnityEvent OnRequestClearAllUIWidgetEvent;

        #endregion

        #region Setup ================================
        public override void Setup()
        {
            // add code here
            OnRequestClearAllUIWidgetEvent?.Invoke();
            OnRequestLoadUIWidgetDataEvent?.Invoke(splashScreenWidgetData);
            base.Setup();
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
            OnRequestLoadUIWidgetDataEvent?.Invoke(mainMenuWidgetData);
        }
        #endregion
        
        #region Main Functions ================================

        #endregion


    }
}