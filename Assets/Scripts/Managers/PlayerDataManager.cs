using UnityEngine;
using UnityEngine.Events;
using System;
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

    [Header("Incoming Channels")]
    [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/Player")]
    [SerializeField] PlayerSkinDataChannel _Request_UpdatePlayerSkin_Channel;

    [Header("Outgoing Events")]
    public PlayerSkinDataEvent OnUpdatePlayerSkin;
     
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
    }
    
    private void TearDownChannels()
    {
        _Request_UpdatePlayerSkin_Channel.channelEvent.RemoveListener(OnRecieve_RequestUpdatePlayerSkin);
    }
    
    #endregion
    #region ChannelRespones

    private void OnRecieve_RequestUpdatePlayerSkin(PlayerSkinData playerSkinData)
    {
        PlayerSkin = playerSkinData;
    }
    
    #endregion
    #region MainFunctions
    
    #endregion
    
}