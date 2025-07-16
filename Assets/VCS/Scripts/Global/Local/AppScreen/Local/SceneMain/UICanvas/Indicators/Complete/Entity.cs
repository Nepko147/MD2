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
            text_number.enabled = value;
            text_kmLeft.enabled = value;
        }
    }

    [SerializeField] private Text text_number;
    public string Text_Number
    {
        get { return (text_number.text); }
        set { text_number.text = value; }
    }
    [SerializeField] private Text text_kmLeft;

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

        text_number.font.material.mainTexture.filterMode = FilterMode.Point;
        text_kmLeft.font.material.mainTexture.filterMode = FilterMode.Point;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private void Start()
    {
        switch (ControlPers_LanguageHandler.SingleOnScene.GameLanguage_Current)
        {
            case ControlPers_LanguageHandler.GameLanguage.english:
                text_kmLeft.text = "KILOMETERS LEFT";
            break;

            case ControlPers_LanguageHandler.GameLanguage.russian:
                text_kmLeft.text = "КМ ДО ЦЕЛИ"; // ДА-ДА имненно "КМ ДО ЦЕЛИ". Шоб не было кринжа с падежами
            break;
        }
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
}
