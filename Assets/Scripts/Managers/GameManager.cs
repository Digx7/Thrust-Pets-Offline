using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    public class GameManager : Singleton<GameManager>
    {
        #region Variables ================================
        
        [Header("Variables")]
        [SerializeField] List<StringAndGameObject> _allGameModes;
        [SerializeField] StringAndGameObject _nextGameModeToLoad;
        [SerializeField] AudioMixer _masterMixer;
        [SerializeField] AnimationCurve _playerPrefAudioValueToAppliedValue;

        private GameMode _activeGameMode;

        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/GameMode")]
        [SerializeField] Channel _on_OptionsChanged_Channel;

        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/GameMode")]
        [SerializeField] StringChannel _request_ChangeGameMode_Channel;

        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/GameMode")]
        [SerializeField] Channel _on_GameModeTeardownFinished_Channel;
        
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/GameMode")]
        [SerializeField] Channel _on_GameModeSetupFinished_Channel;

        [Header("Outgoing Events")]
        public UnityEvent RequestGameModeTearDownEvent;

        private const string MasterVolumeKey = "MasterVolume";
        private const string MusicVolumeKey = "MusicVolume";
        private const string SFXVolumeKey = "SFXVolume";
        private const string FullScreenKey = "FullScreen";
        private const string ResolutionKey = "Resolution";

        #endregion

        #region Setup ================================

        public override void SafeStart()
        {
            // According to Unity Docs making calls to the Audio Mixer in OnEnable will not work
            // So this function will be run in Start() instead
            SetupOptions();
        }
        
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
            _on_OptionsChanged_Channel.channelEvent.AddListener(OnRecieve_OnOptionsChanged);
            _request_ChangeGameMode_Channel.channelEvent.AddListener(OnRecieve_RequestChangeGameMode);
            _on_GameModeTeardownFinished_Channel.channelEvent.AddListener(OnRecieve_OnGameModeTeardownFinished);
            _on_GameModeSetupFinished_Channel.channelEvent.AddListener(OnRecieve_OnGameModeSetupFinished);
        }

        private void TearDownChannels()
        {
            _on_OptionsChanged_Channel.channelEvent.RemoveListener(OnRecieve_OnOptionsChanged);
            _request_ChangeGameMode_Channel.channelEvent.RemoveListener(OnRecieve_RequestChangeGameMode);
            _on_GameModeTeardownFinished_Channel.channelEvent.RemoveListener(OnRecieve_OnGameModeTeardownFinished);
            _on_GameModeSetupFinished_Channel.channelEvent.RemoveListener(OnRecieve_OnGameModeSetupFinished);
        }

        #endregion

        #region Channel Responses ================================

        protected void OnRecieve_OnOptionsChanged()
        {
            SetupOptions();
        }

        protected void OnRecieve_RequestChangeGameMode(string newGameMode)
        {
            // Checks that we are not loading the active gamemode
            if(newGameMode == _nextGameModeToLoad.name)
            {
                Debug.LogWarning("Something just tried to change the gamemode to the currently active gamemode");
                return;
            }

            // Checks the requested gamemode is valide
            if(_allGameModes.Exists(x => x.name == newGameMode))
            {
                _nextGameModeToLoad = _allGameModes.Find(x => x.name == newGameMode);


                // Checks if there is an active gamemode
                if(_activeGameMode != null)
                {
                    // Tearsdown the currently active GameMode, once that is done it GameManager will setup the new GameMode
                    RequestGameModeTearDownEvent.Invoke();
                    return;
                }
                else
                {
                    // Jumps straight to loading the new gamemode
                    OnRecieve_OnGameModeTeardownFinished();
                }
                
            }
            else
            {
                Debug.LogWarning("Something tried to load the gamemode " + newGameMode + " which does not exist on the current GameManager.  Double check your spelling");
            }
        }

        protected void OnRecieve_OnGameModeTeardownFinished()
        {
            // Passing in nextGameModeToLoad as a prefab but it becomes a scene object
            LoadGameMode(_nextGameModeToLoad.obj);
            SetupGameMode(_activeGameMode);
        } 

        protected void OnRecieve_OnGameModeSetupFinished()
        {}

        #endregion

        #region Main Functions ================================
        public void SetupOptions()
        {
            Debug.Log("GameManager: SetupOptions()");
            
            float masterVolumeStoredValue = PlayerPrefs.GetFloat(MasterVolumeKey, 1.0f);
            float musicVolumeStoredValue = PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f);
            float sfxVolumeStoredValue = PlayerPrefs.GetFloat(SFXVolumeKey, 1.0f);
            int fullScreen = PlayerPrefs.GetInt(FullScreenKey, 0);
            int resolution = PlayerPrefs.GetInt(ResolutionKey, 0);

            bool fullScreenValue;

            if(fullScreen == 1) fullScreenValue = true;
            else fullScreenValue = false;

            float masterVolumeAppliedValue = _playerPrefAudioValueToAppliedValue.Evaluate(masterVolumeStoredValue);
            float musicVolumeAppliedValue = _playerPrefAudioValueToAppliedValue.Evaluate(musicVolumeStoredValue);
            float sfxVolumeAppliedValue = _playerPrefAudioValueToAppliedValue.Evaluate(sfxVolumeStoredValue);


            _masterMixer.SetFloat(MasterVolumeKey, masterVolumeAppliedValue);
            _masterMixer.SetFloat(MusicVolumeKey, musicVolumeAppliedValue);
            _masterMixer.SetFloat(SFXVolumeKey, sfxVolumeAppliedValue);

            Screen.fullScreen = fullScreenValue;

            int width = 0;
            int height = 0;

            switch (resolution)
            {
                case 0:
                    width = 1920;
                    height = 1080;
                    break;
                case 1:
                    width = 1280;
                    height = 720;
                    break;
                case 2:
                    width = 720;
                    height = 480;
                    break;
                default:
                    break;
            }

            Screen.SetResolution(width, height, fullScreenValue);

            Debug.Log("masterVolume StoredValue: " + masterVolumeStoredValue + "\nmasterVolume Applied: " + masterVolumeAppliedValue);
            Debug.Log("musicVolume StoredValue: " + musicVolumeStoredValue + "\nmusicVolume Applied: " + musicVolumeAppliedValue);
            Debug.Log("sfxVolume StoredValue: " + sfxVolumeStoredValue + "\nsfxVolume Applied: " + sfxVolumeAppliedValue);
            Debug.Log("fullscreen value: " + fullScreenValue);
            Debug.Log("resolution stored: " + resolution + "\nApplied: " + width + " x " + height);
        }

        public void LoadGameMode(GameObject newGameMode)
        {
            _nextGameModeToLoad.obj = Instantiate(newGameMode);
            _activeGameMode = _nextGameModeToLoad.obj.GetComponent<GameMode>();
        }

        public void SetupGameMode(GameMode gameModeToSetup)
        {
            gameModeToSetup.Setup();
        }

        #endregion

    }

}
