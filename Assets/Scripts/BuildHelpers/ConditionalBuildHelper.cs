using UnityEngine;
using UnityEngine.Events;

public class ConditionalBuildHelper : MonoBehaviour
{
    public bool checkAllOnStart = true;
    
    [Header("OS")]
    public UnityEvent onOsIsWindows;
    public UnityEvent onOsIsNOTWindows;
    public UnityEvent onOsIsWebGL;
    public UnityEvent onOsIsNOTWebGL;
    public UnityEvent onOsIsAndroid;
    public UnityEvent onOsIsNOTAndroid;
    public UnityEvent onOsIsMobile;
    public UnityEvent onOsIsNOTMobile;
    
    [Header("Platform")]
    public UnityEvent onPlatformIsSteam;
    public UnityEvent onPlatformIsNOTSteam;
    public UnityEvent onPlatformIsItch;
    public UnityEvent onPlatformIsNOTItch;
    public UnityEvent onPlatformIsGooglePlay;
    public UnityEvent onPlatformIsNOTGooglePlay;

    [Header("Sku")]
    public UnityEvent onSkuIsDev;
    public UnityEvent onSkuIsNOTDev;
    public UnityEvent onSkuIsShowcase;
    public UnityEvent onSkuIsNOTShowcase;
    public UnityEvent onSkuIsPlaytest;
    public UnityEvent onSkuIsNOTPlaytest;
    public UnityEvent onSkuIsDemo;
    public UnityEvent onSkuIsNOTDemo;
    public UnityEvent onSkuIsGame;
    public UnityEvent onSkuIsNOTGame;

    private void Start() 
    {
        if (checkAllOnStart) CheckAll();
    }

    public void CheckAll()
    {
        CheckOS();
        CheckPlatform();
        CheckSku();
    }

    public void CheckOS()
    {
        #if WIN
            onOsIsWindows.Invoke();
        #else
            onOsIsNOTWindows.Invoke();
        #endif

        #if WEB
            onOsIsWebGL.Invoke();
        #else
            onOsIsNOTWebGL.Invoke();
        #endif
    }

    public void CheckPlatform()
    {
        #if STEAM
            onPlatformIsSteam.Invoke();
        #else
            onPlatformIsNOTSteam.Invoke();
        #endif

        #if ITCH
            onPlatformIsItch.Invoke();
        #else
            onPlatformIsNOTItch.Invoke();
        #endif

        #if GOOGLE
            onPlatformIsGooglePlay.Invoke();
        #else
            onPlatformIsNOTGooglePlay.Invoke();
        #endif
    }

    public void CheckSku()
    {
        #if DEV
            onSkuIsDev.Invoke();
        #else
            onSkuIsNOTDev.Invoke();
        #endif

        #if SHOWCASE
            onSkuIsShowcase.Invoke();
        #else
            onSkuIsNOTShowcase.Invoke();
        #endif

        #if PLAYTEST
            onSkuIsPlaytest.Invoke();
        #else
            onSkuIsNOTPlaytest.Invoke();
        #endif

        #if DEMO
            onSkuIsDemo.Invoke();
        #else
            onSkuIsNOTDemo.Invoke();
        #endif

        #if GAME
            onSkuIsGame.Invoke();
        #else
            onSkuIsNOTGame.Invoke();
        #endif
    }
}
