using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    public class SongHolder : MonoBehaviour
    {
        #region Variables ================================
        
        [Header("Variables")]
        [SerializeField] private SongData _songData;
        public SongData GetSongData(){return _songData;}
        [SerializeField] private GameObject _layerPrefab;


        private const float MAXFADEOUTDURATION = 3f;

        private MusicManager _musicManager;
        private List<SongLayerHolder> _layers;

        private float _runtime;
        private float _songLength;
        private bool _isPlaying = false;

        #endregion

        #region Setup ================================

        public void Setup(SongData newSongData, MusicManager newMusicManager)
        {
            _songData = newSongData;
            _musicManager = newMusicManager;

            _layers = new List<SongLayerHolder>();

            for (int i = 0; i < _songData.layers.Count; i++)
            {
                GameObject obj = Instantiate(_layerPrefab, this.transform);
                SongLayerHolder layer = obj.GetComponent<SongLayerHolder>();

                layer.Setup(i, _songData.layers[i], _songData.shouldLoop);
                _layers.Add(layer);
            }

            _songLength = _songData.layers[0].length;
        }

        public void Update()
        {
            CheckIfSongIsEnding();
        }

        #endregion

        #region Main Functions ================================

        public void StartSong()
        {
            _isPlaying = true;
            foreach (SongLayerHolder layer in _layers)
            {
                layer.StartLayer();
            }
            _layers[0].FadeIn();
        }

        public void StopSong()
        {
            _isPlaying = false;
            foreach (SongLayerHolder layer in _layers)
            {
                layer.StopLayer();
            }
            FadeOut();
        }

        public void PauseOrResume()
        {
            _isPlaying = !_isPlaying;
            foreach (SongLayerHolder layer in _layers)
            {
                layer.PauseOrResume();
            }
        }

        public void AddLayer(int layerNumber)
        {
            if(layerNumber >= _layers.Count)
            {
                Debug.LogWarning("A song just tried to add a layer that does not exist");
                return;
            }

            _layers[layerNumber].FadeIn();
        }

        public void RemoveLayer(int layerNumber)
        {
            if(layerNumber >= _layers.Count)
            {
                Debug.LogWarning("A song just tried to remove a layer that does not exist");
                return;
            }

            _layers[layerNumber].FadeOut();
        }

        private void FadeOut()
        {
            StartCoroutine(FadeOutEnum());
        }

        IEnumerator FadeOutEnum()
        {
            yield return new WaitForSeconds(MAXFADEOUTDURATION);
            Destroy(this.gameObject);
        }

        private void CheckIfSongIsEnding()
        {
            // If we are looping there is no point in checking if the song is ending
            if(_isPlaying && !_songData.shouldLoop)
            {
                _runtime += Time.deltaTime;
                if(_runtime >= _songLength)
                {
                    _musicManager.PlayNextSongInQueue();
                }
            }
        }

        #endregion
    }
}
