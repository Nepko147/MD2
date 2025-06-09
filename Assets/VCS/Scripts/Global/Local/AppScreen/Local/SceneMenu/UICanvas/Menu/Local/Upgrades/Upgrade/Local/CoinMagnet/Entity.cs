using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_CoinMagnet_Entity 
: AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_Entity_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_CoinMagnet_Entity SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }
}
