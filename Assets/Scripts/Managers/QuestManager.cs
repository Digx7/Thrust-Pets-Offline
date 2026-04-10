using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    public class QuestManager : Singleton<QuestManager>
    {
        #region Variables ================================
        
        [Header("Variables")]
        [SerializeField] List<QuestData> _activeQuests;

        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Quest")]
        [SerializeField] QuestDataChannel _request_receiveQuest_Channel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Quest")]
        [SerializeField] QuestObjectiveProgressChannel _request_tryProgressQuest_Channel;

        // [Header("Outgoing Events")]

        #endregion

        #region Setup ================================

        public override void SafeOnEnable()
        {
            _request_receiveQuest_Channel.channelEvent.AddListener(OnRecieve_GiveQuest);
            _request_tryProgressQuest_Channel.channelEvent.AddListener(OnRecieve_TryProgressQuest);
        }

        public override void SafeOnDisable()
        {
            _request_receiveQuest_Channel.channelEvent.RemoveListener(OnRecieve_GiveQuest);
            _request_tryProgressQuest_Channel.channelEvent.RemoveListener(OnRecieve_TryProgressQuest);
        }

        #endregion

        #region Channel Responses ================================

        protected void OnRecieve_GiveQuest(QuestData newQuest)
        {
            GiveQuest(newQuest);
        }

        protected void OnRecieve_TryProgressQuest(QuestObjectiveProgress progress)
        {
            TryProgressQuest(progress);
        }

        #endregion

        #region Main Functions ================================

        public void GiveQuest(QuestData newQuest)
        {
            if(_activeQuests.Contains(newQuest))
            {
                Debug.Log("QuestManager: Something tried to give the quest " + newQuest.questName + "\nBut that quest is already active.");
                return;
            }

            newQuest.ResetQuest();
            _activeQuests.Add(newQuest);

            Debug.Log("QuestManager: Added quest: " + newQuest.ToString());
        }

        public void TryProgressQuest(QuestObjectiveProgress progress)
        {
            int indexOfFinishedQuest = -1;
            
            for (int i = 0; i < _activeQuests.Count; i++)
            {
                if(_activeQuests[i].TryProgress(progress))
                {
                    if(_activeQuests[i].IsComplete())
                    {
                        indexOfFinishedQuest = i;
                    }
                    else
                    {
                        Debug.Log("QuestManager: Progressed a quest but did not finish it");
                    }
                }
            }

            if(indexOfFinishedQuest != -1)
            {
                FinishQuestAtIndex(indexOfFinishedQuest);
            }
        }

        private void FinishQuestAtIndex(int index)
        {
            QuestData finishedQuest = _activeQuests[index];
            finishedQuest.Finish();

            Debug.Log("QuestManager: Finished Quest: " + finishedQuest.ToString());
            _activeQuests.RemoveAt(index);
        }

        #endregion
    }
}