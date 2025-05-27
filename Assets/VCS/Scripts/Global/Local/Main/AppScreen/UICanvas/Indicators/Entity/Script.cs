using UnityEngine;

public class AppScreen_UICanvas_Indicators : MonoBehaviour
{
    public static AppScreen_UICanvas_Indicators SingleOnScene { get; private set; }

    CanvasGroup canvasGroup;
    [SerializeField] float alpha_init = 0.0f;
    [SerializeField] float aplha_delta = 1.0f;

    SpriteRenderer  sr_ups;
    SpriteRenderer  sr_coin;
    Color           sr_color_delta;

    bool show;
    bool hide;

    private void Awake()
    {
        SingleOnScene = this;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = alpha_init;        
    }

    private void Start()
    {
        var _newColor = new Color(1, 1, 1, alpha_init);
        sr_ups = AppScreen_UICanvas_Indicators_Ups_Sprite.SingleOnScene.GetComponent<SpriteRenderer>();
        sr_ups.color *= _newColor;

        sr_coin = AppScreen_UICanvas_Indicators_Coins_Sprite.SingleOnScene.GetComponent<SpriteRenderer>();
        sr_coin.color *= _newColor;
        sr_color_delta = new Color(1, 1, 1, aplha_delta);
    }

    public void Show()
    {
        show = true;
        hide = false;
    }

    public void Hide()
    {
        show = false;
        hide = true;
    }

    private void Update()
    {
        if (show)
        {
            canvasGroup.alpha += aplha_delta * Time.deltaTime;           
            sr_ups.color += sr_color_delta * Time.deltaTime;
            sr_coin.color += sr_color_delta * Time.deltaTime;

            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
                show = false;
            }
        }

        if (hide)
        {
            canvasGroup.alpha -= aplha_delta * Time.deltaTime;
            sr_ups.color -= sr_color_delta * Time.deltaTime;
            sr_coin.color -= sr_color_delta * Time.deltaTime;

            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                hide = false;
            }
        }
    }
}
