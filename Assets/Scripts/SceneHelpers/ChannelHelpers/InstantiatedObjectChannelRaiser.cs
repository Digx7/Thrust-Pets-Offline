using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class InstantiatedObjectChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private InstantiatedObjectChannel channelToRaise;
        [SerializeField] private InstantiatedObject _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(InstantiatedObject data)
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
