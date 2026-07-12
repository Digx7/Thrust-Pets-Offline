using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Digx7.Zygote
{
    public class PlayerController : GameController
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] protected CameraManager cameraManager;
        [SerializeField] private UIWidgetData activeTimeLoreWidgetData;
        [SerializeField] private UIWidgetData pauseMenuWidgetData;
        [SerializeField] private PlayerInput playerInput;

        [Header("Incoming Channels")]
        [SerializeField] StringChannel _On_PlayerControlSchemeChanged_Channel;

        // [Header("Incoming Channels")]
        [Header("Outgoing Events")]
        public UIWidgetDataEvent OnRequestLoadUIWidgetDataEvent;
        public UnityEvent OnRequestSelectUIFallBack;
        public StringEvent OnPlayerControlSchemeChangedEvent;

        // private PlayerCharacter possessedPlayer;
        private PlayerCharacter_EndlessRunner possessedPlayer;

        private bool hasTriedToChangeControlScheme = false;

        #endregion

        #region Setup ================================

        protected void Start()
        {
            
            if(_On_PlayerControlSchemeChanged_Channel.lastValue != null && _On_PlayerControlSchemeChanged_Channel.lastValue != "")
            {
                playerInput.SwitchCurrentControlScheme(_On_PlayerControlSchemeChanged_Channel.lastValue);
            }
            
        }

        #endregion

        #region Main Functions ================================

        // OVERRIDE FUNCTIONS ==============================================

        public override bool PossessCharacter(Character newCharacter)
        {
            // possessedPlayer = newCharacter as PlayerCharacter;
            possessedPlayer = newCharacter as PlayerCharacter_EndlessRunner;

            return base.PossessCharacter(newCharacter);
        }

        public override void ForcePossessCharacter(Character newCharacter)
        {
            // possessedPlayer = newCharacter as PlayerCharacter;
            possessedPlayer = newCharacter as PlayerCharacter_EndlessRunner;

            base.ForcePossessCharacter(newCharacter);
        }

        // CAMERA FUNCTIONS ===================================================

        public bool ConnectCameraManager(CameraManager newCameraManager)
        {
            if(!IsCameraManagerValid(newCameraManager)) return false;

            if(newCameraManager == cameraManager) return true;
            
            if(cameraManager != null)
            {
                Debug.LogWarning("The CameraManager: " + newCameraManager + " tried to connect to the PlayerController " + this + " but it is already connected to the CameraManager: " + cameraManager + ".  If this was intentional use ForceConnectCameraManager instead");
                return false;
            }

            cameraManager = newCameraManager;
            cameraManager.SetID(ID);
            return true;
        }

        public void ForceConnectCameraManager(CameraManager newCameraManager)
        {
            if(!IsCameraManagerValid(newCameraManager))return;
            if(newCameraManager == cameraManager)return;

            cameraManager = newCameraManager;
            cameraManager.SetID(ID);
        }

        private bool IsCameraManagerValid(CameraManager newCameraManager)
        {
            if(newCameraManager == null) return false;
            else return true;
        }

        // PLAYER INPUT FUNCTIONS =============================================

        // PLAYER =============================================================

        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            
            // The direction the player is inputing on the keyboard or gamepad
            float direction = callbackContext.ReadValue<float>();
            
            // For more on the InputActionPhase see: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputActionPhase.html
            switch (callbackContext.phase)
            {
                case InputActionPhase.Disabled:
                    // Add Code here
                    break;
                case InputActionPhase.Waiting:
                    // Add Code here
                    break;
                case InputActionPhase.Started:
                    // Add Code here
                    possessedPlayer.UpdateDesiredMoveDirection(new Vector2(direction,0));
                    break;
                case InputActionPhase.Performed:
                    // Add Code here
                    possessedPlayer.UpdateDesiredMoveDirection(new Vector2(direction,0));
                    break;
                case InputActionPhase.Canceled:
                    // Add Code here
                    possessedPlayer.UpdateDesiredMoveDirection(new Vector2(0,0));
                    break;
                default:
                    // Add Code here
                    break;
            }
        }

        public void OnJumpAndSlide(InputAction.CallbackContext callbackContext)
        {
            
            // The direction the player is inputing on the keyboard or gamepad
            float direction = callbackContext.ReadValue<float>();
            
            // For more on the InputActionPhase see: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputActionPhase.html
            switch (callbackContext.phase)
            {
                case InputActionPhase.Disabled:
                    // Add Code here
                    break;
                case InputActionPhase.Waiting:
                    // Add Code here
                    break;
                case InputActionPhase.Started:
                    // Add Code here
                    possessedPlayer.UpdateDesiredMoveDirection(new Vector2(0,direction));
                    break;
                case InputActionPhase.Performed:
                    // Add Code here
                    possessedPlayer.UpdateDesiredMoveDirection(new Vector2(0,direction));
                    break;
                case InputActionPhase.Canceled:
                    // Add Code here
                    possessedPlayer.UpdateDesiredMoveDirection(new Vector2(0,0));
                    break;
                default:
                    // Add Code here
                    break;
            }
        }

        public void OnPowerUp1(InputAction.CallbackContext callbackContext)
        {
            
            // For more on the InputActionPhase see: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputActionPhase.html
            switch (callbackContext.phase)
            {
                case InputActionPhase.Disabled:
                    // Add Code here
                    break;
                case InputActionPhase.Waiting:
                    // Add Code here
                    break;
                case InputActionPhase.Started:
                    // Add Code here
                    break;
                case InputActionPhase.Performed:
                    // Add Code here
                    possessedPlayer.TryToUsePowerUp(0);
                    break;
                case InputActionPhase.Canceled:
                    // Add Code here
                    break;
                default:
                    // Add Code here
                    break;
            }
        }

        public void OnPowerUp2(InputAction.CallbackContext callbackContext)
        {
            
            // For more on the InputActionPhase see: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputActionPhase.html
            switch (callbackContext.phase)
            {
                case InputActionPhase.Disabled:
                    // Add Code here
                    break;
                case InputActionPhase.Waiting:
                    // Add Code here
                    break;
                case InputActionPhase.Started:
                    // Add Code here
                    break;
                case InputActionPhase.Performed:
                    // Add Code here
                    possessedPlayer.TryToUsePowerUp(1);
                    break;
                case InputActionPhase.Canceled:
                    // Add Code here
                    break;
                default:
                    // Add Code here
                    break;
            }
        }

        public void OnPowerUp3(InputAction.CallbackContext callbackContext)
        {
            
            // For more on the InputActionPhase see: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputActionPhase.html
            switch (callbackContext.phase)
            {
                case InputActionPhase.Disabled:
                    // Add Code here
                    break;
                case InputActionPhase.Waiting:
                    // Add Code here
                    break;
                case InputActionPhase.Started:
                    // Add Code here
                    break;
                case InputActionPhase.Performed:
                    // Add Code here
                    possessedPlayer.TryToUsePowerUp(2);
                    break;
                case InputActionPhase.Canceled:
                    // Add Code here
                    break;
                default:
                    // Add Code here
                    break;
            }
        }



        public void OnPause(InputAction.CallbackContext callbackContext)
        {
            
            // For more on the InputActionPhase see: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputActionPhase.html
            switch (callbackContext.phase)
            {
                case InputActionPhase.Disabled:
                    // Add Code here
                    break;
                case InputActionPhase.Waiting:
                    // Add Code here
                    break;
                case InputActionPhase.Started:
                    // Add Code here
                    break;
                case InputActionPhase.Performed:
                    // Add Code here
                    OnRequestLoadUIWidgetDataEvent?.Invoke(pauseMenuWidgetData);
                    break;
                case InputActionPhase.Canceled:
                    // Add Code here
                    break;
                default:
                    // Add Code here
                    break;
            }
        }

        // UI =============================================================

        public void OnNaviage(InputAction.CallbackContext callbackContext)
        {
            
            // For more on the InputActionPhase see: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputActionPhase.html
            switch (callbackContext.phase)
            {
                case InputActionPhase.Disabled:
                    // Add Code here
                    Debug.Log($"FallBack PlayerController: OnNavigate() InputActionPhase.Disabled");
                    break;
                case InputActionPhase.Waiting:
                    // Add Code here
                    Debug.Log($"FallBack PlayerController: OnNavigate() InputActionPhase.Waiting");
                    break;
                case InputActionPhase.Started:
                    // Add Code here
                    Debug.Log($"FallBack PlayerController: OnNavigate() InputActionPhase.Started");
                    // if (EventSystem.current.currentSelectedGameObject == null)
                    // {
                    //     // Nothing is selected

                    //     Debug.Log($"FallBack PlayerController: OnNavigate() tried naviaging but currentSelectedGameObject == null\nRequesting Select FallBack");

                    //     OnRequestSelectUIFallBack.Invoke();
                    // }
                    break;
                case InputActionPhase.Performed:
                    // Add Code here
                    Debug.Log($"FallBack PlayerController: OnNavigate() InputActionPhase.Performed");
                    break;
                case InputActionPhase.Canceled:
                    // Add Code here
                    Debug.Log($"FallBack PlayerController: OnNavigate() InputActionPhase.Canceled");
                    break;
                default:
                    // Add Code here
                    Debug.Log($"FallBack PlayerController: OnNavigate() InputActionPhase.default");
                    break;
            }
        }

        public void OnDeviceLost(PlayerInput playerInput)
        {
            Debug.Log($"PlayerController: OnDeviceLost() playerInput = {playerInput}");
        }

        public void OnDeviceRegained(PlayerInput playerInput)
        {
            Debug.Log($"PlayerController: OnDeviceRegained() playerInput = {playerInput}");
        }

        public void OnControlsChanged(PlayerInput playerInput)
        {
            if(!hasTriedToChangeControlScheme)
            {
                hasTriedToChangeControlScheme = true;
                return;
            }
            
            Debug.Log($"PlayerController: OnControlsChanged() playerInput.currentControlScheme = {playerInput.currentControlScheme}");

            if(playerInput.currentControlScheme != null)
            {
                OnPlayerControlSchemeChangedEvent?.Invoke(playerInput.currentControlScheme);
            }
            else
            {
                Debug.LogWarning($"User {playerInput.user.id} has no control scheme assigned.");
            }
        }

        #endregion
    }
}