using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class QuestObjectiveChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private QuestObjectiveChannel channelToRaise;
        [SerializeField] private QuestObjective m_data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(QuestObjective data)
        {
            channelToRaise.Raise(data);
        }

        public void Raise()
        {
            channelToRaise.Raise(m_data);
        }

        #endregion
    }
}


