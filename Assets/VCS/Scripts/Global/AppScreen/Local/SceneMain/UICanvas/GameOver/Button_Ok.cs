using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_GameOver_Button_Ok : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_GameOver_Button_Ok SingleOnScene { get; private set; }

    private void ImageRefresh()
    {
        Image_LanguageRefresh(ControlPers_LanguageHandler.ButtonName.ok);
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
