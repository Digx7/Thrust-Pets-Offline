using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class DoubleChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private DoubleChannel channelToRaise;
        [SerializeField] private double m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(double data)
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


