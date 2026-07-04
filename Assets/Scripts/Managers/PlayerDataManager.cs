using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using Digx7.ThrustPets;
using Digx7.Zygote;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    
    #region Variables
    [Header("Variables")]
    [SerializeField] PlayerSkinData _playerSkin;
    public PlayerSkinData PlayerSkin
    {
        get
        {
            return _playerSkin;
        }
        private set
        {
            if (value is PlayerSkinData)
            {
                _playerSkin = value;
                OnUpdatePlayerSkin.Invoke(_playerSkin);
            }
        }
    }

    [SerializeField] PowerUpData _activePowerUp;
    [SerializeField] List<PowerUpData> _activePowerUps;
    public PowerUpData ActivePowerUp
    {
        get
        {
            // if(_activePowerUps.Count > 1)
            // {
            //     return _activePowerUps[0];
            // }
            // else
            // {
            //     return _activePowerUp;
            // }

            return _activePowerUp;
            
        }
        private set
        {
            if (value is PowerUpData)
            {
                _activePowerUp = value;
                // _activePowerUps[0] = value;
                OnUpdateActivePowerUp.Invoke(_activePowerUp);
            }
        }
    }

    [Header("Incoming Channels")]
    [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/Player")]
    [SerializeField] PlayerSkinDataChannel _Request_UpdatePlayerSkin_Channel;
    [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/Player")]
    [SerializeField] PowerUpDataChannel _Request_UpdateActivePowerUp_Channel;

    [Header("Outgoing Events")]
    public PlayerSkinDataEvent OnUpdatePlayerSkin;
    public PowerUpDataEvent OnUpdateActivePowerUp;
     
    #endregion
    #region Setup
    private void Start()
    {
        
    }
    
    private void OnEnable()
    {
        SetupChannels();
    }
    
    private void OnDisable()
    {
        TearDownChannels();
    }
    
    private void SetupChannels()
    {
        _Request_UpdatePlayerSkin_Channel.channelEvent.AddListener(OnRecieve_RequestUpdatePlayerSkin);
        _Request_UpdateActivePowerUp_Channel.channelEvent.AddListener(OnRecieve_RequestUpdateActivePowerUp);
    }
    
    private void TearDownChannels()
    {
        _Request_UpdatePlayerSkin_Channel.channelEvent.RemoveListener(OnRecieve_RequestUpdatePlayerSkin);
        _Request_UpdateActivePowerUp_Channel.channelEvent.RemoveListener(OnRecieve_RequestUpdateActivePowerUp);
    }
    
    #endregion
    #region ChannelRespones

    private void OnRecieve_RequestUpdatePlayerSkin(PlayerSkinData playerSkinData)
    {
        PlayerSkin = playerSkinData;
    }

    private void OnRecieve_RequestUpdateActivePowerUp(PowerUpData powerUpData)
    {
        ActivePowerUp = powerUpData;
    }
    
    #endregion
    #region MainFunctions
    
    #endregion
    
}