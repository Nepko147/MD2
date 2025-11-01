using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneOpening_UICanvas_StartText : MonoBehaviour
{
    public static AppScreen_Local_SceneOpening_UICanvas_StartText SingleOnScene { get; private set; }

    public bool Enabled 
    { 
        get { return (text.enabled); } 
        set { text.enabled = value; }
    }

    private enum Stage
    {
        PressAnyKey,
        LoadData
    }
    private Stage stage = Stage.PressAnyKey;

    private Text text;
    private Color text_color;
    private bool text_color_anim_state = true;
    private const float TEXT_COLOR_ALPHA_MIN = 0.25f;
    private const float TEXT_COLOR_ALPHA_MAX = 1f;
    private const float TEXT_COLOR_ALPHA_SPEED = 1f;

    private void Awake()
    {
        SingleOnScene = this;

        text = GetComponent<Text>();
        text_color = text.color;
        text_color.a = 0;
        text.color = text_color;

        Enabled = false;
    }

    private void Start()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.PlatformType_Current)
        {
            case ControlPers_BuildSettings.PlatformType.windows:
                text.text = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.startText_desktop); ;
                break;
            case ControlPers_BuildSettings.PlatformType.web_yandexGames_desktop:
                text.text = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.startText_desktop); ;
                break;
            case ControlPers_BuildSettings.PlatformType.web_yandexGames_mobile_android:
                text.text = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.startText_mobile); ;
                break;
        }
    }

    private void Update()
    {
        if (Enabled)
        {
            if (!text_color_anim_state)
            {
                text_color.a -= TEXT_COLOR_ALPHA_SPEED * Time.deltaTime;

                if (text_color.a <= TEXT_COLOR_ALPHA_MIN)
                {
                    text_color_anim_state = true;
                }
            }
            else
            {
                text_color.a += TEXT_COLOR_ALPHA_SPEED * Time.deltaTime;

                if (text_color.a >= TEXT_COLOR_ALPHA_MAX)
                {
                    text_color_anim_state = false;
                }
            }
        
            text.color = text_color;

            switch (stage)
            {
                case Stage.PressAnyKey:
                if (ControlScene_Opening.SingleOnScene.Stage_PressAnyKey_Pressed)
                {
                    if (!ControlPers_DataHandler.SingleOnScene.IsDataLoaded)
                    {
                        text.text = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.loadingCloudData);
                        stage = Stage.LoadData;
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                break;

                case Stage.LoadData:
                if (ControlPers_DataHandler.SingleOnScene.IsDataLoaded)
                {
                    Destroy(gameObject);
                }
                break;
            }
        }
    }
}
