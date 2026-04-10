using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class SceneDataChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private SceneDataChannel channelToRaise;
        [SerializeField] private SceneData _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(SceneData data)
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


