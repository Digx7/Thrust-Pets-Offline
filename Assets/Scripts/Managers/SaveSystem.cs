using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Digx7.Zygote
{
    public class SaveSystem : Singleton<SaveSystem>
    {
        #region Variables ================================
        
        [Header("Variables")]
        public int activeSaveSlot = 0;
        public Save save;

        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/SaveSystem")]
        [SerializeField] Channel _request_SaveGame_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/SaveSystem")]
        [SerializeField] Channel _request_LoadGame_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/SaveSystem")]
        [SerializeField] Channel _request_DeleteSave_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/SaveSystem")]
        [SerializeField] Channel _request_MakeNewSave_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/SaveSystem")]
        [SerializeField] IntChannel _request_ChangeActiveSaveSlot_Channel;

        [Header("Outgoing Events")]
        public UnityEvent OnGameSavedEvent;
        public UnityEvent OnGameLoadedEvent;
        public UnityEvent OnDeleteSaveEvent;
        public UnityEvent OnMakeNewSaveEvent;

        

        #endregion

        #region Setup ================================
        public override void SafeOnEnable()
        {
            _request_SaveGame_Channel.channelEvent.AddListener(OnRecieve_RequestSaveGame);
            _request_LoadGame_Channel.channelEvent.AddListener(OnRecieve_RequestLoadGame);
            _request_DeleteSave_Channel.channelEvent.AddListener(OnRecieve_RequestDeleteSave);
            _request_MakeNewSave_Channel.channelEvent.AddListener(OnRecieve_RequestMakeNewSave);

            save = new Save();
        }

        public override void SafeOnDisable()
        {
            _request_SaveGame_Channel.channelEvent.RemoveListener(OnRecieve_RequestSaveGame);
            _request_LoadGame_Channel.channelEvent.RemoveListener(OnRecieve_RequestLoadGame);
            _request_DeleteSave_Channel.channelEvent.RemoveListener(OnRecieve_RequestDeleteSave);
            _request_MakeNewSave_Channel.channelEvent.RemoveListener(OnRecieve_RequestMakeNewSave);
        }
        
        #endregion

        #region Channel Responses ================================

        protected void OnRecieve_RequestSaveGame()
        {
            SaveData();
        }

        protected void OnRecieve_RequestLoadGame()
        {
            LoadData();
        }

        protected void OnRecieve_RequestDeleteSave()
        {
            DeleteData();
        }

        protected void OnRecieve_RequestMakeNewSave()
        {
            MakeNewSaveData();
        }

        #endregion

        #region Main Functions ================================

        public void SaveData()
        {
            string destination = Path();
            FileStream file;

            if(File.Exists(destination)) file = File.OpenWrite(destination);
            else file = File.Create(destination);

            DataContractSerializer serializer = new DataContractSerializer(save.GetType());
            serializer.WriteObject(file, save);
            file.Close();

            Debug.Log("SaveSystem: Saving game to slot " + activeSaveSlot);

            OnGameSavedEvent.Invoke();
        }

        public void LoadData()
        {
            string destination = Path();
            FileStream file;

            Debug.Log("SaveSystem: Trying to load save at slot " + activeSaveSlot);

            if(File.Exists(destination)) file = File.OpenRead(destination);
            else
            {
                Debug.Log("SaveSystem: Save Data not found at slot " + activeSaveSlot);
                MakeNewSaveData();
                return;
            }

            DataContractSerializer serializer = new DataContractSerializer(save.GetType());
            save = serializer.ReadObject(file) as Save;
            file.Close();

            Debug.Log("SaveSystem: Loaded save from slot " + activeSaveSlot);

            OnGameLoadedEvent.Invoke();
        }

        [ContextMenu("DeleteData")]
        public void DeleteData()
        {
            string destination = Path();
            
            if(File.Exists(destination)) File.Delete(destination);
            else
            {
                Debug.LogWarning("SaveSystem: tried to delete a file in slot " + activeSaveSlot + ".  But no file exists there");
                return;
            }

            Debug.Log("SaveSystem: deleted save at slot " + activeSaveSlot);

            OnDeleteSaveEvent.Invoke();
        }

        public void MakeNewSaveData()
        {
            Debug.Log("SaveSystem: Making a new save at slot " + activeSaveSlot);
            save.ClearAllData();
            save.CreateDefaultSaveFile();
            SaveData();
            OnMakeNewSaveEvent.Invoke();
        }

        private string Path()
        {
            return Application.persistentDataPath + "/save" + activeSaveSlot + ".data";
        }

        #endregion
    }
}
