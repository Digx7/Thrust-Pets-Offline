using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class InstantiatedObjectChannelListener : MonoBehaviour 
    {
        #region Variables ==============================================
        [SerializeField] private InstantiatedObjectChannel channelToListenTo;

        public InstantiatedObjectEvent onChannelRaised;

        public bool checkLastValueOnStart;
        public bool shouldFilterValue = false;
        public bool shouldPassHeardDataThrough = true;

        public InstantiatedObject filter;
        public InstantiatedObject outgoingDataIfNotPassHeardDataThrough;
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

        public void OnHearChannel(InstantiatedObject data)
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

        public void SendOutResponse(InstantiatedObject incomingData)
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