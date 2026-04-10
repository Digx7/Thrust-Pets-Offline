using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace Digx7.Zygote
{
    public class OptionsMenuWidget : UIMenu
    {
        #region Variables ================================
        
        [Header("Variables")]
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Toggle fullScreenToggle;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        
        // [Header("Incoming Channels")]
        [Header("Outgoing Events")]
        public UIWidgetDataEvent requestUnLoadUIWidgetEvent;
        public UnityEvent onOptionsChangedEvent;
        public UnityEvent onOptionsMenuQuitEvent;

        private const string MasterVolumeKey = "MasterVolume";
        private const string MusicVolumeKey = "MusicVolume";
        private const string SFXVolumeKey = "SFXVolume";
        private const string FullScreenKey = "FullScreen";
        private const string ResolutionKey = "Resolution";

        #endregion

        #region Setup ================================

        public override void Setup(UIWidgetData newUIWidgetData)
        {
            base.Setup(newUIWidgetData);
            LoadValuesFromPlayerPrefs();
        }

        public override void Teardown()
        {
            base.Teardown();
        }

        #endregion

        #region Main Functions ================================

        public void OnClickBack()
        {
            onOptionsMenuQuitEvent?.Invoke();
            requestUnLoadUIWidgetEvent?.Invoke(ownUIWidgetData);
        }

        private void LoadValuesFromPlayerPrefs()
        {
            float masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1.0f);
            float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f);
            float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1.0f);
            int fullScreen = PlayerPrefs.GetInt(FullScreenKey, 0);
            int resolution = PlayerPrefs.GetInt(ResolutionKey, 0);

            masterVolumeSlider.value = masterVolume;
            musicVolumeSlider.value = musicVolume;
            sfxVolumeSlider.value = sfxVolume;

            bool fullScreenValue;

            if(fullScreen == 1) fullScreenValue = true;
            else fullScreenValue = false;

            fullScreenToggle.isOn = fullScreenValue;
            resolutionDropdown.value = resolution;

        }

        public void OnAdjustMasterVolume(float newValue)
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, newValue);
            PlayerPrefs.Save();
            onOptionsChangedEvent?.Invoke();
        }

        public void OnAdjustMusicVolume(float newValue)
        {
            PlayerPrefs.SetFloat(MusicVolumeKey, newValue);
            PlayerPrefs.Save();
            onOptionsChangedEvent?.Invoke();
        }

        public void OnAdjustSFXVolume(float newValue)
        {
            PlayerPrefs.SetFloat(SFXVolumeKey, newValue);
            PlayerPrefs.Save();
            onOptionsChangedEvent?.Invoke();
        }

        public void OnAdjustFullScreen(bool newValue)
        {
            int value;

            if(newValue) value = 1;
            else value = 0;

            PlayerPrefs.SetInt(FullScreenKey, value);
            PlayerPrefs.Save();
            onOptionsChangedEvent?.Invoke();
        }

        public void OnAdjustResolution(int newValue)
        {
            PlayerPrefs.SetInt(ResolutionKey, newValue);
            PlayerPrefs.Save();
            onOptionsChangedEvent?.Invoke();
        }

        #endregion
    }
}
