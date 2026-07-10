using UnityEngine;
using UnityEngine.Audio;
using Digx7.Zygote;
using TMPro;

public class GameOverMenuWidget : UIMenu 
{
    #region Variables ================================

    [Header("References")]
    [SerializeField] TextMeshProUGUI titleTMPro;
    [SerializeField] TextMeshProUGUI levelReachedTMPro;
    [SerializeField] TextMeshProUGUI coinsCollectedTMPro;
    [SerializeField] TextMeshProUGUI timeRemainingTMPro;
    [SerializeField] TextMeshProUGUI scoreTMPro;
    [SerializeField] AudioSource VOAudioSource;
    [SerializeField] AudioResource VOYouWin;
    [SerializeField] AudioResource VOYouLose;

    [Header("Incomming Channels")]
    [SerializeField] GameEndResultChannel _On_GameEnd_Channel;

    #endregion
    
    #region Setup ================================

    public override void Setup(UIWidgetData newUIWidgetData)
    {
        base.Setup(newUIWidgetData);

        Refresh(_On_GameEnd_Channel.lastValue);
    }

    public override void Teardown()
    {
        base.Teardown();
    }

    #endregion

    #region Main Methods ================================

    public void Refresh(GameEndResult gameEndResults)
    {
        switch (gameEndResults.endCondition)
        {
            case GameEndCondition.Loss:
                titleTMPro.text = "You Lost";
                VOAudioSource.generator = (IAudioGenerator)VOYouLose;
                break;
            case GameEndCondition.Win:
                titleTMPro.text = "You Win";
                VOAudioSource.generator = (IAudioGenerator)VOYouWin;
                break;
            default:
                break;
        }
        
        
        levelReachedTMPro.text = $"Distance: {gameEndResults.levelReached}";
        coinsCollectedTMPro.text = $"Coins: {gameEndResults.coins}";

        if(gameEndResults.time >= -1 && timeRemainingTMPro != null)
        {
            int minutes = Mathf.FloorToInt(gameEndResults.time / 60f);
            int seconds = Mathf.FloorToInt(gameEndResults.time % 60f);

            if(minutes < 0) minutes = 0;
            if(seconds < 0) seconds = 0;

            timeRemainingTMPro.text = $"Time: {minutes:00}:{seconds:00}";
        }


        scoreTMPro.text = $"Score: {gameEndResults.score}";
    }

    public void OnClickQuit()
    {
        
    }

    public void OnClickPlayAgain()
    {
        
    }

    public void PlayVO()
    {
        VOAudioSource.Play();
    }

    #endregion
}