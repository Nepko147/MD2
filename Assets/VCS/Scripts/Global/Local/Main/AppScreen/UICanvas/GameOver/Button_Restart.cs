using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_GameOver_Button_Restart : AppScreen_UICanvas_Button_Parent
{
    public static AppScreen_UICanvas_GameOver_Button_Restart SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Visible = false;
    }
}
