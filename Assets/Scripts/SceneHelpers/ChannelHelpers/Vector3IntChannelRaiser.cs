using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class Vector3IntChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private Vector3IntChannel channelToRaise;
        [SerializeField] private Vector3Int m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(Vector3Int data)
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


