using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Text : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Text SingleOnScene { get; private set; }

    private Text text;
    private CanvasRenderer canvasRenderer;
    private float hide_delay;
    private float alpha_delta = 0.02f;

    enum OnDisplayMode
    {
        idle,
        prepareToHide,
        hide,
        show,
        showtemporally
    }

    OnDisplayMode elementOnDisplayMode;
    public void Enable(bool _state)
    {
        text.enabled = _state;
    }

    public void UpdateText(string _string)
    {
        text.text = _string;
    }

    public void ShowElement()
    {
        elementOnDisplayMode = OnDisplayMode.show;
    }

    public void ShowElementTemporally(float _timeOnDisplay)
    {
        elementOnDisplayMode = OnDisplayMode.showtemporally;
        hide_delay = _timeOnDisplay;
    }

    public void HideElementWithDelay(float _delay)
    {
        elementOnDisplayMode = OnDisplayMode.prepareToHide;
        hide_delay = _delay;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        text = GetComponent<Text>();
        text.font.material.mainTexture.filterMode = FilterMode.Point;
        canvasRenderer = GetComponent<CanvasRenderer>();
    }

    private void Update()
    {
        switch (elementOnDisplayMode)
        {
            case OnDisplayMode.prepareToHide:

                hide_delay -= Time.deltaTime;

                if (hide_delay <= 0)
                {
                    elementOnDisplayMode = OnDisplayMode.hide;
                }

                break;

            case OnDisplayMode.hide:

                var _currentAlpha = canvasRenderer.GetAlpha();

                if (_currentAlpha >= 0)
                {
                    float _newAlpha = _currentAlpha - alpha_delta;
                    canvasRenderer.SetAlpha(_newAlpha);
                }
                else
                {
                    canvasRenderer.SetAlpha(0);
                    elementOnDisplayMode = OnDisplayMode.idle;
                }

                break;

            case OnDisplayMode.show:

                _currentAlpha = canvasRenderer.GetAlpha();

                if (_currentAlpha <= 1)
                {
                    float _newAlpha = _currentAlpha + alpha_delta;
                    canvasRenderer.SetAlpha(_newAlpha);
                }
                else
                {
                    canvasRenderer.SetAlpha(1);
                    elementOnDisplayMode = OnDisplayMode.idle;
                }

                break;
            case OnDisplayMode.showtemporally:

                _currentAlpha = canvasRenderer.GetAlpha();

                if (_currentAlpha <= 1)
                {
                    float _newAlpha = _currentAlpha + alpha_delta;
                    canvasRenderer.SetAlpha(_newAlpha);
                }
                else
                {
                    canvasRenderer.SetAlpha(1);
                    elementOnDisplayMode = OnDisplayMode.prepareToHide;
                }

                break;
        }
    }
}
