using UnityEngine;

public class AppScreen_UICanvas_Indicators : MonoBehaviour
{
    public static AppScreen_UICanvas_Indicators SingleOnScene { get; private set; }

    float alpha;
    [SerializeField] float alpha_init;
    [SerializeField] float aplha_delta;

    bool show;
    bool hide;

    private void Awake()
    {
        SingleOnScene = this;

        GetComponent<CanvasGroup>().alpha = alpha_init; //Если "GetComponent<CanvasGroup>().alpha" запихать в переменную, то работать ничего не будет.
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
            GetComponent<CanvasGroup>().alpha += aplha_delta * Time.deltaTime;
            if (alpha >= 1)
            {
                alpha = 1;
                show = false;
            }
        }

        if (hide)
        {
            GetComponent<CanvasGroup>().alpha -= aplha_delta * Time.deltaTime;
            if (alpha <= 0)
            {
                alpha = 0;
                hide = false;
            }
        }
    }
}
