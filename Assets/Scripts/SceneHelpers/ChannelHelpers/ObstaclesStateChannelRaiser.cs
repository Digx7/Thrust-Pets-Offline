using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ObstaclesStateChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private ObstaclesStateChannel channelToRaise;
        [SerializeField] private ObstaclesState _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(ObstaclesState data)
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
