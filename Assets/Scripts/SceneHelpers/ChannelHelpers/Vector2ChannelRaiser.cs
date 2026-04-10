using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class Vector2ChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private Vector2Channel channelToRaise;
        [SerializeField] private Vector2 m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(Vector2 data)
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


