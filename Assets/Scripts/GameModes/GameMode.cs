using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class GameMode : MonoBehaviour
    {
        #region Variables ================================

        [Header("Variables")]
        public bool spawnPlayerOnSetup = true;
        public GameObject playerCharacterPreFab;
        public GameObject playerControllerPreFab;
        public GameObject cameraManagerPreFab;
        
        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/GameMode")]
        [SerializeField] private Channel _request_GameModeTearDown_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Player")]
        [SerializeField] private PlayerSpawnInfoChannel _request_SpawnPlayer_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Player")]
        [SerializeField] private IntChannel _on_PlayerCharacterFinishedSetup_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Player")]
        [SerializeField] private IntChannel _on_PlayerControllerFinishedSetup_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/GameMode")]
        [SerializeField] private Channel _on_OptionsMenuQuit_Channel;

        [Header("Outgoing Events")]
        public UnityEvent OnGameModeTearDownFinishedEvent;
        public UnityEvent OnGameModeSetupFinishedEvent;

        private PlayerSpawnInfo _playerSpawnInfo;
        private PlayerCharacter _playerCharacterBeingSetup;
        private PlayerController _playerControllerBeingSetup;
        private CameraManager _cameraManagerBeingSetup;

        #endregion

        #region Setup ================================

        public virtual void Setup()
        {
            SetupChannels();
            if(spawnPlayerOnSetup) TryToFindPlayerSpawnHelper();
            
            DontDestroyOnLoad(this);
            OnGameModeSetupFinishedEvent?.Invoke();
        }

        public virtual void Teardown()
        {
            TearDownChannels();
            OnGameModeTearDownFinishedEvent?.Invoke();
            Destroy(this.gameObject);
        }

         protected virtual void SetupChannels()
        {
            _request_GameModeTearDown_Channel.channelEvent.AddListener(OnRecieve_RequestGameModeTearDown);
            _request_SpawnPlayer_Channel.channelEvent.AddListener(OnRecieve_RequestSpanPlayer);
            _on_PlayerCharacterFinishedSetup_Channel.channelEvent.AddListener(OnRecieve_OnPlayerCharacterFinishedSetup);
            _on_PlayerControllerFinishedSetup_Channel.channelEvent.AddListener(OnRecieve_OnPlayerControllerFinishedSetup);
            _on_OptionsMenuQuit_Channel.channelEvent.AddListener(OnRecieve_OnOptionsMenuQuit);
        }

        protected virtual void TearDownChannels()
        {
            _request_GameModeTearDown_Channel.channelEvent.RemoveListener(OnRecieve_RequestGameModeTearDown);
            _request_SpawnPlayer_Channel.channelEvent.RemoveListener(OnRecieve_RequestSpanPlayer);
            _on_PlayerCharacterFinishedSetup_Channel.channelEvent.RemoveListener(OnRecieve_OnPlayerCharacterFinishedSetup);
            _on_PlayerControllerFinishedSetup_Channel.channelEvent.RemoveListener(OnRecieve_OnPlayerControllerFinishedSetup);
            _on_OptionsMenuQuit_Channel.channelEvent.RemoveListener(OnRecieve_OnOptionsMenuQuit);
        }

        #endregion

        #region Channel Responses ================================
            protected void OnRecieve_RequestGameModeTearDown( )
            {
                Teardown();
            }

            protected void OnRecieve_RequestSpanPlayer(PlayerSpawnInfo newPlayerSpawnInfo )
            {
                SpawnPlayerCharacter(newPlayerSpawnInfo);
            }

            
            protected virtual void OnRecieve_OnPlayerCharacterFinishedSetup(int playerId)
            {
                SpawnPlayerController();
            }

            protected virtual void OnRecieve_OnPlayerControllerFinishedSetup(int playerId)
            {
                SpawnCameraManager();
            }

            protected virtual void OnRecieve_OnOptionsMenuQuit()
            {}

        #endregion

        #region Main Functions ================================

        protected virtual void TryToFindPlayerSpawnHelper()
        {
            // We are making us of the SceneSetupManagers
            // PlayerSpawnHelper playerSpawnHelper = FindFirstObjectByType<PlayerSpawnHelper>();
            // if(playerSpawnHelper != null) playerSpawnHelper.SpawnPlayer();
        }

        protected virtual void SpawnPlayerCharacter(PlayerSpawnInfo newPlayerSpawnInfo)
        {
            Debug.Log("GameMode: SpawnPlayerCharacter()");

            _playerSpawnInfo = newPlayerSpawnInfo;
            
            GameObject characterObj = Instantiate(playerCharacterPreFab, _playerSpawnInfo.location, _playerSpawnInfo.rotation);
            _playerCharacterBeingSetup = characterObj.GetComponent<PlayerCharacter>();
            if(_playerCharacterBeingSetup == null) return;

            _playerCharacterBeingSetup.Setup(_playerSpawnInfo.ID);
        }

        protected virtual void SpawnPlayerController()
        {
            Debug.Log("GameMode: SpawnPlayerController()");
            
            GameObject controllerObj = Instantiate(playerControllerPreFab, _playerSpawnInfo.location, _playerSpawnInfo.rotation);
            _playerControllerBeingSetup = controllerObj.GetComponent<PlayerController>();
            if(_playerControllerBeingSetup == null) return;

            _playerControllerBeingSetup.Setup(_playerSpawnInfo.ID, _playerCharacterBeingSetup);
        }

        protected virtual void SpawnCameraManager()
        {
            Debug.Log("GameMode: SpawnCameraManager()");
            
            GameObject cameraObj = Instantiate(cameraManagerPreFab, _playerSpawnInfo.location, _playerSpawnInfo.rotation);
            _cameraManagerBeingSetup = cameraObj.GetComponent<CameraManager>();
            if(_cameraManagerBeingSetup == null) return;

            _cameraManagerBeingSetup.Setup(_playerSpawnInfo.ID, _playerControllerBeingSetup, _playerCharacterBeingSetup);
        }


        #endregion

    }

}
