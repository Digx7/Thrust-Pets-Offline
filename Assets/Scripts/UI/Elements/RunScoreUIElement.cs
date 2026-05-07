using UnityEngine;
using Digx7.Zygote;
using TMPro;

public class RunScoreUIElement : UIElement 
{
    #region Variables =================

    [Header("References")]
    [SerializeField] TextMeshProUGUI coinsTMPro;
    [SerializeField] TextMeshProUGUI distanceTMPro;

    [Header("Incoming Channels")]
    [SerializeField] IntChannel on_CoinsChanged_Channel;
    [SerializeField] IntChannel on_LevelChanged_Channel;

    #endregion

    #region Setup =================

    private void OnEnable() 
    {
        on_CoinsChanged_Channel.channelEvent.AddListener(OnRecieve_OnCoinsChanged);
        on_LevelChanged_Channel.channelEvent.AddListener(OnRecieve_OnLevelChanged);

        OnRecieve_OnCoinsChanged(on_CoinsChanged_Channel.lastValue);
        OnRecieve_OnLevelChanged(on_LevelChanged_Channel.lastValue);
    }

    private void OnDisable() 
    {
        on_CoinsChanged_Channel.channelEvent.RemoveListener(OnRecieve_OnCoinsChanged);
        on_LevelChanged_Channel.channelEvent.RemoveListener(OnRecieve_OnLevelChanged);
    }

    #endregion

    #region Channel Response Methods ======================

    public void OnRecieve_OnCoinsChanged(int newScore)
    {
        coinsTMPro.text = $"Coins: {newScore}";
    }

    public void OnRecieve_OnLevelChanged(int newLevel)
    {
        distanceTMPro.text = $"Distance: {newLevel}";
    }

    #endregion
}