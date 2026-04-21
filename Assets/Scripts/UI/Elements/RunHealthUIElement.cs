using UnityEngine;
using Digx7.Zygote;

public class RunHealthUIElement : UIElement 
{
    #region Variables =================

    [Header("References")]
    [SerializeField] Transform healthImagesHolder;

    [Header("Incoming Channels")]
    [SerializeField] IntChannel _On_PlayerCurrentHealthUpdate_Channel;

    #endregion

    #region Setup =================

    private void OnEnable() 
    {
        _On_PlayerCurrentHealthUpdate_Channel.channelEvent.AddListener(OnRecieve_OnPlayerCurrentHealthUpdate);
    }

    private void OnDisable() 
    {
        _On_PlayerCurrentHealthUpdate_Channel.channelEvent.RemoveListener(OnRecieve_OnPlayerCurrentHealthUpdate);
    }

    #endregion

    #region Channel Response Methods ======================

    public void OnRecieve_OnPlayerCurrentHealthUpdate(int currentHealth)
    {
        Debug.Log($"RunHealthUIElement: Recieved currentHealth: {currentHealth}");
        
        foreach (Transform child in healthImagesHolder)
        {
            if(child.GetSiblingIndex() < currentHealth)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    #endregion
}