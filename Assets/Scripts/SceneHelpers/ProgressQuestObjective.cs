using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    public class ProgressQuestObjective : MonoBehaviour
    {
        #region Variables ================================
        [Header("Variables")]
        public QuestObjectiveProgress quest;
        [Header("Outgoing Channels")]
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Quests")]
        public QuestObjectiveProgressChannel giveQuestChannel;

        #endregion

        #region Main Functions ================================
        public void ProgressQuest()
        {
            giveQuestChannel.Raise(quest);
        }
        #endregion
    }
}