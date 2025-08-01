using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Ru : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Ru SingleOnScene { get; private set; }

    private void ImageRefresh()
    {
        Image_LanguageRefresh(ControlPers_LanguageHandler.BUTTON_NAME_SWITCH_RU);
    }

    public override void OnClick()
    {
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_En.SingleOnScene.Pressed = false;
        ControlPers_LanguageHandler.SingleOnScene.SetGameLanguage(ControlPers_LanguageHandler.GameLanguage.russian);

        base.OnClick();
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    private void Start()
    {
        ImageRefresh();
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += ImageRefresh;
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate -= ImageRefresh;
    }
}
