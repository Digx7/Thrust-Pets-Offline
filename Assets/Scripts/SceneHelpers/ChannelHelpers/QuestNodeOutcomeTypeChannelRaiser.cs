using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class QuestNodeOutcomeTypeChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private QuestNodeOutcomeTypeChannel channelToRaise;
        [SerializeField] private QuestNodeOutcomeType _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(QuestNodeOutcomeType data)
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


