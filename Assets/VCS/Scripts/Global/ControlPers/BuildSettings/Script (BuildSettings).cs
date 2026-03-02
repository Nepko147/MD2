using UnityEngine;
using YG;
using Utils;

public class ControlPers_BuildSettings : MonoBehaviour
{
    public static ControlPers_BuildSettings SingleOnScene { get; private set; }

    private enum BuildType_Compilation
    {
        windows_standalone,
        web_yandexGames,
        web_itchIo,
        android_standalone
    }

    [SerializeField] private BuildType_Compilation buildType_compilation;

    public enum BuildType_Runtime
    {
        windows_standalone,
        web_yandexGames_desktop,
        web_yandexGames_mobile_android,
        web_itchIo,
        android_standalone
    }
    
    public BuildType_Runtime BuildType_Runtime_Current { get; private set; }

    public const int BUILDTYPE_RUNTIME_WEB_YANDEXGAMES_BONUS_PRICE_MULT = 2;
    public const int BUILDTYPE_RUNTIME_WEB_YANDEXGAMES_AD_MULT = 3;
    
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
        
        switch (buildType_compilation)
        {
            case BuildType_Compilation.windows_standalone:
                BuildType_Runtime_Current = BuildType_Runtime.windows_standalone;
            break;

            case BuildType_Compilation.web_yandexGames:
                if (!YG2.envir.isMobile)
                {
                    BuildType_Runtime_Current = BuildType_Runtime.web_yandexGames_desktop;
                }
                else
                {
                    BuildType_Runtime_Current = BuildType_Runtime.web_yandexGames_mobile_android;
                }
            break;

            case BuildType_Compilation.web_itchIo:
                BuildType_Runtime_Current = BuildType_Runtime.web_itchIo;
            break;
            
            case BuildType_Compilation.android_standalone:
                BuildType_Runtime_Current = BuildType_Runtime.android_standalone;
            break;
        }
    }
}
