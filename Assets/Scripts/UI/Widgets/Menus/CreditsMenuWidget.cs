using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class CreditsMenuWidget : UIMenu
    {
        #region Variables ================================
        [Header("Variables")]
        [SerializeField] UIWidgetData mainMenuWidgetData;
        
        // [Header("Incoming Channels")]
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

        public void OnClickBack()
        {
            requestLoadUIWidgetEvent?.Invoke(mainMenuWidgetData);
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);
        }

        #endregion
    }
}
