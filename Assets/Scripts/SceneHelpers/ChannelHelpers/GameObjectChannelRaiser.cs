using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class GameObjectChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private GameObjectChannel channelToRaise;
        [SerializeField] private GameObject m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(GameObject data)
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
