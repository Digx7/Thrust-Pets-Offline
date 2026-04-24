using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class PowerUpSelectButtonElement : UIElement 
{
    [Header("Variables")]
    [SerializeField] PowerUpData _powerUp;
    public PowerUpData PlayerSkin
    {
        get
        {
            return _powerUp;
        }
        set
        {
            if(value is PowerUpData)
            {
                _powerUp = value;
                Refreash();
            }
        }
    }

    [Header("Refferences")]
    [SerializeField] Image _icon;
    [SerializeField] PowerUpDataChannelRaiser _powerUpDataChannelRaiser;

    [ContextMenu("Refreash")]
    public void Refreash()
    {
        
        if(_icon != null)_icon.sprite = _powerUp.MenuImage;
        _powerUpDataChannelRaiser?.SetData(_powerUp);
    }
}