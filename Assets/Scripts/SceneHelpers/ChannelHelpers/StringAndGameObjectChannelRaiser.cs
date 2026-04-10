using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class StringAndGameObjectChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private StringAndGameObjectChannel channelToRaise;
        [SerializeField] private StringAndGameObject _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(StringAndGameObject data)
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


