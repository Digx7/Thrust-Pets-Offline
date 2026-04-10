using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class PlayerCharacter : Character
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] protected CameraManager cameraManager;

        // [Heder("Incoming Channels")]

        [Header("Outgoing Events")]
        public IntEvent OnPlayerCharacterFinishedSetupEvent;
        public UnityEvent OnPlayerTryInteractEvent;

        protected Vector2 desiredMoveDirection;

        #endregion

        #region Setup ================================

        protected override void OnEnable(){}
        
        public override void Setup(int newID = 0)
        {
            base.Setup(newID);

            OnPlayerCharacterFinishedSetupEvent?.Invoke(ID);
        }

        protected override void Start()
        {
            desiredMoveDirection = new Vector2();
        }

        #endregion

        #region Main Functions ================================

        public bool ConnectCameraManager(CameraManager newCameraManager)
        {
            if(!IsCameraManagerValid(newCameraManager)) return false;

            if(newCameraManager == cameraManager) return true;
            
            if(cameraManager != null)
            {
                Debug.LogWarning("The CameraManager: " + newCameraManager + " tried to connect to the PlayerCharacter " + this + " but it is already connected to the CameraManager: " + cameraManager + ".  If this was intentional use ForceConnectCameraManager instead");
                return false;
            }

            cameraManager = newCameraManager;
            return true;
        }

        public void ForceConnectCameraManager(CameraManager newCameraManager)
        {
            if(!IsCameraManagerValid(newCameraManager)) return;
            if(newCameraManager == cameraManager) return;
            
            cameraManager = newCameraManager;
        }

        private bool IsCameraManagerValid(CameraManager newCameraManager)
        {
            if(newCameraManager == null) return false;
            else return true;
        }

        public virtual void UpdateDesiredMoveDirection(Vector2 newDesiredDirection)
        {
            desiredMoveDirection = newDesiredDirection;
        }

        public virtual void Jump()
        {

        }

        public virtual void Fire1()
        {
            Debug.Log("PlayerCharacter: Fire1()");
            OnPlayerTryInteractEvent.Invoke();

        }

        public virtual void Fire2()
        {

        }

        #endregion
        

    }
}