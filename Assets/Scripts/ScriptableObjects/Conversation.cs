using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewConversation", menuName = "ScriptableObjects/Dialogue/Conversation", order = 1)]
    public class Conversation : ScriptableObject
    {
        #region Variables ==============================================
        public List<ConversationNode> nodes;

        public Conversation nextConversationToLoadOnFinish;
        #endregion
    }
}