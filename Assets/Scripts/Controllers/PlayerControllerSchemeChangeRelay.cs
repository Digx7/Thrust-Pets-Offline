using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Digx7.Zygote;

namespace Digx7.ThrustPets
{
    public class PlayerControllerSchemeChangeRelay : MonoBehaviour 
    {
        [Header("Outgoing Events")]
        public StringEvent OnValidPlayerControlSchemeChangedEvent;
        
        private void OnEnable()
        {
            InputUser.onChange += OnUserChange;
        }

        private void OnDisable()
        {
            InputUser.onChange -= OnUserChange;
        }

        private void OnUserChange(InputUser user, InputUserChange change, InputDevice device)
        {
            if (change == InputUserChange.ControlSchemeChanged)
            {
                Debug.Log($"User {user.id} switched to scheme: {user.controlScheme?.name}");
                
                if(user.controlScheme != null)
                {
                    OnValidPlayerControlSchemeChangedEvent.Invoke(user.controlScheme?.name);
                }
                else
                {
                    Debug.LogWarning($"User {user.id} has no control scheme assigned.");
                }
            }
        }
    }
}