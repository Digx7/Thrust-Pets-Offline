using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class UIWidgetDataChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private UIWidgetDataChannel channelToRaise;
        [SerializeField] private UIWidgetData m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(UIWidgetData data)
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


