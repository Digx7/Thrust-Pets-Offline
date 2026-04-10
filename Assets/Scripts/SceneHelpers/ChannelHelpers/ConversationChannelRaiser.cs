using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ConversationChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private ConversationChannel channelToRaise;
        [SerializeField] private Conversation m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(Conversation data)
        {
            channelToRaise.Raise(data);
        }

        public void Raise()
        {
            channelToRaise.Raise(m_data);
        }

        #endregion
    }
}


