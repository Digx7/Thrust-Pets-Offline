using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class GameEndConditionChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private GameEndConditionChannel channelToRaise;
        [SerializeField] private GameEndCondition _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(GameEndCondition data)
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
