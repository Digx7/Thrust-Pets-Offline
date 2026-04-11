using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class CameraManager : MonoBehaviour
    {
        #region Variables ================================
        
        [Header("Variables")]
        [SerializeField] protected int _ID = 1;
        [SerializeField] protected GameObject _cameraPrefab;
        [SerializeField] Camera _camera;
        [SerializeField] Vector3 _cameraOffset;
        public Camera Camera => _camera;
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
            // FindMainCamera();
            CreateCamera();
            ConnectToPlayerController(controllerToConnectTo);
            ConnectToPlayerCharacter(newPlayerCharacter);
            PositionCamera(_playerCharacter, _cameraOffset);
            OnCameraManagerFinishedSetupEvent?.Invoke(_ID);
        }

        protected virtual void Teardown()
        {

        }

        #endregion

        #region Main Functions ================================

        public bool FindMainCamera()
        {
            if(_camera != null) return true;

            _camera = Camera.main;
            if(_camera != null) return true;

            Debug.LogWarning("The CameraManager: " + this + " failed to find a main camera in the scene.  Make sure there is a camera with the tag MainCamera in the scene, or assign a camera to the CameraManager directly.");

            return false;
        }

        public void CreateCamera()
        {
            if(_camera != null) return;
            if(_cameraPrefab == null)
            {
                Debug.LogWarning("The CameraManager: " + this + " failed to create a camera because the camera prefab is not assigned.  Please assign a camera prefab to the CameraManager.");
                return;
            }

            GameObject newCamera = Instantiate(_cameraPrefab, transform);
            _camera = newCamera.GetComponent<Camera>();
            if(_camera == null)
            {
                Debug.LogWarning("The CameraManager: " + this + " failed to create a camera because the camera prefab does not have a Camera component.  Please make sure the camera prefab has a Camera component.");
                return;
            }
        }

        public void PositionCamera(PlayerCharacter playerCharacter, Vector3 offset)
        {
            if(!IsPlayerCharacterValid(playerCharacter)) return;
            if(_camera == null) return;

            Debug.Log($"CameraManager: PositionCamera: Positioning camera: {Camera} to playerCharacter: {playerCharacter} with offset: {offset}");

            _camera.transform.SetParent(playerCharacter.transform);
            _camera.transform.localPosition = offset;
        }

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
