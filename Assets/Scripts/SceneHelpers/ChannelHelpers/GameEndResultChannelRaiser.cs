using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class GameEndResultChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private GameEndResultChannel channelToRaise;
        [SerializeField] private GameEndResult _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(GameEndResult data)
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
