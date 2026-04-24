using UnityEngine;
using UnityEngine.UI;
using Digx7.Zygote;
using Digx7.ThrustPets;
using TMPro;

public class MainMenuSelectedPowerUpElment : UIElement 
{
    [Header("Reffernces")]
    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _count;

    public void Start()
    {
        Refreash(PlayerDataManager.Instance.ActivePowerUp);
    }

    public void Refreash(PowerUpData powerUpData)
    {
        Debug.Log("MainMenuSelectedPowerUpElment: Refresh()");
        
        if(powerUpData.MenuImage != null)
        {
            _icon.sprite = powerUpData.MenuImage;

            Debug.Log($"MainMenuSelectedPowerUpElment: Refresh() _icon.sprite = {powerUpData.MenuImage}");
        }
        else
        {
            Debug.Log($"MainMenuSelectedPowerUpElment: Refresh() powerUpData.MenuImage == null");
        }
    }
}