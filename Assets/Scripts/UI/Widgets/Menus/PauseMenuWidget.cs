using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class PauseMenuWidget : UIMenu
    {
        #region Variables ================================
        
        [Header("Variables")]
        [SerializeField] private SceneData mainMenuScene;
        [SerializeField] UIWidgetData optionsMenuWidgetData;

        // [Header("Incoming Channels")]
        [Header("Outgoing Events")]
        public SceneDataEvent requestChangeSceneDataEvent;
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

        public void OnClickResume()
        {
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);
        }

        public void OnClickOptions()
        {
            requestLoadUIWidgetEvent?.Invoke(optionsMenuWidgetData);
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);
        }

        public void OnClickQuit()
        {
            requestChangeSceneDataEvent?.Invoke(mainMenuScene);
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);
        }

        #endregion
    }
}
