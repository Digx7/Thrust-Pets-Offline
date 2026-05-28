using UnityEngine;
using UnityEngine.Events;

namespace Digx7.ThrustPets
{
    public class ObstacleDataChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private ObstacleDataChannel channelToRaise;
        [SerializeField] private ObstacleData _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(ObstacleData data)
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
