using UnityEngine;
using UnityEngine.Events;

namespace Digx7.ThrustPets
{
    public class ThrustPetGameModeDataChannelListener : MonoBehaviour 
    {
        #region Variables ==============================================
        [SerializeField] private ThrustPetGameModeDataChannel channelToListenTo;

        public ThrustPetGameModeDataEvent onChannelRaised;

        public bool checkLastValueOnStart;
        public bool shouldFilterValue = false;
        public bool shouldPassHeardDataThrough = true;

        public ThrustPetGameModeData filter;
        public ThrustPetGameModeData outgoingDataIfNotPassHeardDataThrough;
        #endregion

        #region Setup ==============================================

        private void Start()
        {
            if (checkLastValueOnStart) OnHearChannel(channelToListenTo.lastValue);
        }

        private void OnEnable()
        {
            channelToListenTo.channelEvent.AddListener(OnHearChannel);
        }

        private void OnDisable()
        {
            channelToListenTo.channelEvent.RemoveListener(OnHearChannel);
        }

        #endregion

        #region Channel Response Functions ==============================================

        public void OnHearChannel(ThrustPetGameModeData data)
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

        public void SendOutResponse(ThrustPetGameModeData incomingData)
        {
            if(shouldPassHeardDataThrough) 
            {
                onChannelRaised.Invoke(incomingData);
            }
            else
            {
                onChannelRaised.Invoke(outgoingDataIfNotPassHeardDataThrough);
            }
        }

        #endregion
    }
}