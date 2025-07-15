using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_MoreBonuses_Entity 
: AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_Entity_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_MoreBonuses_Entity SingleOnScene { get; private set; }

    [SerializeField] private Text text_bonusName;

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        switch (ControlPers_LanguageHandler.SingleOnScene.CurrentGameLanguage)
        {
            case ControlPers_LanguageHandler.GameLanguage.english:
                text_bonusName.text = "MORE BONUSES";
                break;

            case ControlPers_LanguageHandler.GameLanguage.russian:
                text_bonusName.text = "анкэье анмсянб";
                break;
        }
    }
}
