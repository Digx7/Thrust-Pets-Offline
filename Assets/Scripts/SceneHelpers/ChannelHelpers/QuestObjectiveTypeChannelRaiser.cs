using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class QuestObjectiveTypeChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private QuestObjectiveTypeChannel channelToRaise;
        [SerializeField] private QuestObjectiveType _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(QuestObjectiveType data)
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


