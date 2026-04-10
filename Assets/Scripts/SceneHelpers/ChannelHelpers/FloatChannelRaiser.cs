using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class FloatChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private FloatChannel channelToRaise;
        [SerializeField] private float m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(float data)
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


