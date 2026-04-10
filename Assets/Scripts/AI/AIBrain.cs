using UnityEngine;

namespace Digx7.Zygote
{
    public class AIBrain : MonoBehaviour
    {
        #region Variables ================================
        [Header("Variables")]
        [SerializeField] protected int _ID;
        [SerializeField] protected AIController _aIController;

        // [Header("Channels")]

        [Header("Outgoing Events")]
        public IntEvent OnAIBrainFinishedSetupEvent;

        #endregion

        #region Main Functions ================================
        public void SetID(int newID)
        {
            if(newID == _ID) return;

            _ID = newID;
            if(_aIController != null)_aIController.SetID(newID);
        }

        public bool ConnectAIController(AIController newAIController)
        {
            if(!IsAIControllerValid(newAIController)) return false;

            if(newAIController == _aIController) return true;
            
            if(_aIController != null)
            {
                Debug.LogWarning("The AIBrain: " + newAIController + " tried to connect to the AIController " + this + " but it is already connected to the AIBrain: " + _aIController + ".  If this was intentional use ForceConnectAIBrain instead");
                return false;
            }

            _aIController = newAIController;
            _aIController.SetID(_ID);
            return true;
        }

        public void ForceConnectAIController(AIController newAIController)
        {
            if(!IsAIControllerValid(newAIController))return;
            if(newAIController == _aIController)return;

            _aIController = newAIController;
            _aIController.SetID(_ID);
        }

        private bool IsAIControllerValid(AIController newAIController)
        {
            if(newAIController == null) return false;
            else return true;
        }

        protected virtual void OnDisable()
        {
            Teardown();
        }

        public void Setup(int newID, AIController newAIController = null)
        {
            ConnectAIController(newAIController);
            SetID(newID);
            OnAIBrainFinishedSetupEvent.Invoke(_ID);
        }

        protected virtual void Teardown()
        {
            
        }

        #endregion
    }
}
