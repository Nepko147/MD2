using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AppScreen_Local_SceneMain_UICanvas_Received_Entity : MonoBehaviour
{
    public static AppScreen_Local_SceneMain_UICanvas_Received_Entity SingleOnScene 
    { 
        get; 
        private set; 
    }

    private CanvasGroup canvasGroup;
    private float canvasGroup_deltaApha = 1.0f;
    
    [SerializeField] private Text received_text;
    [SerializeField] private Text received_coins_text;
    
    private int received_coins_count;
    public int Received_Coins_Count 
    {
        get 
        {
            return (received_coins_count);
        }

        set 
        {
            received_coins_count = value;
            received_coins_text.text = received_coins_count.ToString();
        } 
    }

    [SerializeField] private Text received_ad_text;
    public bool Received_Ad_Text_Visible
    {
        get
        {
            return received_ad_text.enabled;
        }

        set
        {
            received_ad_text.enabled = value;
        }
    }

    public void Text_LanguageRefresh()
    {
        received_text.text = ControlPers_LanguageHandler_Entity.SingleOnScene.Text_Get(ControlPers_LanguageHandler_Entity.Text_Key.received_text);
        received_ad_text.text = ControlPers_LanguageHandler_Entity.SingleOnScene.Text_Get(ControlPers_LanguageHandler_Entity.Text_Key.received_ad_text);
    }

    private enum received_state
    {
        onDisplay,
        hidden,
        idle,
        size
    }

    received_state received_state_currnet;

    public void Show(float _delay)
    {
        IEnumerator _coroutine(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            received_state_currnet = received_state.onDisplay;
        }

        var _routine = _coroutine(_delay);
        StartCoroutine(_routine);
    }

    public void Hide(float _delay)
    {
        IEnumerator _coroutine(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            received_state_currnet = received_state.hidden;
        }

        var _routine = _coroutine(_delay);
        StartCoroutine(_routine);
    }

    private void Awake()
    {
        SingleOnScene = this;

        Received_Coins_Count = 0;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;

        received_state_currnet = received_state.idle;
    }

    private void Start()
    {
        Text_LanguageRefresh();
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate += Text_LanguageRefresh;
    }

    private void Update()
    {
        switch (received_state_currnet)
        {
            case received_state.onDisplay:

                if (canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += canvasGroup_deltaApha * Time.deltaTime;
                }
                else
                {
                    received_state_currnet = received_state.idle;
                    canvasGroup.alpha = 1; // Гарантируем полное появление
                }

                break;

            case received_state.hidden:

                if (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= canvasGroup_deltaApha * Time.deltaTime;
                }
                else
                {
                    received_state_currnet = received_state.idle;
                    canvasGroup.alpha = 0; // Гарантируем полное исчезновение
                }

                break;
        }
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate -= Text_LanguageRefresh;
    }
}
