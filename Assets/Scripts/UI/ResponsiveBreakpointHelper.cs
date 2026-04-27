using UnityEngine;
using UnityEngine.Events;
using Digx7.Zygote;
using System.Collections;
using System.Collections.Generic;

public class ResponsiveBreakpointHelper : MonoBehaviour 
{
    #region Variables ================================
    [Header("Variables")]
    public List<breakPointDataAndUnityEventPair> breakPoints;
    public bool updateInEditMode = false;
    public bool updateOnStart = true;
    public bool coninuallyUpdate = false;
    public float checkDelay = 1f;
    #endregion

    #region Setup ================================

    private void Start() 
    {
        if(updateOnStart)
        {
            UpdateUI();
            if(coninuallyUpdate)
            {
                StartCoroutine(ConinualUpdate());
            }
        }
    }

    #endregion

    #region Main Methods ================================

    private void OnRectTransformDimensionsChange() 
    {
        if(Application.isPlaying || updateInEditMode)
        {
            UpdateUI();
        }
    }

    public void UpdateUI() 
    {   
        ScreenInfo currentScreenInfo = new ScreenInfo { width = Screen.width, height = Screen.height };

        foreach (var breakPoint in breakPoints) 
        {
            if (IsWithinBreakPoint(currentScreenInfo, breakPoint)) 
            {
                ApplyBreakPoint(breakPoint);
                break; // Exit after applying the first matching breakpoint
            }
        }
    }

    public void ApplyBreakPoint(breakPointDataAndUnityEventPair breakPoint)
    {   
        breakPoint.onBreakPointApplied?.Invoke();
    }

    private bool IsWithinBreakPoint(ScreenInfo screenInfo, breakPointDataAndUnityEventPair breakPoint) 
    {
        
        if (screenInfo.width < breakPoint.screenBreakPointData.minScreenWidth || screenInfo.width > breakPoint.screenBreakPointData.maxScreenWidth)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    IEnumerator ConinualUpdate()
    {
        while(gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(checkDelay);
            UpdateUI();
        }
    }

    #endregion
}