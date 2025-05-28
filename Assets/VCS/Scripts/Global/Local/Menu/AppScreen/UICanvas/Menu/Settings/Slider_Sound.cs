using UnityEngine;

public class AppScreen_UICanvas_Menu_Settings_Slider_Sound : AppScreen_UICanvas_Menu_Settings_Slider_Parent
{    
    public static AppScreen_UICanvas_Menu_Settings_Slider_Sound SingleOnScene { get; private set; }

    public new void OnClick()
    {
        base.OnClick();

        //
    }

    private new void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }
}
