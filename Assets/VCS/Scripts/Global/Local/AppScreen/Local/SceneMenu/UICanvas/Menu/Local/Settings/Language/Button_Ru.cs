using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Ru : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Ru SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    public override void OnClick()
    {
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_En.SingleOnScene.Pressed = false;
        ControlPers_LanguageHandler.SingleOnScene.SetGameLanguage(ControlPers_LanguageHandler.GameLanguage.russian);

        base.OnClick();
    }
}
