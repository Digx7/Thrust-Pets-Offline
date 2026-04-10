using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class MainMenuWidget : UIMenu
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] SceneData gamePlayScene;
        [SerializeField] UIWidgetData optionsMenuWidgetData;
        [SerializeField] UIWidgetData creditsMenuWidgetData;
        [SerializeField] UIWidgetData quitMenuWidgetData;
        
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

        public void OnClickPlay()
        {
            StartCoroutine(Delay(PlayButton, 0.1f));
        }
        
        private void PlayButton()
        {
            requestChangeSceneDataEvent?.Invoke(gamePlayScene);
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);
        }

        public void OnClickOptions()
        {
            StartCoroutine(Delay(OptionsButton, 0.1f));
        }

        private void OptionsButton()
        {
            requestLoadUIWidgetEvent?.Invoke(optionsMenuWidgetData);
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);
        }

        public void OnClickCredits()
        {
            StartCoroutine(Delay(CreditsButton, 0.1f));
        }

        private void CreditsButton()
        {
            requestLoadUIWidgetEvent?.Invoke(creditsMenuWidgetData);
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);
        }

        public void OnClickQuit()
        {
            StartCoroutine(Delay(QuitButton, 0.1f));
        }

        private void QuitButton()
        {
            requestLoadUIWidgetEvent?.Invoke(quitMenuWidgetData);
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);
        }

        #endregion
    }
}
