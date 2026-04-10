using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class PlayerSpawnHelper : MonoBehaviour
    {
        #region Variables ================================
        
        [Header("Variables")]
        [SerializeField] private int ID = 0;
        [SerializeField] private SceneData activeSceneToWaitFor;

        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Scenes")]
        [SerializeField] private SceneContextChannel contextOnSceneSetupChannel;

        [Header("Outgoing Events")]
        public PlayerSpawnInfoEvent OnRequestSpawnPlayerEvent;

        #endregion

        #region Setup ================================

        private void OnEnable()
        {
            // contextOnSceneSetupChannel.channelEvent.AddListener(SpawnPlayer);
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnRecieve_ActiveSceneChanged;

        }

        private void OnDisable()
        {
            // contextOnSceneSetupChannel.channelEvent.RemoveListener(SpawnPlayer);
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= OnRecieve_ActiveSceneChanged;
        }

        #endregion

        #region Channel Responses ================================

        public void OnRecieve_ActiveSceneChanged(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.Scene scene2)
        {
            if(scene2.name == activeSceneToWaitFor.sceneName)
            {
                SpawnPlayer(contextOnSceneSetupChannel.lastValue);
            }
        }

        #endregion

        #region Main Functions ================================

        public void SpawnPlayer(SceneContext context)
        {
            if(context.SpawnPointID == ID)
            {
                Debug.Log("PlayerSpawnHelper:  SpawnPlayer()");
            
                PlayerSpawnInfo playerSpawnInfo = new PlayerSpawnInfo();

                playerSpawnInfo.ID = 1;
                playerSpawnInfo.location = this.transform.position;
                playerSpawnInfo.rotation = this.transform.rotation;

                OnRequestSpawnPlayerEvent?.Invoke(playerSpawnInfo);

            }
        }

        #endregion
    }
}