using UnityEngine;
using UnityEngine.Events;
using Digx7.Zygote;

namespace Digx7.ThrustPets
{
    public class PowerUpDataChannelListener : MonoBehaviour 
    {
        #region Variables ==============================================
        [SerializeField] private PowerUpDataChannel channelToListenTo;

        public PowerUpDataEvent onChannelRaised;
        public StringEvent onChannelRaised_DisplayName;

        public bool checkLastValueOnStart;
        public bool checkLastValueOnEnable;
        public bool shouldFilterValue = false;
        public bool shouldPassHeardDataThrough = true;

        public PowerUpData filter;
        public PowerUpData outgoingDataIfNotPassHeardDataThrough;
        #endregion

        #region Setup ==============================================

        private void Start()
        {
            if (checkLastValueOnStart && channelToListenTo.lastValue != null) 
            {
                OnHearChannel(channelToListenTo.lastValue);
            }
        }

        private void OnEnable()
        {
            channelToListenTo.channelEvent.AddListener(OnHearChannel);

            if (checkLastValueOnEnable && channelToListenTo.lastValue != null) 
            {
                OnHearChannel(channelToListenTo.lastValue);
            }
        }

        private void OnDisable()
        {
            channelToListenTo.channelEvent.RemoveListener(OnHearChannel);
        }

        #endregion

        #region Channel Response Functions ==============================================

        public void OnHearChannel(PowerUpData data)
        {
            if(shouldFilterValue)
            {
                if(data == filter)
                {
                    SendOutResponse(data);
                }
            }
            else
            {
                SendOutResponse(data);
            }
        }

        #endregion

        #region Main Functions ==============================================

        public void SendOutResponse(PowerUpData incomingData)
        {
            if(shouldPassHeardDataThrough) 
            {
                onChannelRaised.Invoke(incomingData);
                onChannelRaised_DisplayName.Invoke(incomingData.DisplayName);
            }
            else
            {
                onChannelRaised.Invoke(outgoingDataIfNotPassHeardDataThrough);
                onChannelRaised_DisplayName.Invoke(outgoingDataIfNotPassHeardDataThrough.DisplayName);
            }
        }

        #endregion
    }
}