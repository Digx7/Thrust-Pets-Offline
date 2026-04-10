using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class SongDataChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private SongDataChannel channelToRaise;
        [SerializeField] private SongData m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(SongData data)
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


