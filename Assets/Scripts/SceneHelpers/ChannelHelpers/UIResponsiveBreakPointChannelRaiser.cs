using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class UIResponsiveBreakPointChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private UIResponsiveBreakPointChannel channelToRaise;
        [SerializeField] private UIResponsiveBreakPoint _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(UIResponsiveBreakPoint data)
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
