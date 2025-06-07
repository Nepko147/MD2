using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Main_Button_Quit : AppScreen_UICanvas_Button_Parent
{
    public static AppScreen_UICanvas_Menu_Main_Button_Quit SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }
}
