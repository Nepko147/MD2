using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Ru : AppScreen_General_UICanvas_Button_Parent
{
    protected new void OnClick()
    {
        ControlPers_LanguageHandler.SingleOnScene.SetGameLanguage(ControlPers_LanguageHandler.GameLanguage.russian);
    }
}
