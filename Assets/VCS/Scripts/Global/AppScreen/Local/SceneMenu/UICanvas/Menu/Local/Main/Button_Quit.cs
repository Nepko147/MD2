using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Quit : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Quit SingleOnScene { get; private set; }

    private void ImageRefresh()
    {
        var _idle = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_quit_idle);
        var _pointed = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_quit_pointed);
        var _pressed = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_quit_pressed);

        Image_LanguageRefresh(_idle, _pointed, _pressed);
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    private void Start()
    {
        ImageRefresh();
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate += ImageRefresh;
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate -= ImageRefresh;
    }
}
