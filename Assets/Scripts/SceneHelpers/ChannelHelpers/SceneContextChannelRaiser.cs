using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class SceneContextChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private SceneContextChannel channelToRaise;
        [SerializeField] private SceneContext _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(SceneContext data)
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


