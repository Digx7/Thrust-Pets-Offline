using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ConversationNodeChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private ConversationNodeChannel channelToRaise;
        [SerializeField] private ConversationNode _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(ConversationNode data)
        {
            channelToRaise.Raise(data);
        }

        public void Raise()
        {
            channelToRaise.Raise(_data);
        }

        #endregion
    }
}


