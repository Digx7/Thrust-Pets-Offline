using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class Vector2IntChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private Vector2IntChannel channelToRaise;
        [SerializeField] private Vector2Int m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(Vector2Int data)
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


