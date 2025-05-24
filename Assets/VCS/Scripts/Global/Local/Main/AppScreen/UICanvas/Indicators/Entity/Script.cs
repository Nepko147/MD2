using UnityEngine;

public class AppScreen_UICanvas_Indicators : MonoBehaviour
{
    public static AppScreen_UICanvas_Indicators SingleOnScene { get; private set; }

    CanvasGroup canvasGroup;
    [SerializeField] float alpha_init;
    [SerializeField] float aplha_delta;

    bool show;
    bool hide;

    private void Awake()
    {
        SingleOnScene = this;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = alpha_init;
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
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
                show = false;
            }
        }

        if (hide)
        {
            canvasGroup.alpha -= aplha_delta * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                hide = false;
            }
        }
    }
}
