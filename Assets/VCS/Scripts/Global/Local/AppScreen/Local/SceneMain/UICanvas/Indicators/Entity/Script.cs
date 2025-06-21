using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Indicators_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Indicators_Entity SingleOnScene { get; private set; }

    CanvasGroup canvasGroup;
    [SerializeField] float alpha_init = 0.0f;
    [SerializeField] float aplha_delta = 1.0f;

    enum OnDisplay
    {
        show,
        hide,
        none
    }

    OnDisplay state;

    public void Show()
    {
        state = OnDisplay.show;
    }

    public void Hide()
    {
        state = OnDisplay.hide;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = alpha_init;
    }

    private void Start()
    {
        AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Sprite.SingleOnScene.SetAlpha(alpha_init);
        AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Sprite.SingleOnScene.SetAlpha(alpha_init);
        AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.SetAlpha(alpha_init);
    }

    private void Update()
    {
        switch (state)
        {
            case OnDisplay.show:
                
                var _newAlpha = canvasGroup.alpha + aplha_delta * Time.deltaTime;
                canvasGroup.alpha = _newAlpha;
                AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Sprite.SingleOnScene.SetAlpha(_newAlpha);
                AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Sprite.SingleOnScene.SetAlpha(_newAlpha);
                AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.SetAlpha(_newAlpha);

                if (canvasGroup.alpha >= 1)
                {
                    _newAlpha = 1;
                    canvasGroup.alpha = _newAlpha;
                    AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Sprite.SingleOnScene.SetAlpha(_newAlpha);
                    AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Sprite.SingleOnScene.SetAlpha(_newAlpha);
                    AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.SetAlpha(_newAlpha);
                    state = OnDisplay.none;
                }

                break;

            case OnDisplay.hide:

                _newAlpha = canvasGroup.alpha - aplha_delta * Time.deltaTime;
                canvasGroup.alpha = _newAlpha;
                AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Sprite.SingleOnScene.SetAlpha(_newAlpha);
                AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Sprite.SingleOnScene.SetAlpha(_newAlpha);
                AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.SetAlpha(_newAlpha);

                if (canvasGroup.alpha <= 0)
                {
                    _newAlpha = 0;
                    canvasGroup.alpha = _newAlpha;
                    AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Sprite.SingleOnScene.SetAlpha(_newAlpha);
                    AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Sprite.SingleOnScene.SetAlpha(_newAlpha);
                    AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.SetAlpha(_newAlpha);
                    state = OnDisplay.none;
                }

                break;
        }
    }
}
