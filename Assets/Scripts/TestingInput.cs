using UnityEngine;

namespace Digx7.Zygote
{
    public class TestingInput : MonoBehaviour
    {
        #region Variables ================================

        [SerializeField] string gameModeToLoadOnStart;
        [SerializeField] StringChannel onChangeGameModeChannel;
        [SerializeField] StringChannel changeSceneChannel;
        [SerializeField] Channel requestPauseOrResumeSongDataChannel;
        [SerializeField] Channel requestSkipSongDataChannel;
        [SerializeField] SongDataChannel requestQueueSongDataChannel;
        [SerializeField] SongData songToQueue;
        
        #endregion

        #region Main Functions ================================

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                onChangeGameModeChannel.Raise(gameModeToLoadOnStart);
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                onChangeGameModeChannel.Raise("MainMenu");
            }

            if(Input.GetKeyDown(KeyCode.D))
            {
                onChangeGameModeChannel.Raise("GamePlay");
            }

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                changeSceneChannel.Raise("MainMenu");
            }

            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                changeSceneChannel.Raise("GamePlay");
            }

            if(Input.GetKeyDown(KeyCode.P))
            {
                requestPauseOrResumeSongDataChannel.Raise();
            }

            if(Input.GetKeyDown(KeyCode.O))
            {
                requestSkipSongDataChannel.Raise();
            }

            if(Input.GetKeyDown(KeyCode.I))
            {
                requestQueueSongDataChannel.Raise(songToQueue);
            }
        }
    
        #endregion
    }
}