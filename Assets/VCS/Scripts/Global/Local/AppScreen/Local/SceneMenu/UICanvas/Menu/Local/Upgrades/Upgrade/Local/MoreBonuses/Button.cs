using UnityEngine;

public class AppScrren_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_MoreBonuses_Button 
: AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_Button_Parent
{
    protected override void Start()
    {
        price_coins_buy = 100;
        price_coins_improve = 300;

        IsBought = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreBonuses_IsBought;
        IsImproved = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreBonuses_IsImproved;

        Buy = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreBonuses_Buy;
        Improve = ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreBonuses_Improve;
        Animation = AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_MoreBonuses_Entity.SingleOnScene.Animation_Start;

        base.Start();
    }
}
