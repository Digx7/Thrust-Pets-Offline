using UnityEngine;
using System.Collections;
using TMPro;

namespace Digx7.Zygote
{
    public class DialogueBoxWidget : UIWidget
    {
        #region Variables ================================

        [Header("Variables")]
        public float typingSpeed;
        public TextMeshProUGUI speakerNameTextMeshPro;
        public TextMeshProUGUI dialogueTextMeshPro;

        [Header("Incoming Channels")]
        public ConversationNodeChannel onConversationUpdateChannel;

        private ConversationNode currentNode;
        private bool isTyping = false;

        #endregion

        #region Setup ================================
        
        public override void Setup(UIWidgetData newUIWidgetData)
        {
            onConversationUpdateChannel.channelEvent.AddListener(Render);
            base.Setup(newUIWidgetData);
        }

        public override void Teardown()
        {
            onConversationUpdateChannel.channelEvent.RemoveListener(Render);
            base.Teardown();
        }

        #endregion

        #region Main Functions ================================

        public void Render(ConversationNode latestNode)
        {
            currentNode = latestNode;

            speakerNameTextMeshPro.text = currentNode.speaker;
            if(isTyping) StopAllCoroutines();
            StartCoroutine(TypeOutLine());
        }

        IEnumerator TypeOutLine()
        {
            isTyping = true;
            dialogueTextMeshPro.text = "";
            int characterIndex = 0;

            while(characterIndex < currentNode.line.Length)
            {
                dialogueTextMeshPro.text += currentNode.line[characterIndex];
                yield return new WaitForSeconds(1f / typingSpeed);
                characterIndex++;
            }
            isTyping = false;
        }

        #endregion
    }
}
