using UnityEngine;
using System.Collections;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_PopUpMessage_Button : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_PopUpMessage_Button SingleOnScene { get; private set; }

    private void ImageRefresh()
    {
        Image_LanguageRefresh(ControlPers_LanguageHandler.ButtonName.ok);
    }

    protected override void Awake()
    {
        base.Awake();

        ActiveOnPopUpMessage = true;

        SingleOnScene = this;
    }

    private void Start()
    {
        ImageRefresh();
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += ImageRefresh;
    }

    protected override void Update()
    {
        base.Update();

        Image_PointsRefresh();
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate -= ImageRefresh;
    }

}
