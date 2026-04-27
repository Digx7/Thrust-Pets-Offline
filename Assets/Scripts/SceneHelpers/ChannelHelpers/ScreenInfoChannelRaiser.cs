using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ScreenInfoChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private ScreenInfoChannel channelToRaise;
        [SerializeField] private ScreenInfo _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(ScreenInfo data)
        {
            channelToRaise.Raise(data);
        }

        public void Raise()
        {
            channelToRaise.Raise(_data);
        }

        #endregion
    }
}
