using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_En : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_En SingleOnScene { get; private set; }

    private void ImageRefresh()
    {
        Image_LanguageRefresh(ControlPers_LanguageHandler.ButtonName.switch_en);
    }

    public override void OnClick()
    {
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Ru.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Es.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Pt.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_De.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Id.SingleOnScene.Pressed = false;
        ControlPers_LanguageHandler.SingleOnScene.SetGameLanguage(ControlPers_LanguageHandler.GameLanguage.english);

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
