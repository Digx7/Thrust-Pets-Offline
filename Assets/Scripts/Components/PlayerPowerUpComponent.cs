using UnityEngine;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class PlayerPowerUpComponent : MonoBehaviour 
{
    [Header("Reffernces")]
    [SerializeField] PowerUpData _currentPowerUp;

    [Header("Incoming Channels")]
    [SerializeField] Channel _request_TryToUsePowerUp_channel;

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

    public void TryUsePowerUp()
    {
        if(_currentPowerUp.TryUse(out GameObject powerupRuntimePrefab))
        {
            GameObject powerupRuntimeObj = Instantiate(powerupRuntimePrefab, this.transform);
        }
    }
}