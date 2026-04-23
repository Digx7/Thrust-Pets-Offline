using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class PetSelectButtonElement : UIElement 
{
    [Header("Variables")]
    [SerializeField] PlayerSkinData _playerSkin;
    public PlayerSkinData PlayerSkin
    {
        get
        {
            return _playerSkin;
        }
        set
        {
            if(value is PlayerSkinData)
            {
                _playerSkin = value;
                Refreash();
            }
        }
    }

    [Header("Refferences")]
    [SerializeField] Image _icon;
    [SerializeField] PlayerSkinDataChannelRaiser _playerSkinDataChannelRaiser;

    [ContextMenu("Refreash")]
    public void Refreash()
    {
        
        if(_icon != null)_icon.sprite = _playerSkin.MenuImage;
        _playerSkinDataChannelRaiser?.SetData(_playerSkin);
    }
}