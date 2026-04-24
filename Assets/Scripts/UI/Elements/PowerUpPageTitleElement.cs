using UnityEngine;
using TMPro;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class PowerUpPageTitleElement : UIElement 
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI title;

    public void Start()
    {
        if(PlayerDataManager.Instance.ActivePowerUp != null)
        {
            SetText(PlayerDataManager.Instance.ActivePowerUp);
        }
        else
        {
            title.text = "";
        }
    }

    public void SetText(PowerUpData powerUpData)
    {
        title.text = powerUpData.DisplayName;
    }
}