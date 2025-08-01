using UnityEngine;

public class AppScrren_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_Revive_Button 
: AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_Button_Parent
{
    private void ImageRefresh()
    {
        Image_LanguageRefresh(ControlPers_LanguageHandler.BUTTON_NAME_UPGRADE);
    }
    protected override void Start()
    {
        price_coins_buy = 300;
        price_coins_improve = 900;

        IsBought = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_Revive_IsBought;
        IsImproved = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_Revive_IsImproved;

        Buy = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_Revive_Buy;
        Improve = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_Revive_Improve;
        Animation = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_Revive_Entity.SingleOnScene.Animation_Start;

        base.Start();

        ImageRefresh();
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += ImageRefresh;
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate -= ImageRefresh;
    }
}
