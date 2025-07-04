using UnityEngine;
using YG;
using UnityEngine.UI;

public class ControlPers_BuildSettings : MonoBehaviour
{
    public static ControlPers_BuildSettings SingleOnScene { get; private set; }

    private enum BuildCompilationType
    {
        windows_standalone,
        web_yandexGames
    }

    [SerializeField] private BuildCompilationType buildCompilationType;

    public enum CurrentPlatformType
    {
        windows,
        web_yandexGames_desktop,
        web_yandexGames_mobile_android
    }

    public CurrentPlatformType currentPlatformType { get; private set; }
    
    [SerializeField] private Text currentPlatformType_Hint; //Отладка

    private const int FRAMERATE = 60;

    public const int SCENEINDEX_OPENING = 0;
    public const int SCENEINDEX_MENU = 1;
    public const int SCENEINDEX_MAIN = 2;

    void Awake()
    {
        SingleOnScene = this;

        Application.targetFrameRate = FRAMERATE;
        
        switch (buildCompilationType)
        {
            case BuildCompilationType.windows_standalone:
                currentPlatformType = CurrentPlatformType.windows;
                currentPlatformType_Hint.text = "Platform: WINDOWS"; //Отладка
            break;

            case BuildCompilationType.web_yandexGames:
                if (YG2.envir.isMobile)
                {
                    currentPlatformType = CurrentPlatformType.web_yandexGames_mobile_android;
                    currentPlatformType_Hint.text = "Platform: WEB_YANDEXGAMES_MOBILE_ANDROID"; //Отладка
                }
                else
                {
                    currentPlatformType = CurrentPlatformType.web_yandexGames_desktop;
                    currentPlatformType_Hint.text = "Platform: WEB_YANDEXGAMES_DESKTOP"; //Отладка
                }
            break;
        }
    }
}
