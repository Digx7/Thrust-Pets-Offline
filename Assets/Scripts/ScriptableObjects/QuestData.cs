using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewQuestData", menuName = "ScriptableObjects/Data/QuestData", order = 1)]
    public class QuestData : ScriptableObject
    {
        #region Variables ==============================================
        public string questName;
        public List<QuestNode> nodes;

        private int nodeIndex = 0;
        #endregion

        #region Main Functions ==============================================


        public void ResetQuest()
        {
            Debug.Log("QuestData - " + questName + " : ResetQuest()");
            
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Reset();
            }
            
            nodeIndex = 0;
        }

        public bool IsComplete()
        {
            bool value;
            if(nodeIndex >= nodes.Count) value = true;
            else value = false;

            Debug.Log("QuestData - " + questName + " : IsComplete() => " + value);
            return value;
        }

        public bool TryProgress(QuestObjectiveProgress progress)
        {
        bool value;
        
        if(IsComplete()) 
            {
                value = false;
                Debug.Log("QuestData - " + questName + " : TryProgress() => " + value);
                return value;
            }

        if(nodes[nodeIndex].TryProgress(progress))
        {
                if(nodes[nodeIndex].AllObjectivesMet()) 
                {
                    nodeIndex++;
                    Debug.Log("QuestData - " + questName + " : Increasing nodeIndex to " + nodeIndex);
                }
                value = true;
        }
        else value = false;

        Debug.Log("QuestData - " + questName + " : TryProgress() => " + value);
        return value;
        }

        public void Finish()
        {
            Debug.Log("QuestData - " + questName + " : Finish()");
        }

        public override string ToString()
        {
            return questName;
        }
        #endregion
    }

    [System.Serializable]
    public class QuestNode
    {
        #region Variables ==============================================
        public string nodeName;
        public List<QuestObjective> objectives;
        #endregion

        #region Main Functions ==============================================

        public bool AllObjectivesMet()
        {
            bool value;

            for (int i = 0; i < objectives.Count; i++)
            {
                if(objectives[i].IsMet == false) 
                {
                    value = false;
                    Debug.Log("QuestNode - " + nodeName + " : AllObjectivesMet() => " + value);
                    return value;
                }
            }

            value = true;
            Debug.Log("QuestNode - " + nodeName + " : AllObjectivesMet() => " + value);
            return value;
        }

        public void Reset()
        {
            Debug.Log("QuestNode - " + nodeName + " : Reset()");
            for (int i = 0; i < objectives.Count; i++)
            {
                objectives[i].Reset();
            }
        }

        public bool TryProgress(QuestObjectiveProgress progress)
        {
            bool value;

            for (int i = 0; i < objectives.Count; i++)
            {
                if(objectives[i].TryProgress(progress))
                {
                    value = true;
                    Debug.Log("QuestNode - " + nodeName + " : TryProgress() => " + value);
                    return value;
                }
            }

            value = false;
            Debug.Log("QuestNode - " + nodeName + " : TryProgress() => " + value);
            return value;
        }

        #endregion

    }

    [System.Serializable]
    public class QuestObjective
    {
        #region Variables ==============================================
        public string objectiveName;
        public string data;
        public int amount;
        public int CurrentAmount = 0;
        public bool IsMet = false;
        #endregion
        
        #region Main Functions ==============================================

        public bool TryProgress(QuestObjectiveProgress progress)
        {
            bool value;
            
            if(progress.objectiveName == objectiveName)
            {
                CurrentAmount += progress.addedAmount;

                if(amount > 0)
                {
                    if(HasMetXGoal()) IsMet = true;
                }
                else IsMet = true;

                value = true;
                Debug.Log("QuestObjective - " + objectiveName + ": TryProgress() => " + value + "\nIsMet: " + IsMet);
                return value;
            }
            value = false;
            Debug.Log("QuestObjective - " + objectiveName + ": TryProgress() => " + value + "\nIsMet: " + IsMet);
            return value;
        }

        private bool HasMetXGoal()
        {
            bool value;

            if(CurrentAmount >= amount) value = true;
            else value = false;

            Debug.Log("QuestObjective - " + objectiveName + ": HasMetXGoal() => " + value);
            return value;
        }

        public void Reset()
        {
            Debug.Log("QuestObjective - " + objectiveName + ": Reset()");
            
            IsMet = false;
            CurrentAmount = 0;
        }

        #endregion
    }
}