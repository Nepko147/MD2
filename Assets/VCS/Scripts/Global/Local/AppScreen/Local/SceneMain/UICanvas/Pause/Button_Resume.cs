using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Pause_Button_Resume : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Pause_Button_Resume SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Visible = false;
    }
}
