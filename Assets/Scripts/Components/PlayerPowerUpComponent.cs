using UnityEngine;
using Digx7.Zygote;
using Digx7.ThrustPets;
using System.Collections.Generic;

public class PlayerPowerUpComponent : MonoBehaviour 
{
    [Header("Reffernces")]
    [SerializeField] PowerUpData _currentPowerUp;
    [SerializeField] List<PowerUpData> _currentPowerUps;

    [Header("Incoming Channels")]
    [SerializeField] IntChannel _request_TryToUsePowerUp_channel;

    public void Start()
    {
        _currentPowerUp = PlayerDataManager.Instance.ActivePowerUp;
    }

    private void OnEnable() 
    {
        _request_TryToUsePowerUp_channel.channelEvent.AddListener(TryUsePowerUp);
    }

    private void OnDisable() 
    {
        _request_TryToUsePowerUp_channel.channelEvent.RemoveListener(TryUsePowerUp);
    }

    public void TryUsePowerUp(int powerUpIndex = -1)
    {
        if(powerUpIndex == -1)
        {    
            if(_currentPowerUp.TryUse(out GameObject powerupRuntimePrefab))
            {
                GameObject powerupRuntimeObj = Instantiate(powerupRuntimePrefab, this.transform);
            }
        }
        else
        {
            if(powerUpIndex < _currentPowerUps.Count)
            {
                if(_currentPowerUps[powerUpIndex].TryUse(out GameObject powerupRuntimePrefab))
                {
                    Debug.Log($"PlayerPowerUpComponent just used the powerup at index {powerUpIndex} which is {_currentPowerUps[powerUpIndex].name}");
                    
                    GameObject powerupRuntimeObj = Instantiate(powerupRuntimePrefab, this.transform);
                }
            }
            else
            {
                Debug.LogWarning($"PlayerPowerUpComponent just tried to use the powerup at index {powerUpIndex} but this is out of bounds\nThe component currently only has {_currentPowerUps.Count} powerups");
            }
        }
    }
}