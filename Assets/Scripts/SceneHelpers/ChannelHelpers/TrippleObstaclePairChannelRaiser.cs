using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class TrippleObstaclePairChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private TrippleObstaclePairChannel channelToRaise;
        [SerializeField] private TrippleObstaclePair _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(TrippleObstaclePair data)
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
