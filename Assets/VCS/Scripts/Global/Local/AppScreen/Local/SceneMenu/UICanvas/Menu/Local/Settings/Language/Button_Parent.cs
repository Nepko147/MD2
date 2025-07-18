using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Parent : AppScreen_General_UICanvas_Parent
{      
    protected void OnClick(ControlPers_LanguageHandler.GameLanguage _gameLanguage)
    {
        ControlPers_LanguageHandler.SingleOnScene.SetGameLanguage(_gameLanguage);
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
