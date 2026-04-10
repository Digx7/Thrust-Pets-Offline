using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class PlayerSpawnInfoChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private PlayerSpawnInfoChannel channelToRaise;
        [SerializeField] private PlayerSpawnInfo _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(PlayerSpawnInfo data)
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


