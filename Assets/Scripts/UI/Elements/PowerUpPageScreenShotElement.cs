using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class PowerUpPageScreenShotElement : UIElement 
{
    [Header("References")]
    [SerializeField] Image image;

    public void Start()
    {
        if(PlayerDataManager.Instance.ActivePowerUp != null)
        {
            SetImage(PlayerDataManager.Instance.ActivePowerUp);
        }
        else
        {
            image.sprite = null;
        }
    }

    public void SetImage(PowerUpData powerUpData)
    {
        image.sprite = powerUpData.MenuPhoto;
    }
}