using UnityEngine;
using Digx7.Zygote;
using TMPro;

public class RunTimerUIElement : MonoBehaviour 
{
    #region Variables =================

    [Header("References")]
    [SerializeField] TextMeshProUGUI timerTMPro;

    [Header("Incoming Channels")]
    [SerializeField] FloatChannel on_TimerChanged_Channel;

    #endregion

    #region Setup =================

    private void OnEnable() 
    {
        on_TimerChanged_Channel.channelEvent.AddListener(OnRecieve_OnTimerChanged);
        OnRecieve_OnTimerChanged(on_TimerChanged_Channel.lastValue);
    }

    private void OnDisable() 
    {
        on_TimerChanged_Channel.channelEvent.RemoveListener(OnRecieve_OnTimerChanged);
    }

    #endregion

    #region Channel Response Methods ======================

    public void OnRecieve_OnTimerChanged(float newTimer)
    {
        // TODO: Format the timer value into minutes and seconds
        int minutes = Mathf.FloorToInt(newTimer / 60f);
        int seconds = Mathf.FloorToInt(newTimer % 60f);

        if(minutes < 0) minutes = 0;
        if(seconds < 0) seconds = 0;

        timerTMPro.text = $"{minutes:00}:{seconds:00}";
    }

    #endregion
}