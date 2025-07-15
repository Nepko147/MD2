using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneOpening_UICanvas_StartText : MonoBehaviour
{
    public static AppScreen_Local_SceneOpening_UICanvas_StartText SingleOnScene { get; private set; }

    private enum Stage
    {
        PressAnyKey,
        LoadData
    }
    private Stage stage = Stage.PressAnyKey;

    private Text text;
    private Color text_color;
    private bool text_color_anim_state = false;
    [SerializeField] private float text_color_anim_timeCf = 1.1f;
    private float text_color_alpha_min = 0.25f;
    private float text_color_alpha_max = 1f;

    private void Awake()
    {
        text = GetComponent<Text>();
        
        switch (ControlPers_LanguageHandler.SingleOnScene.CurrentGameLanguage)
        {
            case ControlPers_LanguageHandler.GameLanguage.english:
                text.text = "PRESS ANY KEY";
            break;
            
            case ControlPers_LanguageHandler.GameLanguage.russian:
                text.text = "Õ¿∆Ã»“≈ Àﬁ¡”ﬁ  À¿¬»ÿ”";
            break;
        }

        text_color = text.color;
    }

    private void Update()
    {
        if (!text_color_anim_state)
        {
            text_color.a -= text_color_anim_timeCf * Time.deltaTime;

            if (text_color.a <= text_color_alpha_min)
            {
                text_color_anim_state = true;
            }
        }
        else
        {
            text_color.a += text_color_anim_timeCf * Time.deltaTime;

            if (text_color.a >= text_color_alpha_max)
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
                    switch (ControlPers_LanguageHandler.SingleOnScene.CurrentGameLanguage)
                    {
                        case ControlPers_LanguageHandler.GameLanguage.english:
                            text.text = "LOADING CLOUD DATA";
                            break;

                        case ControlPers_LanguageHandler.GameLanguage.russian:
                            text.text = "«¿√–”« ¿ ƒ¿ÕÕ€’";
                            break;
                    }
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
