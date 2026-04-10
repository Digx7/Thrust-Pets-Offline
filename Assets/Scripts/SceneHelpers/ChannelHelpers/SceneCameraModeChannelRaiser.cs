using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class SceneCameraModeChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private SceneCameraModeChannel channelToRaise;
        [SerializeField] private SceneCameraMode _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(SceneCameraMode data)
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


