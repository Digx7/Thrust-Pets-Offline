using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ChannelListener : MonoBehaviour 
    {
        #region Variables ==============================================
        [SerializeField] private Channel channelToListenTo;

        public UnityEvent onChannelRaised;
        #endregion

        #region Setup ==============================================

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

        public void OnHearChannel()
        {
            SendOutResponse();
        }

        #endregion

        #region Main Functions ==============================================

        public void SendOutResponse()
        {
            onChannelRaised.Invoke();
        }

        #endregion
    }
}

