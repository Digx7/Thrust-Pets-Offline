using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class PowerUpPageVOElement : UIElement 
{
    [Header("References")]
    [SerializeField] AudioSource audioSource;

    public void Start()
    {
        // if(PlayerDataManager.Instance.ActivePowerUp != null)
        // {
        //     SetAudioGenerator(PlayerDataManager.Instance.ActivePowerUp);
        // }
        // else
        // {
        //     audioSource.Stop();
        // }
    }

    public void SetAudioGenerator(PowerUpData powerUpData)
    {
        audioSource.Stop();
        audioSource.generator = (IAudioGenerator)powerUpData.MenuVO;
        audioSource.Play();
    }
}