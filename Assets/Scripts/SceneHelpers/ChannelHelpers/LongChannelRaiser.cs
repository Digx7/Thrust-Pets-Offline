using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class LongChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private LongChannel channelToRaise;
        [SerializeField] private long m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(long data)
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


