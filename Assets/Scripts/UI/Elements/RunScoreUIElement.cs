using UnityEngine;
using System.Collections.Generic;
using Digx7.Zygote;
using TMPro;

public class RunScoreUIElement : UIElement 
{
    #region Variables =================

    [Header("References")]
    [SerializeField] TextMeshProUGUI coinsTMPro;
    [SerializeField] List<AudioSource> coinsAudioSources;
    [SerializeField] TextMeshProUGUI distanceTMPro;

    private int lastCointAudioSourceIndex = 0;
    private float lastCoinAudioSourcePlayTime = 0f;

    [Header("Incoming Channels")]
    [SerializeField] IntChannel on_CoinsChanged_Channel;
    [SerializeField] IntChannel on_CoinGoalChanged_Channel;
    [SerializeField] IntChannel on_LevelChanged_Channel;

    private int coins = 0;
    private int coinGoal = 0;
    private int level = 0;

    #endregion

    #region Setup =================

    private void OnEnable() 
    {
        on_CoinsChanged_Channel.channelEvent.AddListener(OnRecieve_OnCoinsChanged);
        on_CoinGoalChanged_Channel.channelEvent.AddListener(OnRecieve_OnCoinGoalChanged);
        on_LevelChanged_Channel.channelEvent.AddListener(OnRecieve_OnLevelChanged);

        OnRecieve_OnCoinsChanged(0);
        OnRecieve_OnCoinGoalChanged(on_CoinGoalChanged_Channel.lastValue);
        OnRecieve_OnLevelChanged(1);
    }

    private void OnDisable() 
    {
        on_CoinsChanged_Channel.channelEvent.RemoveListener(OnRecieve_OnCoinsChanged);
        on_CoinGoalChanged_Channel.channelEvent.RemoveListener(OnRecieve_OnCoinGoalChanged);
        on_LevelChanged_Channel.channelEvent.RemoveListener(OnRecieve_OnLevelChanged);
    }

    #endregion

    #region Channel Response Methods ======================

    public void OnRecieve_OnCoinsChanged(int newCoins)
    {
        coins = newCoins;

        if(newCoins > 0)
        {
            if(Time.time - lastCoinAudioSourcePlayTime > 0.5f)
            {
                lastCointAudioSourceIndex = 0;
            }
            lastCoinAudioSourcePlayTime = Time.time;
            
            coinsAudioSources[lastCointAudioSourceIndex].Play();
            lastCointAudioSourceIndex = (lastCointAudioSourceIndex + 1) % coinsAudioSources.Count;
        }

        RefreshUI();
    }

    public void OnRecieve_OnCoinGoalChanged(int newCoinGoal)
    {
        coinGoal = newCoinGoal;
        RefreshUI();
    }

    public void OnRecieve_OnLevelChanged(int newLevel)
    {
        level = newLevel;
        RefreshUI();
    }

    

    #endregion

    #region Main Methods ======================

    public void RefreshUI()
    {
        if(coinGoal > 0)
        {
            coinsTMPro.text = $"Coins: {coins}/{coinGoal}";
        }
        else
        {
            coinsTMPro.text = $"Coins: {coins}";
        }

        distanceTMPro.text = $"Distance: {level}";
    }

    #endregion
}