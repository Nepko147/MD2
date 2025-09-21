using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AppScreen_Local_SceneMain_UICanvas_Cutscene_ContinueText : MonoBehaviour
{
    public static AppScreen_Local_SceneMain_UICanvas_Cutscene_ContinueText SingleOnScene { get; private set; }

    public bool Enabled
    {
        get { return (text.enabled); }
        set { text.enabled = value; }
    }

    private Text text;
    private Color text_color;
    private bool text_color_anim_state = true;
    private const float TEXT_COLOR_ALPHA_MIN = 0.25f;
    private const float TEXT_COLOR_ALPHA_MAX = 1f;
    private const float TEXT_COLOR_ALPHA_SPEED = 1f;

    public void Text_LanguageRefresh()
    {
        text.text = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.startText);
    }

    private IEnumerator text_coroutine(float _delay)
    {
        yield return new WaitForSeconds(_delay);

        Enabled = true;
    }

    private IEnumerator text_coroutine_current;

    public void Show(float _delay)
    {
        text_coroutine_current = text_coroutine(_delay);
        StartCoroutine(text_coroutine_current);
    }

    public void Hide()
    {
        StopCoroutine(text_coroutine_current);
        Enabled = false;
    }

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
        Text_LanguageRefresh();
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += Text_LanguageRefresh;
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
        }
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate -= Text_LanguageRefresh;
    }
}
