using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class QuestDataChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private QuestDataChannel channelToRaise;
        [SerializeField] private QuestData m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(QuestData data)
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


