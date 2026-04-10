using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class PlayerSpawnHelper : MonoBehaviour
    {
        #region Variables ================================
        
        [Header("Variables")]
        [SerializeField] private int ID = 0;

        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Scenes")]
        [SerializeField] private SceneContextChannel contextOnSceneSetupChannel;

        [Header("Outgoing Events")]
        public PlayerSpawnInfoEvent OnRequestSpawnPlayerEvent;

        #endregion

        #region Setup ================================

        private void OnEnable()
        {
            contextOnSceneSetupChannel.channelEvent.AddListener(SpawnPlayer);
        }

        private void OnDisable()
        {
            contextOnSceneSetupChannel.channelEvent.RemoveListener(SpawnPlayer);
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