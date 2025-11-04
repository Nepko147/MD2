using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

    private int TEXT_SIZE_KMLEFT = 48;
    private int TEXT_SIZE_RADIO = 24;

    private int TEXT_HEIGHT_KMLEFT = 48;
    private int TEXT_HEIGHT_RADIO = 60;

    private void Text_Update()
    {
        text.text = text_number + text_kmLeft;
    }

    public void Text_LanguageRefresh()
    {
        Text_Km_Left = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.indicators_complete);

        var _languageHandler = ControlPers_LanguageHandler.SingleOnScene;

        text_radio_list_early = new List<string>
        {
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_early_1),
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_early_2),
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_early_3),
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_early_4),
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_early_5),
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_early_6),
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_early_7),
        };

        text_radio_list_late = new List<string>
        {
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_late_1),
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_late_2),
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_late_3),
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_late_4),
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_late_5),
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_late_6),
            _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.radio_string_late_7),
        };
    }

    private string text_number;
    public string Text_Number
    {
        get 
        { 
            return (text_number); 
        }
        set 
        {
            text_number = "<color=#" + ColorUtility.ToHtmlStringRGB(text_number_color) + ">" + value + "</color> ";
            Text_Update();
        }
    }
    [SerializeField] private Color text_number_color = Color.white;

    private string  text_kmLeft;    

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

    [SerializeField] private Color  text_kmLeft_color = new Color(1.000f, 0.137f, 0.451f, 1.000f);    
    
    private List<string>    text_radio_list_early;
    private bool            text_radio_list_early_isFirst = true;
    private List<string>    text_radio_list_late;

    private const float TEXT_RADIO_MUSIC_VOLUME_MULTIPLIER = 0.75f;
    private float       text_radio_music_volume_init;

    private CanvasGroup canvasGroup;

    private enum State
    {
        hidden,
        appear_kmLeft,
        appear_radio,
        wait_kmLeft,
        wait_radio,
        wait_hide,
        hide_kmLeft,
        hide_radio,
        size
    }
    private State state = State.hidden;

    private float state_time;
    private const float STATE_TIME_APPEAR = 1f;
    private const float STATE_TIME_WAIT_KMLEFT = 2f;
    private const float STATE_TIME_WAIT_RADIO = 3f;
    private const float STATE_TIME_HIDE = 1f;

    public void Show(float _delay = 0)
    {
        IEnumerator _Coroutine(float _delay)
        {
            yield return (new WaitForSeconds(_delay));

            state_time = STATE_TIME_APPEAR;
            state = State.appear_kmLeft;

            text.fontSize = TEXT_SIZE_KMLEFT;
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, TEXT_HEIGHT_KMLEFT);
        }

        var _routine = _Coroutine(_delay);
        StartCoroutine(_routine);
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        text.font.material.mainTexture.filterMode = FilterMode.Point;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private void Start()
    {
        Text_LanguageRefresh();
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += Text_LanguageRefresh;
    }

    private void Update()
    {
        if (active)
        {
            switch (state)
            {
                case State.appear_kmLeft:
                    state_time -= Time.deltaTime;

                    canvasGroup.alpha = 1f - state_time / STATE_TIME_APPEAR;

                    if (state_time <= 0)
                    {
                        state_time = STATE_TIME_WAIT_KMLEFT;
                        state = State.wait_kmLeft;
                    }
                break;

                case State.wait_kmLeft:
                    state_time -= Time.deltaTime;

                    if (state_time <= 0)
                    {
                        state_time = STATE_TIME_HIDE;
                        state = State.hide_kmLeft;
                    }
                break;

                case State.hide_kmLeft:
                    state_time -= Time.deltaTime;

                    canvasGroup.alpha = state_time / STATE_TIME_HIDE;

                    if (state_time <= 0)
                    {
                        state_time = STATE_TIME_APPEAR;

                        text.fontSize = TEXT_SIZE_RADIO;
                        text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, TEXT_HEIGHT_RADIO);

                        if (text_radio_list_early.Count > 0)
                        {
                            var _stringID = 0;

                            if (text_radio_list_early_isFirst)
                            {                                                           
                                text_radio_list_early_isFirst = false;
                            }
                            else
                            {
                                _stringID = Random.Range(0, text_radio_list_early.Count - 1);
                            }

                            text.text = text_radio_list_early[_stringID];
                            text_radio_list_early.RemoveAt(_stringID);
                        }
                        else
                        {
                            if (text_radio_list_late.Count > 0)
                            {
                                var _stringID = Random.Range(0, text_radio_list_late.Count - 1);
                                text.text = text_radio_list_late[_stringID];

                                text_radio_list_late.RemoveAt(_stringID);
                            }
                            else
                            {
                                text.text = "[...]";
                            }
                        }

                        //TODO: Звук "помех и неразборчивой речи" ВКЛ
                        text_radio_music_volume_init = ControlPers_AudioMixer_Music.SingleOnScene.Volume_Get();
                        var _volume = text_radio_music_volume_init * TEXT_RADIO_MUSIC_VOLUME_MULTIPLIER;
                        ControlPers_AudioMixer_Music.SingleOnScene.Volume_Set(_volume);

                        state = State.appear_radio;
                    }
                    break;

                case State.appear_radio:
                    state_time -= Time.deltaTime;

                    canvasGroup.alpha = 1f - state_time / STATE_TIME_APPEAR;

                    if (state_time <= 0)
                    {
                        state_time = STATE_TIME_WAIT_RADIO;
                        state = State.hide_radio;
                    }
                break;

                case State.hide_radio:
                    state_time -= Time.deltaTime;

                    canvasGroup.alpha = state_time / STATE_TIME_HIDE;

                    if (state_time <= 0)
                    {
                        state_time = STATE_TIME_HIDE;

                        //TODO: Звук "помех и неразборчивой речи" ВЫКЛ (Если он сильно длиный)
                        ControlPers_AudioMixer_Music.SingleOnScene.Volume_Set(text_radio_music_volume_init);

                        state = State.hidden;
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
