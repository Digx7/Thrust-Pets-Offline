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
        // _count.text = powerUpData.UsesLeft.ToString();
        
        if(powerUpData.MenuImage != null)
        {
            _icon.sprite = powerUpData.MenuImage;
        }
    }
}