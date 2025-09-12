using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AppScreen_UICanvas_DebugInfo : MonoBehaviour
{
    private Text text_component;

    private void Awake()
    {
        text_component = GetComponent<Text>();
    }

    private void Start()
    {
        if (!ControlPers_BuildSettings.SingleOnScene.DebugInfo)
        {
            gameObject.SetActive(false);
        }
        else
        {
            switch (ControlPers_BuildSettings.SingleOnScene.PlatformType_Current)
            {
                case ControlPers_BuildSettings.PlatformType.windows:
                    text_component.text = "Platform: WINDOWS";
                break;

                case ControlPers_BuildSettings.PlatformType.web_yandexGames_desktop:
                    text_component.text = "Platform: WEB_YANDEXGAMES_DESKTOP";
                break;

                case ControlPers_BuildSettings.PlatformType.web_yandexGames_mobile_android:
                    text_component.text = "Platform: WEB_YANDEXGAMES_MOBILE_ANDROID";
                break;
            }
        }

        text_component.text += "; " + SceneManager.GetActiveScene().name;
    }
}
