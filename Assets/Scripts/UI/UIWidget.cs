using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

namespace Digx7.Zygote
{
    public class UIWidget : MonoBehaviour
    {
        #region Variables ================================
        
        [Header("Variables")]
        protected UIWidgetData ownUIWidgetData;

        // [Header("Incoming Channels")]
        [Header("Outgoing Events")]
        public UIWidgetDataEvent OnRequestLoadUIWidgetEvent;

        #endregion

        #region Setup ================================

        public virtual void Setup(UIWidgetData newUIWidgetData)
        {
            ownUIWidgetData = newUIWidgetData;
        }

        public virtual void Teardown()
        {
            Destroy(this.gameObject);
        }

        #endregion

        #region Main Functions ================================

        protected void UnloadSelf()
        {
            OnRequestLoadUIWidgetEvent?.Invoke(ownUIWidgetData);
        }
        
        protected IEnumerator Delay(VoidDelegate funcToCalAtEnd, float time)
        {
            yield return new WaitForSeconds(time);
            funcToCalAtEnd();
        }

        protected IEnumerator Delay(IntDelegate funcToCalAtEnd, int value, float time)
        {
            yield return new WaitForSeconds(time);
            funcToCalAtEnd(value);
        }

        #endregion
    }
}
