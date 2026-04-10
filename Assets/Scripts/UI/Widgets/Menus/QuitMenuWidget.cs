using UnityEngine;

namespace Digx7.Zygote
{
    public class QuitMenuWidget : UIMenu
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

        public void OnClickNo()
        {
            requestLoadUIWidgetEvent?.Invoke(mainMenuWidgetData);
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);
        }

        public void OnClickYes()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        #endregion
    }
}
