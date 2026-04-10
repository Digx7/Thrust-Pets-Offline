using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class QuestObjectiveProgressChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private QuestObjectiveProgressChannel channelToRaise;
        [SerializeField] private QuestObjectiveProgress _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(QuestObjectiveProgress data)
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


