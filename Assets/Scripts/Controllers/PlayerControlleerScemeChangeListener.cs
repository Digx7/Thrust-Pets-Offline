using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Digx7.Zygote;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Digx7.ThrustPets
{
    public class PlayerControlleerScemeChangeListener : MonoBehaviour 
    {
        [Header("Incoming Channels")]
        [SerializeField] StringChannel _On_PlayerControlSchemeChanged_Channel;

        [Header("Responses")]
        [SerializeField] ControlSchemeChangeResponsePair[] _controlSchemeChangeResponses;

        private void OnEnable()
        {
            _On_PlayerControlSchemeChanged_Channel.channelEvent.AddListener(OnRecieve_PlayerControlSchemeChanged);
            
            Debug.Log($"PlayerControlleerScemeChangeListener: OnEnable() _On_PlayerControlSchemeChanged_Channel.lastValue = {_On_PlayerControlSchemeChanged_Channel.lastValue}");
            OnRecieve_PlayerControlSchemeChanged(_On_PlayerControlSchemeChanged_Channel.lastValue);

            // if(_On_PlayerControlSchemeChanged_Channel.lastValue != null && _On_PlayerControlSchemeChanged_Channel.lastValue != "")
            // {
            //     OnRecieve_PlayerControlSchemeChanged(_On_PlayerControlSchemeChanged_Channel.lastValue);
            // }
        }

        private void OnDisable()
        {
            _On_PlayerControlSchemeChanged_Channel.channelEvent.RemoveListener(OnRecieve_PlayerControlSchemeChanged);
        }

        private void OnRecieve_PlayerControlSchemeChanged(string newControlScheme)
        {
            if(string.IsNullOrEmpty(newControlScheme))
            {
                Debug.LogWarning($"PlayerControlleerScemeChangeListener: OnRecieve_PlayerControlSchemeChanged() newControlScheme is null or empty.");
                return;
            }
            
            Debug.Log($"PlayerControlleerScemeChangeListener: OnRecieve_PlayerControlSchemeChanged() newControlScheme = {newControlScheme}");

            foreach (var responsePair in _controlSchemeChangeResponses)
            {
                if (responsePair.controlSchemeName.GetDescription() == newControlScheme)
                {
                    Debug.Log($"PlayerControlleerScemeChangeListener: OnRecieve_PlayerControlSchemeChanged() Invoking response for control scheme: {newControlScheme}");
                    responsePair.onControlSchemeChanged?.Invoke();
                }
            }
        }
    }

    [System.Serializable]
    public class ControlSchemeChangeResponsePair
    {
        public Digx7.ThrustPets.ValidControlScheme controlSchemeName;
        public UnityEngine.Events.UnityEvent onControlSchemeChanged;
    }

    [System.Serializable]
    public enum ValidControlScheme
    {
        [Description("Keyboard&Mouse")]
        KeyboardMouse,
        [Description("Gamepad")]
        Gamepad,
        [Description("Touch")]
        Touch,
        [Description("Other")]
        Other
    }

    // Extension method to easily grab the description string
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }
    }
}
