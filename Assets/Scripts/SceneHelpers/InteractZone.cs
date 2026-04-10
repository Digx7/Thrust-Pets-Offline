using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    [RequireComponent(typeof(BoxCollider))]
    public class InteractZone : MonoBehaviour
    {
        #region Variables ================================

        [Header("Variables")]
        public string playerTag = "Player";

        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/Zygote/ScriptableObjects/Channels/Players")]
        public Channel onPlayerTryInteractChannel;

        [Header("Outgoing Events")]
        public UnityEvent onPlayerEnter;
        public UnityEvent onPlayerLeave;
        public UnityEvent onPlayerInteract;

        private bool isPlayerInZone = false;

        #endregion

        #region Setup ================================

        private void OnEnable()
        {
            onPlayerTryInteractChannel.channelEvent.AddListener(TryInteract);
        }

        private void OnDisable()
        {
            onPlayerTryInteractChannel.channelEvent.RemoveListener(TryInteract);
        }

        #endregion

        #region Main Functions ================================

        public void TryInteract()
        {
            if(isPlayerInZone) 
            {
                onPlayerInteract.Invoke();
                Debug.Log("InteractZone: Interact");
            }
        }

        public void OnTriggerEnter(Collider col)
        {
            if(col.tag == playerTag) 
            {
                Debug.Log("InteractZone: Player Entered Zone");
                onPlayerEnter.Invoke();
                isPlayerInZone = true;
            }
        }

        public void OnTriggerExit(Collider col)
        {
            if(col.tag == playerTag) 
            {
                Debug.Log("InteractZone: Player Left Zone");
                onPlayerLeave.Invoke();
                isPlayerInZone = false;
            }
        }

        #endregion
    }
}