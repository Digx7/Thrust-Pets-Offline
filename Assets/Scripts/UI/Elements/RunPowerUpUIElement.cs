using UnityEngine;
using UnityEngine.UI;
using Digx7.Zygote;
using Digx7.ThrustPets;
using TMPro;

public class RunPowerUpUIElement : UIElement 
{
    [Header("Reffernces")]
    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _count;
    [SerializeField] Slider _slider;

    private PowerUpData currentPowerUp;

    public void Start()
    {
        Refreash(PlayerDataManager.Instance.ActivePowerUp);
    }

    public void Refreash(PowerUpData powerUpData)
    {
        if(currentPowerUp != null)
        {
            // Removes listeners to events of last powerup, if it exitst
            currentPowerUp.OnSuccessfullyUse.RemoveListener(OnPowerUpUse);
        }
        currentPowerUp = powerUpData;
        // Setsup listeners to events of new powerup
        currentPowerUp.OnSuccessfullyUse.AddListener(OnPowerUpUse);
        
        if(powerUpData.MenuImage != null)
        {
            SetIcon(powerUpData.MenuImage);
        }
    }

    public void OnPowerUpUse()
    {
        SetCount(currentPowerUp.UsesLeft);
    }

    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    public void SetCount(int currentCount)
    {
        _count.text = currentCount.ToString();
    }
}