using UnityEngine;

public class AppScrren_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_PopUpMessage_Button
: AppScreen_General_UICanvas_Button_Parent
{
    public static AppScrren_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_PopUpMessage_Button SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        ActiveOnPopUpMessage = true;

        SingleOnScene = this;
    }
}
