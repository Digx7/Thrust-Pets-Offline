using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    public class SongLayerHolder : MonoBehaviour
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] private int _layerNumber;
        [SerializeField] private AudioClip _clip;
        [SerializeField] private AudioSource _audioSource;

        private const float FADEINSPEED = 0.5f;
        private const float FADEOUTSPEED = 0.5f;
        
        #endregion

        #region Setup ================================

        public void Setup(int newLayerNumber, AudioClip newClip, bool shouldLoop = false)
        {
            _layerNumber = newLayerNumber;
            _clip = newClip;
            _audioSource.clip = _clip;
            _audioSource.volume = 0;
            _audioSource.loop = shouldLoop;
        }

        #endregion

        #region Main Functions ================================

        public void StartLayer()
        {
            _audioSource.Play();
        }

        public void StopLayer()
        {
            FadeOut();
        }

        public void PauseOrResume()
        {
            if(_audioSource.isPlaying)
            {
                _audioSource.Pause();
            }
            else
            {
                _audioSource.UnPause();
            }
        }

        public void SetVolume(float volume)
        {
            _audioSource.volume = volume;
        }

        public void FadeIn()
        {
            StartCoroutine(FadeInEnum());
        }

        public void FadeOut()
        {
            StartCoroutine(FadeOutEnum());
        }

        IEnumerator FadeInEnum()
        {
            while(_audioSource.volume < 1.0f)
            {
                SetVolume(_audioSource.volume + (FADEINSPEED * Time.deltaTime));
                yield return null;
            }
            SetVolume(1.0f);
        }

        IEnumerator FadeOutEnum()
        {
            while(_audioSource.volume > 0.0f)
            {
                SetVolume(_audioSource.volume - (FADEINSPEED * Time.deltaTime));
                yield return null;
            }
            SetVolume(0.0f);
        }

        #endregion
    }
}
