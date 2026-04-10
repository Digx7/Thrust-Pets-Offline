using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class BooleanChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private BooleanChannel channelToRaise;
        [SerializeField] private bool m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(bool data)
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


