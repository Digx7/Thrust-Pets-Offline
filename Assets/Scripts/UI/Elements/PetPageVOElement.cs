using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class PetPageVOElement : UIElement 
{
    [Header("References")]
    [SerializeField] AudioSource audioSource;

    public void Start()
    {
        // SetAudioGenerator(PlayerDataManager.Instance.PlayerSkin);
    }

    public void SetAudioGenerator(PlayerSkinData playerSkinData)
    {
        audioSource.Stop();
        audioSource.generator = (IAudioGenerator)playerSkinData.MenuVO;
        audioSource.Play();
    }
}