using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_CoinMagnet_Entity 
: AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_Entity_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_CoinMagnet_Entity SingleOnScene { get; private set; }

    [SerializeField] private Text text_bonusName;

    public void Text_LanguageRefresh()
    {
        switch (ControlPers_LanguageHandler.SingleOnScene.GameLanguage_Current)
        {
            case ControlPers_LanguageHandler.GameLanguage.english:
                text_bonusName.text = "COIN MAGNET";
            break;

            case ControlPers_LanguageHandler.GameLanguage.russian:
                text_bonusName.text = "Ã¿√Õ»“ ƒÀﬂ ÃŒÕ≈“";
            break;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    private void Start()
    {
        Text_LanguageRefresh();
    }
}
