using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ConversationHolder : MonoBehaviour
    {
        #region Variables ================================

        [Header("Variables")]
        public Conversation conversation;
        public UIWidgetData dialogueWidgetData;

        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/UI")]
        public UIWidgetDataChannel requestLoadDialogueWidgetChannel;
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/UI")]
        public UIWidgetDataChannel requestUnloadDialogueWidgetChannel;

        [Header("Outgoing Events")]
        public ConversationNodeEvent OnConversationUpdateEvent;
        public UnityEvent OnConversationEndEvent;

        private int currentNodeIndex = 0;
        private ConversationNode currentNode;
        private bool isConversationGoing = false;

        #endregion

        #region Main Functions ================================

        public void Interact()
        {
            Debug.Log("ConversationHolder: Interact()");

            if(!isConversationGoing)
            {
                isConversationGoing = true;
                StartConversation();
            }
            else
            {
                ProgressConversation();
            }
        }

        private void StartConversation()
        {
            Debug.Log("ConversationHolder: StartConversation()");

            currentNodeIndex = 0;
            if(TryGetNode())
            {
                requestLoadDialogueWidgetChannel.Raise(dialogueWidgetData);
                OnConversationUpdateEvent.Invoke(currentNode);

                currentNode.Print();
            }
            else
            {
                EndConversation();
            }
        }

        private void ProgressConversation()
        {
            Debug.Log("ConversationHolder: ProgressConversation()");
            
            currentNodeIndex++;
            if(TryGetNode())
            {
                OnConversationUpdateEvent.Invoke(currentNode);

                currentNode.Print();
            }
            else
            {
                EndConversation();
            }
        }

        private void EndConversation()
        {
            Debug.Log("ConversationHolder: EndConversation()");
            
            isConversationGoing = false;
            requestUnloadDialogueWidgetChannel.Raise(dialogueWidgetData);
            OnConversationEndEvent.Invoke();
            TryLoadNextConversation();
        }

        private bool TryGetNode()
        {
            if(currentNodeIndex < conversation.nodes.Count)
            {
                currentNode = conversation.nodes[currentNodeIndex];
                return true;
            }
            return false;
        }

        private bool TryLoadNextConversation()
        {
            if(conversation.nextConversationToLoadOnFinish != null)
            {
                conversation = conversation.nextConversationToLoadOnFinish;
                return true;
            }
            return false;
        }

        #endregion
    }
}