using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    public class UIWidgetManager : Singleton<UIWidgetManager>
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] private Transform _canvas;
        [SerializeField] private List<UIWidgetData> _activeWidgets;
        [SerializeField] private EventSystem eventSystem;

        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/UI")]
        [SerializeField] private UIWidgetDataChannel _request_LoadUIWidget_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/UI")]
        [SerializeField] private UIWidgetDataChannel _request_UnloadUIWidget_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/UI")]
        [SerializeField] private Channel _request_ClearAllUIWidgets_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/UI")]
        [SerializeField] private GameObjectChannel _request_UpdateFallBackSelection_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/UI")]
        [SerializeField] private Channel _request_SelectFallBack_Channel;

        [Header("Outgoing Events")]
        public UnityEvent OnLoadUIWidgetEvent;
        public UnityEvent OnUnloadUIWidgetEvent;
        public UnityEvent OnClearAllUIWidgetsEvent;

        private GameObject fallBackSelectionObject;

        #endregion

        #region Setup ================================

        public override void SafeOnEnable()
        {
            SetupChannels();
        }

        public override void SafeOnDisable()
        {
            TeardownChannels();
        }

        private void SetupChannels()
        {
            _request_LoadUIWidget_Channel.channelEvent.AddListener(OnRecieve_LoadUIWidget);
            _request_UnloadUIWidget_Channel.channelEvent.AddListener(OnRecieve_UnloadUIWidget);
            _request_ClearAllUIWidgets_Channel.channelEvent.AddListener(OnRecieve_UnloadAllUIWidgets);
            _request_UpdateFallBackSelection_Channel.channelEvent.AddListener(OnRecieve_RequestUpdateFallBackSelect);
            _request_SelectFallBack_Channel.channelEvent.AddListener(OnRecieve_RequestSelectFallBack);
        }

        private void TeardownChannels()
        {
            _request_LoadUIWidget_Channel.channelEvent.RemoveListener(OnRecieve_LoadUIWidget);
            _request_UnloadUIWidget_Channel.channelEvent.RemoveListener(OnRecieve_UnloadUIWidget);
            _request_ClearAllUIWidgets_Channel.channelEvent.RemoveListener(OnRecieve_UnloadAllUIWidgets);
            _request_UpdateFallBackSelection_Channel.channelEvent.RemoveListener(OnRecieve_RequestUpdateFallBackSelect);
            _request_SelectFallBack_Channel.channelEvent.RemoveListener(OnRecieve_RequestSelectFallBack);
        }

        #endregion

        #region Channel Responses ================================

        protected void OnRecieve_LoadUIWidget(UIWidgetData newWidgetData)
        {
            LoadWidget(newWidgetData);
        }

        protected void OnRecieve_UnloadUIWidget(UIWidgetData widgetDataToUnload)
        {
            UnloadWidget(widgetDataToUnload);
        }

        protected void OnRecieve_UnloadAllUIWidgets()
        {
            UnloadAllWidgets();
        }

        protected void OnRecieve_RequestUpdateFallBackSelect(GameObject newFallBackObject)
        {
            UpdateFallBackSelection(newFallBackObject);
        }

        protected void OnRecieve_RequestSelectFallBack()
        {
            SelectFallBack();
        }

        #endregion

        #region Main Functions ================================

        private void LoadWidget(UIWidgetData newWidgetData)
        {
            if(_activeWidgets.Contains(newWidgetData))
            {
                Debug.LogWarning("Something tried to load the UIWidget: " + newWidgetData + " which is already loaded");
                return;
            }
            
            newWidgetData.SpawnWidget(_canvas);
            _activeWidgets.Add(newWidgetData);

            OnLoadUIWidgetEvent.Invoke();
        }

        private void UnloadWidget(UIWidgetData widgetDataToUnload)
        {
            if(!_activeWidgets.Contains(widgetDataToUnload)) return;

            widgetDataToUnload.DespawnWidget();
            _activeWidgets.Remove(widgetDataToUnload);

            OnUnloadUIWidgetEvent.Invoke();
        }

        private void UnloadAllWidgets()
        {
            foreach (UIWidgetData uIWidgetData in _activeWidgets)
            {
                uIWidgetData.DespawnWidget();
            }

            _activeWidgets.Clear();
            OnClearAllUIWidgetsEvent.Invoke();

        }

        private void UpdateFallBackSelection(GameObject newFallBackSelection)
        {
            fallBackSelectionObject = newFallBackSelection;

            Debug.Log($"FallBack UIWidgetManager: UpdateFallBackSelection() fallBackSelectionObject = {fallBackSelectionObject}");
        }

        private void SelectFallBack()
        {
            
            
            if(fallBackSelectionObject != null)
            {
                eventSystem.SetSelectedGameObject(fallBackSelectionObject);
            }

            Debug.Log($"FallBack UIWidgetManager: SelectFallBack() fallBackSelectionObject = {fallBackSelectionObject}");
        }

        #endregion
    }
}
