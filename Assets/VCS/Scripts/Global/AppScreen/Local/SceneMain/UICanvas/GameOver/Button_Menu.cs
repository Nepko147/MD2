using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_GameOver_Button_Menu : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_GameOver_Button_Menu SingleOnScene { get; private set; }

    private void ImageRefresh()
    {
        Image_LanguageRefresh(ControlPers_LanguageHandler.ButtonName.menu);
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
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += ImageRefresh;
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate -= ImageRefresh;
    }
}
