using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity SingleOnScene { get; private set; }

    private bool active = true;
    public bool Active 
    {
        get 
        { 
            return (active); 
        }
        set 
        { 
            active = value;
            text.enabled = value;
        }
    }

    [SerializeField] private Text text;

    private void Text_Update()
    {
        text.text = text_number + text_kmLeft;
    }

    public void Text_LanguageRefresh()
    {
        Text_Km_Left = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.indicators_complete);
    }

    private string text_number;
    public string Text_Number
    {
        get { return (text_number); }
        set 
        {
            text_number = "<color=#" + ColorUtility.ToHtmlStringRGB(text_number_color) + ">" + value + "</color> ";
            Text_Update();
        }
    }
    [SerializeField] private Color text_number_color = Color.white;

    private string text_kmLeft;

    public string Text_Km_Left 
    {
        get 
        {
            return text_kmLeft;
        }
        set
        {
            text_kmLeft = "<color=#" + ColorUtility.ToHtmlStringRGB(text_kmLeft_color) + ">" + value + "</color>";
            Text_Update();
        }
    }

    [SerializeField] private Color text_kmLeft_color = new Color(1.000f, 0.137f, 0.451f, 1.000f);

    private CanvasGroup canvasGroup;

    private enum State
    {
        hiden,
        appear,
        wait,
        hide
    }
    private State state = State.hiden;

    private float state_time;
    private const float STATE_TIME_APPEAR = 1f;
    private const float STATE_TIME_WAIT = 2f;
    private const float STATE_TIME_HIDE = 1f;

    public void Show(float _delay = 0)
    {
        IEnumerator _coroutine(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            state_time = STATE_TIME_APPEAR;
            state = State.appear;
        }

        var _routine = _coroutine(_delay);
        StartCoroutine(_routine);
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        text.font.material.mainTexture.filterMode = FilterMode.Point;

        Text_LanguageRefresh();

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += Text_LanguageRefresh;
    }

    private void Update()
    {
        if (active)
        {
            switch (state)
            {
                case State.appear:
                    state_time -= Time.deltaTime;

                    canvasGroup.alpha = 1f - state_time / STATE_TIME_APPEAR;

                    if (state_time <= 0)
                    {
                        state_time = STATE_TIME_WAIT;
                        state = State.wait;
                    }
                break;

                case State.wait:
                    state_time -= Time.deltaTime;

                    if (state_time <= 0)
                    {
                        state_time = STATE_TIME_HIDE;
                        state = State.hide;
                    }
                break;

                case State.hide:
                    state_time -= Time.deltaTime;

                    canvasGroup.alpha = state_time / STATE_TIME_HIDE;

                    if (state_time <= 0)
                    {
                        state = State.hiden;
                    }
                break;
            }
        }
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate -= Text_LanguageRefresh;
    }
}
