using UnityEngine;
using TMPro;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class PowerUpPageDescriptionElement : UIElement 
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI description;

    public void Start()
    {
        if(PlayerDataManager.Instance.ActivePowerUp != null)
        {
            SetText(PlayerDataManager.Instance.ActivePowerUp);
        }
        else
        {
            description.text = "";
        }
    }

    public void SetText(PowerUpData powerUpData)
    {
        description.text = powerUpData.MenuDescription;
    }
}