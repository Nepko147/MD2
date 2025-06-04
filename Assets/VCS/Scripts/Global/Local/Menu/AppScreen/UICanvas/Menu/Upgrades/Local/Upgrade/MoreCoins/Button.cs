using UnityEngine;

public class AppScrren_UICanvas_Menu_Upgrades_Upgrade_MoreCoins_Button : AppScreen_UICanvas_Menu_Upgrades_Upgrade_Button_Parent
{
    protected override void Awake()
    {
        price_coins_buy = 50;
        price_coins_improve = 150;

        IsBought = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreCoins_IsBought;
        IsImproved = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreCoins_IsImproved;

        Buy = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreCoins_Buy;
        Improve = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreCoins_Improve;

        base.Awake();
    }
}
