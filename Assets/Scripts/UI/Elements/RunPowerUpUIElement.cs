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

    public void Start()
    {
        Refreash(PlayerDataManager.Instance.ActivePowerUp);
    }

    public void Refreash(PowerUpData powerUpData)
    {
        if(powerUpData.MenuImage != null)
        {
            SetIcon(powerUpData.MenuImage);
        }
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