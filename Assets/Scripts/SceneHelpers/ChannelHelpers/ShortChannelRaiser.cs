using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ShortChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private ShortChannel channelToRaise;
        [SerializeField] private short m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(short data)
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


