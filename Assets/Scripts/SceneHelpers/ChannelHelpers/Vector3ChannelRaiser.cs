using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class Vector3ChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private Vector3Channel channelToRaise;
        [SerializeField] private Vector3 m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(Vector3 data)
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


