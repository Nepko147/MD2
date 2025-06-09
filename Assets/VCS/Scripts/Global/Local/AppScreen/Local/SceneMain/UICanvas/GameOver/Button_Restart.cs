using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMain_UICanvas_GameOver_Button_Restart : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_GameOver_Button_Restart SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Visible = false;
    }
}
