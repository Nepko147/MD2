using UnityEngine;
using YG;
using UnityEngine.UI;
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

    public enum CurrentPlatformType
    {
        windows,
        web_yandexGames_desktop,
        web_yandexGames_mobile_android
    }

    public CurrentPlatformType currentPlatformType { get; private set; }

    [SerializeField] private Text currentPlatformType_Hint;

    void Awake()
    {
        SingleOnScene = this;

        Application.targetFrameRate = Constants.TARGETFRAMERATE;
        
        switch (buildCompilationType)
        {
            case BuildCompilationType.windows_standalone:
                currentPlatformType = CurrentPlatformType.windows;
                currentPlatformType_Hint.text = "Platform: WINDOWS";
            break;

            case BuildCompilationType.web_yandexGames:
                if (YG2.envir.isMobile)
                {
                    currentPlatformType = CurrentPlatformType.web_yandexGames_mobile_android;
                    currentPlatformType_Hint.text = "Platform: WEB_YANDEXGAMES_MOBILE_ANDROID";
                }
                else
                {
                    currentPlatformType = CurrentPlatformType.web_yandexGames_desktop;
                    currentPlatformType_Hint.text = "Platform: WEB_YANDEXGAMES_DESKTOP";
                }
            break;
        }
    }
}
