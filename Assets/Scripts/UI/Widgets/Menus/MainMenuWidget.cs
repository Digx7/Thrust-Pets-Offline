using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Digx7.Zygote
{
    public class MainMenuWidget : UIMenu
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] SceneData gameModeScene;
        [SerializeField] SceneData mapScene;
        [SerializeField] SceneData mainMenuScene;
        [SerializeField] UIWidgetData optionsMenuWidgetData;
        [SerializeField] UIWidgetData creditsMenuWidgetData;
        [SerializeField] UIWidgetData quitMenuWidgetData;
        
        // [Header("Incoming Channels")]
        [Header("Outgoing Events")]
        public SceneDataEvent requestChangeSceneDataEvent;
        public SceneDataEvent requestAddSceneDataEvent;
        public SceneDataEvent requestRemoveSceneDataEvent;
        public UIWidgetDataEvent requestLoadUIWidgetEvent;
        public UIWidgetDataEvent requestUnLoadUIWidgetEvent;

        private bool onClickPlayCoroutineIsGoing = false;
        private bool sceneChanged = false;

        #endregion

        #region Setup ================================

        public override void Setup(UIWidgetData newUIWidgetData)
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnRecieve_OnAcitveSceneChanged;
            
            base.Setup(newUIWidgetData);
        }

        public override void Teardown()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnRecieve_OnAcitveSceneChanged;
            
            base.Teardown();
        }

        #endregion

        #region Channel Responses ================================

        public void OnRecieve_OnAcitveSceneChanged(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            Debug.Log($"MenuIssue: MainMenuWidget: OnRecieve_OnAcitveSceneChanged({scene.name}, {mode})");
            sceneChanged = true;
        }

        #endregion

        #region Main Functions ================================

        public void OnClickPlay()
        {
            StartCoroutine(Delay(PlayButton, 0.1f));
        }
        
        private void PlayButton()
        {
            Debug.Log("MenuIssue: MainMenuWidget: PlayButton()");
            if(onClickPlayCoroutineIsGoing) return;
            StartCoroutine(playCoroutine());
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

        private IEnumerator playCoroutine()
        {
            Debug.Log($"MenuIssue: MainMenuWidget: playCoroutine() started\nsceneChanged: {sceneChanged}");
            
            onClickPlayCoroutineIsGoing = true;
            sceneChanged = false;

            requestAddSceneDataEvent?.Invoke(gameModeScene);
            requestAddSceneDataEvent?.Invoke(mapScene);

            yield return new WaitUntil(() => sceneChanged);
            yield return new WaitForSeconds(0.1f);

            requestRemoveSceneDataEvent?.Invoke(mainMenuScene);
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);

            onClickPlayCoroutineIsGoing = false;

            Debug.Log("MenuIssue: MainMenuWidget: playCoroutine() finished");
        }

        #endregion
    }
}
