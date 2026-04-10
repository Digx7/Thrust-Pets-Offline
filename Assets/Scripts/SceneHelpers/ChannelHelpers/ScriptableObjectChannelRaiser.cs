using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ScriptableObjectChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private ScriptableObjectChannel channelToRaise;
        [SerializeField] private ScriptableObject m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(ScriptableObject data)
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


