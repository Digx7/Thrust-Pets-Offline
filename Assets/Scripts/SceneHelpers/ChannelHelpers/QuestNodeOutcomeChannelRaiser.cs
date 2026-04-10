using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class QuestNodeOutcomeChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private QuestNodeOutcomeChannel channelToRaise;
        [SerializeField] private QuestNodeOutcome _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(QuestNodeOutcome data)
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


