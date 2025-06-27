using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Text : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Text SingleOnScene { get; private set; }

    Text text;

    public void Enable(bool _state)
    {
        text.enabled = _state;
    }

    public void UpdateText(string _string)
    {
        text.text = _string;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        text = GetComponent<Text>();
        text.font.material.mainTexture.filterMode = FilterMode.Point;
    }
}
