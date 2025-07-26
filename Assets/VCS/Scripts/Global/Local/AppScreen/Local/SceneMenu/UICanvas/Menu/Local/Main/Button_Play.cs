using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Play : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Play SingleOnScene { get; private set; }

    private void ImageRefresh()
    {
        //
    }

    protected override void Awake()
    {
        base.Awake();
        
        ImageRefresh();

        SingleOnScene = this;
    }

    protected override void Start()
    {
        base .Start();

        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += ImageRefresh;
    }
}
