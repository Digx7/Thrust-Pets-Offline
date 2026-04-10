using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class IntChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private IntChannel channelToRaise;
        [SerializeField] private int m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(int data)
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


