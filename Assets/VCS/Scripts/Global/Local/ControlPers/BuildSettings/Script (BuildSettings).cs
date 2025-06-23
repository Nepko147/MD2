using UnityEngine;
using YG;
using UnityEngine.UI;

public class ControlPers_BuildSettings : MonoBehaviour
{
    private enum BuildCompilationType
    {
        windows_standalone,
        web_yandexGames
    }

    [SerializeField] private BuildCompilationType buildCompilationType;

    private enum CurrentPlatformType
    {
        windows,
        web_yandexGames_desktop,
        web_yandexGames_mobile
    }

    private CurrentPlatformType currentPlatformType;

    [SerializeField] private Text currentPlatformType_Hint; //Отладка

    public const int SCENEINDEX_OPENING = 0;
    public const int SCENEINDEX_MENU = 1;
    public const int SCENEINDEX_MAIN = 2;

    void Awake()
    {
        Application.targetFrameRate = 60;

        switch (buildCompilationType)
        {
            case BuildCompilationType.windows_standalone:
                currentPlatformType = CurrentPlatformType.windows;
                currentPlatformType_Hint.text = "Platform: WINDOWS"; //Отладка
            break;

            case BuildCompilationType.web_yandexGames:
                if (YG2.envir.isMobile)
                {
                    currentPlatformType = CurrentPlatformType.web_yandexGames_mobile;
                    currentPlatformType_Hint.text = "Platform: MOBILE"; //Отладка
                }
                else
                {
                    currentPlatformType = CurrentPlatformType.web_yandexGames_desktop;
                    currentPlatformType_Hint.text = "Platform: DESKTOP"; //Отладка
                }
            break;
        }
    }
}
