using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Digx7.ThrustPets;

namespace Digx7.Zygote
{
    public class MainMenuWidget : UIMenu
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] List<SceneData> gameModeScenes;
        [SerializeField] List<ThrustPetGameModeData> gameModes;
        [SerializeField] List<SceneData> mapScenes;
        [SerializeField] List<ThrustPetMapData> maps;
        [SerializeField] SceneData mainMenuScene;
        [SerializeField] UIWidgetData optionsMenuWidgetData;
        [SerializeField] UIWidgetData creditsMenuWidgetData;
        [SerializeField] UIWidgetData quitMenuWidgetData;
        [SerializeField] AudioSource VOAudioSource;
        
        // [Header("Incoming Channels")]
        [Header("Outgoing Events")]
        public SceneDataEvent requestChangeSceneDataEvent;
        public SceneDataEvent requestSetActiveSceneDataEvent;
        public SceneDataEvent requestAddSceneDataEvent;
        public SceneDataEvent requestRemoveSceneDataEvent;
        public UIWidgetDataEvent requestLoadUIWidgetEvent;
        public UIWidgetDataEvent requestUnLoadUIWidgetEvent;

        private bool onClickPlayCoroutineIsGoing = false;
        private bool mapLoaded = false;

        private int selectedGameModeIndex = 0;
        private int selectedMapIndex = 0;

        #endregion

        #region Setup ================================

        public override void Setup(UIWidgetData newUIWidgetData)
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnRecieve_SceneLoaded;
            
            base.Setup(newUIWidgetData);
        }

        public override void Teardown()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnRecieve_SceneLoaded;
            
            base.Teardown();
        }

        #endregion

        #region Channel Responses ================================

        public void OnRecieve_SceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            Debug.Log($"MenuIssue: MainMenuWidget: OnRecieve_SceneLoaded({scene.name}, {mode})");
            
            if(scene.name == mapScenes[selectedMapIndex].sceneName)
            {
                mapLoaded = true;
            }
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

        public void OnSelectGameMode(int index)
        {
            selectedGameModeIndex = index;

            VOAudioSource.generator = (IAudioGenerator)gameModes[selectedGameModeIndex].MenuVO;
            VOAudioSource.Play();
        }

        public void OnSelectMap(int index)
        {
            selectedMapIndex = index;

            VOAudioSource.generator = (IAudioGenerator)maps[selectedMapIndex].MenuVO;
            VOAudioSource.Play();
        }

        private IEnumerator playCoroutine()
        {
            Debug.Log($"MenuIssue: MainMenuWidget: playCoroutine() started\nmapLoaded: {mapLoaded}");
            
            onClickPlayCoroutineIsGoing = true;
            mapLoaded = false;

            // requestAddSceneDataEvent?.Invoke(gameModeScenes[selectedGameModeIndex]);
            // requestAddSceneDataEvent?.Invoke(mapScenes[selectedMapIndex]);

            requestAddSceneDataEvent?.Invoke(gameModes[selectedGameModeIndex].sceneData);
            requestAddSceneDataEvent?.Invoke(maps[selectedMapIndex].sceneData);

            yield return new WaitUntil(() => mapLoaded);
            yield return new WaitForSeconds(0.1f);

            // requestSetActiveSceneDataEvent?.Invoke(mapScenes[selectedMapIndex]);
            requestSetActiveSceneDataEvent?.Invoke(maps[selectedMapIndex].sceneData);
            requestRemoveSceneDataEvent?.Invoke(mainMenuScene);
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);

            onClickPlayCoroutineIsGoing = false;

            Debug.Log("MenuIssue: MainMenuWidget: playCoroutine() finished");
        }

        #endregion
    }
}
