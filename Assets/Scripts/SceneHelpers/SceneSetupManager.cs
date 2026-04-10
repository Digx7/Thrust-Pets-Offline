using UnityEngine;

namespace Digx7.Zygote
{
    public class SceneSetupManager : MonoBehaviour
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] private bool changeGameModeOnSceneStart = true;
        [SerializeField] private string gameModeToChangeToOnSetup;
        [SerializeField] private bool changeSongOnSceneStart = true;
        [SerializeField] private SongData songToJumpTo;
        [SerializeField] private SceneContext context;
        [SerializeField] bool triggerOnStart = true;

        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/SceneManager")]
        [SerializeField] private SceneContextChannel updateContextChannel;

        [Header("Outgoing Events")]
        public StringEvent OnChangeGameModeEvent;
        public SongDataEvent OnRequestJumpToSongDataEvent;
        public SceneContextEvent OnSceneSetupEvent;


        public SceneContextEvent onSetup;

        #endregion

        #region Setup ================================

        public void Start()
        {
            if(triggerOnStart) Setup();
        }

        public void Setup()
        {
            if(updateContextChannel.lastValue.SpawnPointID != 0) context.SpawnPointID = updateContextChannel.lastValue.SpawnPointID;
            
            if(changeGameModeOnSceneStart) OnChangeGameModeEvent?.Invoke(gameModeToChangeToOnSetup);
            if(changeSongOnSceneStart) OnRequestJumpToSongDataEvent?.Invoke(songToJumpTo);

            onSetup.Invoke(context);
            OnSceneSetupEvent?.Invoke(context);

            // Add code here

            Debug.Log("SceneSetupManager: Setup()");
        }

        #endregion
    }
}
