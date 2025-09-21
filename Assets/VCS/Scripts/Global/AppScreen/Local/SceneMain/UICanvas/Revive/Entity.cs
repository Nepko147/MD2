using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Revive_Entity :MonoBehaviour
{
    public static AppScreen_Local_SceneMain_UICanvas_Revive_Entity SingleOnScene { get; private set; }

    CanvasGroup canvasGroup;

    private float aplha_delta = 10.0f;

    enum OnDisplay
    {
        show,
        hide,
        none,
        size
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

    private void Awake()
    {
        SingleOnScene = this;

        state = OnDisplay.none;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private void Update()
    {
        switch (state)
        {
            case OnDisplay.show:

                var _newAlpha = canvasGroup.alpha + aplha_delta * Time.deltaTime;
                canvasGroup.alpha = _newAlpha;

                if (canvasGroup.alpha >= 1)
                {
                    _newAlpha = 1;
                    canvasGroup.alpha = _newAlpha;
                    state = OnDisplay.none;
                }

            break;

            case OnDisplay.hide:

                _newAlpha = canvasGroup.alpha - aplha_delta * Time.deltaTime;
                canvasGroup.alpha = _newAlpha;

                if (canvasGroup.alpha <= 0)
                {
                    _newAlpha = 0;
                    canvasGroup.alpha = _newAlpha;
                    state = OnDisplay.none;
                }

            break;
        }
    }
}
