using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class breakPointDataAndUnityEventPairChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private breakPointDataAndUnityEventPairChannel channelToRaise;
        [SerializeField] private breakPointDataAndUnityEventPair _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(breakPointDataAndUnityEventPair data)
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
