using UnityEngine;

public class AppScrren_UICanvas_Menu_Upgrades_Upgrade_Local_Revive_Button 
: AppScreen_UICanvas_Menu_Upgrades_Upgrade_General_Button_Parent
{
    protected override void Start()
    {
        price_coins_buy = 300;
        price_coins_improve = 900;

        IsBought = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_Revive_IsBought;
        IsImproved = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_Revive_IsImproved;

        Buy = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_Revive_Buy;
        Improve = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_Revive_Improve;
        Animation = AppScreen_UICanvas_Menu_Upgrades_Upgrade_Local_Revive_Entity.SingleOnScene.Animation_Start;

        base.Start();

        Image_LanguageRefresh();
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate += Image_LanguageRefresh;
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate -= Image_LanguageRefresh;
    }
}
