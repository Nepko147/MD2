using UnityEngine;

public class AppScreen_UICanvas_Menu_Upgrades_Button_Menu : AppScreen_UICanvas_Button_Parent
{
    public static AppScreen_UICanvas_Menu_Upgrades_Button_Menu SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }
}
