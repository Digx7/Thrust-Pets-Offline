using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class StringChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private StringChannel channelToRaise;
        [SerializeField] private string m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(string data)
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


