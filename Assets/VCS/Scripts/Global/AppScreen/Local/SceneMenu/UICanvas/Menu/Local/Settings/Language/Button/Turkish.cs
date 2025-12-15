using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Turkish : AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Turkish SingleOnScene { get; private set; }

    public override void OnClick()
    {
        Pressed_OffAll();

        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_State_Set(ControlPers_LanguageHandler_Entity.GameLanguage_State.turkish);

        base.OnClick();
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }
}
