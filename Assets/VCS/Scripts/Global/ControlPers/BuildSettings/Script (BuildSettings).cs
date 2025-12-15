using UnityEngine;
using YG;
using Utils;

public class ControlPers_BuildSettings : MonoBehaviour
{
    public static ControlPers_BuildSettings SingleOnScene { get; private set; }

    private enum BuildCompilationType
    {
        windows_standalone,
        web_yandexGames
    }

    [SerializeField] private BuildCompilationType buildCompilationType;

    public enum PlatformType
    {
        windows,
        web_yandexGames_desktop,
        web_yandexGames_mobile_android
    }

    public PlatformType PlatformType_Current { get; private set; }

    public const int PLATFORMTYPE_WEB_YANDEXGAMES_BONUS_PRICE_MULT = 2;
    public const int PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT = 3;

    [SerializeField] private bool debugInfo;
    public bool DebugInfo 
    { 
        get 
        { 
            return (debugInfo); 
        } 
    }

    [SerializeField] private bool notEncryptProgressFile;
    public bool NotEncryptProgressFile 
    { 
        get 
        { 
            return (notEncryptProgressFile); 
        } 
    }

    [SerializeField] private bool resetAsFirstLaunch;
    public bool ResetAsFirstLaunch 
    { 
        get 
        { 
            return (resetAsFirstLaunch); 
        } 
    }

    void Awake()
    {
        SingleOnScene = this;
        
        Application.targetFrameRate = Constants.TARGETFRAMERATE;
        
        switch (buildCompilationType)
        {
            case BuildCompilationType.windows_standalone:
                PlatformType_Current = PlatformType.windows;
            break;

            case BuildCompilationType.web_yandexGames:
                if (YG2.envir.isMobile)
                {
                    PlatformType_Current = PlatformType.web_yandexGames_mobile_android;
                }
                else
                {
                    PlatformType_Current = PlatformType.web_yandexGames_desktop;
                }
            break;
        }
    }
}
