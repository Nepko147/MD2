using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AppScreen_General_UICanvas_DebugInfo : MonoBehaviour
{
    public static AppScreen_General_UICanvas_DebugInfo SingleOnScene { get; private set; }

    private Text text_component;

    public void Text_Set(string _text)
    {
        text_component.text = _text;
    }

    public void Text_Add(string _text)
    {
        text_component.text += "; " + _text;
    }

    private void Awake()
    {
        SingleOnScene = this;

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
            switch (ControlPers_BuildSettings.SingleOnScene.BuildRuntimeType_Current)
            {
                case ControlPers_BuildSettings.BuildRuntimeType.windows_standalone:
                    text_component.text = "Platform: WINDOWS";
                break;

                case ControlPers_BuildSettings.BuildRuntimeType.web_yandexGames_desktop:
                    text_component.text = "Platform: WEB_YANDEXGAMES_DESKTOP";
                break;

                case ControlPers_BuildSettings.BuildRuntimeType.web_yandexGames_mobile_android:
                    text_component.text = "Platform: WEB_YANDEXGAMES_MOBILE_ANDROID";
                break;

                case ControlPers_BuildSettings.BuildRuntimeType.web_itchIo:
                    text_component.text = "Platform: WEB_ITCHIO";
                break;
            }
        }
        
        text_component.text += "; " + SceneManager.GetActiveScene().name;
    }
}
