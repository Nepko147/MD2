using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Text : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Text SingleOnScene { get; private set; }

    private Text text;

    public string Text 
    { 
        get
        {
            return text.text;
        }
        set
        {
            text.text = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        text = GetComponent<Text>();
        text.font.material.mainTexture.filterMode = FilterMode.Point;
    }
}
