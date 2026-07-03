using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class PetPagePhotoElement : UIElement 
{
    [Header("References")]
    [SerializeField] Image photo;

    public void Start()
    {
        SetPhoto(PlayerDataManager.Instance.PlayerSkin);
    }

    public void SetPhoto(PlayerSkinData playerSkinData)
    {
        photo.sprite = playerSkinData.PhotoImage;
    }
}