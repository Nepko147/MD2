using UnityEngine;

public class AppScrren_UICanvas_Menu_Upgrades_Upgrade_CoinMagnet_Button : AppScreen_UICanvas_Menu_Upgrades_Upgrade_Button_Parent
{
    protected override void Awake()
    {
        price_coins_buy = 150;
        price_coins_improve = 450;

        IsBought = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_CoinMagnet_IsBought;
        IsImproved = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_CoinMagnet_IsImproved;

        Buy = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_CoinMagnet_Buy;
        Improve = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_CoinMagnet_Improve;

        base.Awake();
    }
}
