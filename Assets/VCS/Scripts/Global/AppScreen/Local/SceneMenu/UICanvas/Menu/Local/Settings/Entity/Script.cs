using System.Collections.Generic;
using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Entity SingleOnScene { get; private set; }

    private Dictionary<GameLanguage_State, AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Parent> LanguageToButton = new Dictionary<GameLanguage_State, AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Parent>();

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    private void Start()
    {
        var _source_ofs = new Vector2(0, -360f);
        Shift_Pos_Define(_source_ofs, Vector2.zero);

        LanguageToButton[GameLanguage_State.english] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_English.SingleOnScene;
        LanguageToButton[GameLanguage_State.russian] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Russian.SingleOnScene;
        LanguageToButton[GameLanguage_State.spanish] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Spanish.SingleOnScene;
        LanguageToButton[GameLanguage_State.portuguese] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Portuguese.SingleOnScene;
        LanguageToButton[GameLanguage_State.german] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_German.SingleOnScene;
        LanguageToButton[GameLanguage_State.french] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_French.SingleOnScene;
        LanguageToButton[GameLanguage_State.italian] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Italian.SingleOnScene;
        LanguageToButton[GameLanguage_State.polish] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Polish.SingleOnScene;
        LanguageToButton[GameLanguage_State.turkish] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Turkish.SingleOnScene;
        LanguageToButton[GameLanguage_State.kazakh] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Kazakh.SingleOnScene;
        LanguageToButton[GameLanguage_State.belarusian] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Belarusian.SingleOnScene;
        LanguageToButton[GameLanguage_State.ukrainian] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Ukrainian.SingleOnScene;
        LanguageToButton[GameLanguage_State.uzbek] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Uzbek.SingleOnScene;
        LanguageToButton[GameLanguage_State.indonesian] = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Indonesian.SingleOnScene;

        LanguageToButton[ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_State_Current].Pressed = true;
    }
}
