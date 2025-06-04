using UnityEngine;

public class AppScrren_UICanvas_Menu_Upgrades_Upgrade_MoreBonuses_Button : AppScreen_UICanvas_Menu_Upgrades_Upgrade_Button_Parent
{
    protected override void Awake()
    {
        price_coins_buy = 100;
        price_coins_improve = 300;

        IsBought = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreBonuses_IsBought;
        IsImproved = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreBonuses_IsImproved;

        Buy = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreBonuses_Buy;
        Improve = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreBonuses_Improve;

        base.Awake();
    }
}
