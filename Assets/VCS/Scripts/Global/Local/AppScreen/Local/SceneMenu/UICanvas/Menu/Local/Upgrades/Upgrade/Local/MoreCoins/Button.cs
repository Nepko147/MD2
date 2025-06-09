using UnityEngine;

public class AppScrren_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_MoreCoins_Button : AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_Button_Parent
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
