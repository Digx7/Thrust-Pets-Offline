using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Digx7.Zygote
{
    public class UIMenu : UIWidget
    {
        
        public GameObjectEvent requestUpdateFallBackSelection;
        public UnityEvent requestSelectFallBack;

        public GameObject fallBackSelection;
        public InputActionReference uiNavigateAction;
        
        #region Setup ================================
        
        public override void Setup(UIWidgetData newUIWidgetData)
        {
            UpdateFallBackSelection();
            uiNavigateAction.action.Enable();
            uiNavigateAction.action.performed += TrySelectFallBack;
            base.Setup(newUIWidgetData);
        }

        public override void Teardown()
        {
            uiNavigateAction.action.performed -= TrySelectFallBack;
            // uiNavigateAction.action.Disable();
            base.Teardown();
        }

        public virtual void UpdateFallBackSelection()
        {
            Debug.Log($"FallBack UIMenu: UpdateFallBackSelection() requestingUpdateFallBackSelection to {fallBackSelection}");
            
            requestUpdateFallBackSelection.Invoke(fallBackSelection);
        }

        public virtual void UpdateFallBackSelection(GameObject newFallBackSelection)
        {
            // Debug.Log($"FallBack UIMenu: UpdateFallBackSelection() requestingUpdateFallBackSelection to {fallBackSelection}");
            
            fallBackSelection = newFallBackSelection;
            
            UpdateFallBackSelection();
        }

        public virtual void TrySelectFallBack(InputAction.CallbackContext context)
        {
            Debug.Log($"FallBack UIMenu: TrySelectFallBack() {EventSystem.current.currentSelectedGameObject}");
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                // Nothing is selected
                Debug.Log($"FallBack UIMenu: TrySelectFallBack() currentSelectedGameObject is Null, requesting Select Fallback");
                requestSelectFallBack.Invoke();
            }
        }

        #endregion
    }
}
