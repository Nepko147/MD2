using UnityEngine;

public class AppScrren_UICanvas_Menu_Upgrades_Upgrade_Local_CoinMagnet_Button 
: AppScreen_UICanvas_Menu_Upgrades_Upgrade_General_Button_Parent
{
    protected override void Start()
    {
        price_coins_buy = 150;
        price_coins_improve = 450;

        IsBought = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_CoinMagnet_IsBought;
        IsImproved = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_CoinMagnet_IsImproved;

        Buy = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_CoinMagnet_Buy;
        Improve = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_CoinMagnet_Improve;
        Animation = AppScreen_UICanvas_Menu_Upgrades_Upgrade_Local_CoinMagnet_Entity.SingleOnScene.Animation_Start;

        base.Start();

        Image_LanguageRefresh();
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate += Image_LanguageRefresh;
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate -= Image_LanguageRefresh;
    }
}
