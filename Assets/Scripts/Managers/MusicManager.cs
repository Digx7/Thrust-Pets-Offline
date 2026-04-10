using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    public class MusicManager : Singleton<MusicManager>
    {
        #region Variables ================================
        [Header("Variables")]
        [SerializeField] List<SongData> _songQueue;
        [SerializeField] GameObject _songPrefab;
        [SerializeField] SongHolder _activeSong;

        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Music")]
        [SerializeField] SongDataChannel _request_JumpToSongData_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Music")]
        [SerializeField] SongDataChannel _request_QueueSongData_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Music")]
        [SerializeField] Channel _request_SkipSongData_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Music")]
        [SerializeField] Channel _request_PauseOrResumeSongData_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Music")]
        [SerializeField] IntChannel _request_AddSongLayer_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Music")]
        [SerializeField] IntChannel _request_RemoveSongLayer_Channel;

        [Header("Outgoing Events")]
        public UnityEvent OnJumpToSongDataEvent;
        public UnityEvent OnQueueSongDataEvent;
        public UnityEvent OnSkipSongDataEvent;
        public UnityEvent OnPauseOrResumeSongDataEvent;
        public UnityEvent OnAddSongLayerEvent;
        public UnityEvent OnRemoveSongLayerEvent;


        #endregion

        #region Setup ================================

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
            _request_JumpToSongData_Channel.channelEvent.AddListener(OnRecieve_JumpToSongData);
            _request_QueueSongData_Channel.channelEvent.AddListener(OnRecieve_QueueSongData);
            _request_SkipSongData_Channel.channelEvent.AddListener(OnRecieve_SkipSongData);
            _request_PauseOrResumeSongData_Channel.channelEvent.AddListener(OnRecieve_PauseOrResumeSongData);
            _request_AddSongLayer_Channel.channelEvent.AddListener(OnRecieve_AddSongLayer);
            _request_RemoveSongLayer_Channel.channelEvent.AddListener(OnRecieve_RemoveSongLayer);
        }

        private void TearDownChannels()
        {
            _request_JumpToSongData_Channel.channelEvent.RemoveListener(OnRecieve_JumpToSongData);
            _request_QueueSongData_Channel.channelEvent.RemoveListener(OnRecieve_QueueSongData);
            _request_SkipSongData_Channel.channelEvent.RemoveListener(OnRecieve_SkipSongData);
            _request_PauseOrResumeSongData_Channel.channelEvent.RemoveListener(OnRecieve_PauseOrResumeSongData);
            _request_AddSongLayer_Channel.channelEvent.RemoveListener(OnRecieve_AddSongLayer);
            _request_RemoveSongLayer_Channel.channelEvent.RemoveListener(OnRecieve_RemoveSongLayer);
        }

        #endregion

        #region Channel Responses ================================

        protected void OnRecieve_JumpToSongData(SongData song)
        {
            JumpToSong(song);
        }

        protected void OnRecieve_QueueSongData(SongData song)
        {
            QueueSong(song);
        }

        protected void OnRecieve_SkipSongData()
        {
            SkipSong();
        }

        protected void OnRecieve_PauseOrResumeSongData()
        {
            PauseOrResume();
        }

        protected void OnRecieve_AddSongLayer(int layerNumber)
        {
            AddSongLayer(layerNumber);
        }

        protected void OnRecieve_RemoveSongLayer(int layerNumber)
        {
            RemoveSongLayer(layerNumber);
        }

        #endregion

        #region Main Functions ================================

        private void JumpToSong(SongData song)
        {
            PlayNewSong(song);
            OnJumpToSongDataEvent.Invoke();
        }

        private void QueueSong(SongData song)
        {
            _songQueue.Add(song);
            OnQueueSongDataEvent.Invoke();

            if(!IsThereAnActiveSong()) PlayNextSongInQueue();
        } 

        private void SkipSong()
        {
            PlayNextSongInQueue();
            OnSkipSongDataEvent.Invoke();
        }

        private void PauseOrResume()
        {
            if(_activeSong != null) _activeSong.PauseOrResume();
            OnPauseOrResumeSongDataEvent.Invoke();
        }

        private void AddSongLayer(int layerNumber)
        {
            if(_activeSong != null) 
            {
                _activeSong.AddLayer(layerNumber);
                OnAddSongLayerEvent.Invoke();
            }
        }

        private void RemoveSongLayer(int layerNumber)
        {
            if(_activeSong != null) 
            {
                _activeSong.RemoveLayer(layerNumber);
                OnRemoveSongLayerEvent.Invoke();
            }
        }

        public void PlayNextSongInQueue()
        {
            if(_songQueue.Count == 0)
            {
                Debug.Log("The Music Manager tried playing the next song in its queue, but the queue is empty");
                _activeSong.StopSong();
                _activeSong = null;
                return;
            }

            PlayNewSong(_songQueue[0]);
            _songQueue.RemoveAt(0);
        }

        private void PlayNewSong(SongData song)
        {
            if(IsThereAnActiveSong())
            {
                // check is this song is already playing
                if(_activeSong.GetSongData() == song)
                {
                    Debug.LogWarning("We just tried to play the same song that already is playing");
                    return;
                }
                // If a song is active stop it
                _activeSong.StopSong();
            }
            
            // load this song
            GameObject loaded = Instantiate(_songPrefab, this.transform);
            _activeSong = loaded.GetComponent<SongHolder>();
            _activeSong.Setup(song, this);
            // Start this song
            _activeSong.StartSong();
        }

        private bool IsThereAnActiveSong()
        {
            if(_activeSong != null) return true;
            else return false;
        }
    
        #endregion

    }
}