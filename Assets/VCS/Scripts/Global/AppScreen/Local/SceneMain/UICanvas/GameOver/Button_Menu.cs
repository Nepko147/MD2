using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_GameOver_Button_Menu : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_GameOver_Button_Menu SingleOnScene { get; private set; }

    private void ImageRefresh()
    {
        var _idle = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_menu_idle);
        var _pointed = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_menu_pointed);
        var _pressed = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_menu_pressed);

        Image_LanguageRefresh(_idle, _pointed, _pressed);
    }       

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Visible = false;
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
