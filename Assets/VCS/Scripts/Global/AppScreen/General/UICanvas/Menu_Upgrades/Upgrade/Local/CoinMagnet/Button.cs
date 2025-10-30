using UnityEngine;

public class AppScrren_UICanvas_Menu_Upgrades_Upgrade_Local_CoinMagnet_Button 
: AppScreen_UICanvas_Menu_Upgrades_Upgrade_General_Button_Parent
{
    private void ImageRefresh()
    {
        Image_LanguageRefresh(ControlPers_LanguageHandler.ButtonName.upgrade);
    }

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

        ImageRefresh();
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += ImageRefresh;
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate -= ImageRefresh;
    }
}
