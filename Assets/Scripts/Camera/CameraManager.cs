using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class CameraManager : MonoBehaviour
    {
        #region Variables ================================
        
        [Header("Variables")]
        [SerializeField] protected int _ID = 1;
        [SerializeField] PlayerController _connectedPlayerController;
        [SerializeField] PlayerCharacter _playerCharacter;


        [SerializeField] private bool _runSetupOnEnable = true;
        [SerializeField] private PlayerController _controllerToConnectToOnEnable;
        [SerializeField] private PlayerCharacter _playerCharacterToConnectToOnEnable;

        // [Header("Incoming Channels")]

        [Header("Outgoing Events")]
        public IntEvent OnCameraManagerFinishedSetupEvent;
        #endregion

        #region Setup ================================

        protected virtual void OnEnable()
        {
            if(_runSetupOnEnable)Setup(_ID, _controllerToConnectToOnEnable, _playerCharacterToConnectToOnEnable);
        }

        protected virtual void OnDisable()
        {
            Teardown();
        }

        public virtual void Setup(int newID = 1, PlayerController controllerToConnectTo = null, PlayerCharacter newPlayerCharacter = null)
        {
            SetID(newID);
            ConnectToPlayerController(controllerToConnectTo);
            ConnectToPlayerCharacter(newPlayerCharacter);
            OnCameraManagerFinishedSetupEvent?.Invoke(_ID);
        }

        protected virtual void Teardown()
        {

        }

        #endregion

        #region Main Functions ================================

        public bool ConnectToPlayerController(PlayerController newPlayerController)
        {
            if(!IsPlayerControllerValid(newPlayerController)) return false;

            if(newPlayerController == _connectedPlayerController) return true;

            if(newPlayerController.ConnectCameraManager(this))
            {
                _connectedPlayerController = newPlayerController;
                return true;
            }

            Debug.LogWarning("The CameraManager: " + this + " failed to connect to PlayerController, because it is connecte to another CameraManager.  If this was intentional use ForceConnectToPlayerController instead");

            return false;
        }

        public void ForceConnectToPlayerController(PlayerController newPlayerController)
        {
            if(!IsPlayerControllerValid(newPlayerController)) return;
            if(newPlayerController == _connectedPlayerController) return;

            newPlayerController.ForceConnectCameraManager(this);
            _connectedPlayerController = newPlayerController;
        }

        public bool ConnectToPlayerCharacter(PlayerCharacter newPlayerCharacter)
        {
            if(!IsPlayerCharacterValid(newPlayerCharacter)) return false;

            if(newPlayerCharacter == _playerCharacter) return true;

            if(newPlayerCharacter.ConnectCameraManager(this))
            {
                _playerCharacter = newPlayerCharacter;
                return true;
            }

            Debug.LogWarning("The CameraManager: " + this + " failed to connect to PlayerController, because it is connecte to another CameraManager.  If this was intentional use ForceConnectToPlayerController instead");

            return false;
        }

        public void ForceConnectToPlayerCharacter(PlayerCharacter newPlayerCharacter)
        {
            if(!IsPlayerCharacterValid(newPlayerCharacter)) return;
            if(newPlayerCharacter == _playerCharacter) return;
            
            newPlayerCharacter.ForceConnectCameraManager(this);
            _playerCharacter = newPlayerCharacter;
        }

        public void SetID(int newID)
        {
            if(_ID == newID) return;
            
            _ID = newID;
            if(IsPlayerControllerValid(_connectedPlayerController)) _connectedPlayerController.SetID(_ID);
            if(IsPlayerCharacterValid(_playerCharacter)) _playerCharacter.SetID(_ID);
        }

        private bool IsPlayerControllerValid(PlayerController playerController)
        {
            if(playerController == null) return false;
            else return true;
        }

        private bool IsPlayerCharacterValid(PlayerCharacter playerCharacter)
        {
            if(playerCharacter == null) return false;
            else return true;
        }

        #endregion

        

    }
}
