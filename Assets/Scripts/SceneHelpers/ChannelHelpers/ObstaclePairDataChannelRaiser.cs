using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ObstaclePairDataChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private ObstaclePairDataChannel channelToRaise;
        [SerializeField] private ObstaclePairData _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(ObstaclePairData data)
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
