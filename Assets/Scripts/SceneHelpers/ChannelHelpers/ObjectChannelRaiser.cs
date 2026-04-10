using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ObjectChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private ObjectChannel channelToRaise;
        [SerializeField] private Object m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(Object data)
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


