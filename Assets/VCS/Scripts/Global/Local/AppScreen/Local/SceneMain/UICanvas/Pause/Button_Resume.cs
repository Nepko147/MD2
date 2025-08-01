using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Pause_Button_Resume : AppScreen_General_UICanvas_Button_Parent
{
    private void ImageRefresh()
    {
        Image_LanguageRefresh(ControlPers_LanguageHandler.BUTTON_NAME_RESUME);
    }

    public static AppScreen_Local_SceneMain_UICanvas_Pause_Button_Resume SingleOnScene { get; private set; }

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
