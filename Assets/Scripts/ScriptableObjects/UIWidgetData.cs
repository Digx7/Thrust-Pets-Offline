using UnityEngine;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewUIWidgetData", menuName = "ScriptableObjects/Data/UIWidgetData", order = 1)]
    public class UIWidgetData : ScriptableObject
    {
        #region Variables ==============================================
        
        public GameObject widgetPrefab;

        private GameObject instantiatedWidget;

        public GameObject GetInstantiatedWidget()
        {
            return instantiatedWidget;
        }

        #endregion

        #region Main Functions ==============================================

        public void SpawnWidget(Transform parent = null)
        {
            if(parent != null)
            {
                instantiatedWidget = Instantiate(widgetPrefab, parent);
            }

            UIWidget uIWidget = instantiatedWidget.GetComponent<UIWidget>();
            uIWidget.Setup(this);
        }

        public void DespawnWidget()
        {
            UIWidget uIWidget = instantiatedWidget.GetComponent<UIWidget>();
            uIWidget.Teardown();
        }

        #endregion
    }
}