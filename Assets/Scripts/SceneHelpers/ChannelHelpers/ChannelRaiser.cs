using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private Channel channelToRaise;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise()
        {
            channelToRaise.Raise();
        }

        #endregion
    }
}


