using UnityEngine;

namespace Digx7.Zygote
{
    public class SplashScreenMenuWidget : UIMenu
    {
        #region Variables ================================
        
        [Header("Variables")]
        [SerializeField] UIWidgetData mainMenuWidgetData;
        
        [Header("Outgoing Events")]
        public UIWidgetDataEvent requestLoadUIWidgetEvent;
        public UIWidgetDataEvent requestUnLoadUIWidgetEvent;

        #endregion

        #region Setup ================================

        public override void Setup(UIWidgetData newUIWidgetData)
        {
            base.Setup(newUIWidgetData);
        }

        public override void Teardown()
        {
            base.Teardown();
        }

        #endregion

        #region Main Functions ================================

        public void OnClickStart()
        {
            requestLoadUIWidgetEvent?.Invoke(mainMenuWidgetData);
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);
        }

        #endregion
    }
}