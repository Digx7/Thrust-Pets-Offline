using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class GameObjectAndWeightChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private GameObjectAndWeightChannel channelToRaise;
        [SerializeField] private GameObjectAndWeight _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(GameObjectAndWeight data)
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
