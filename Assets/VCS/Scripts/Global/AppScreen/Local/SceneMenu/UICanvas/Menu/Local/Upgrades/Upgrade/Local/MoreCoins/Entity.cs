using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_MoreCoins_Entity 
: AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_Entity_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_MoreCoins_Entity SingleOnScene { get; private set; }

    [SerializeField] private Text text_bonusName;

    public void Text_LanguageRefresh()
    {
        text_bonusName.text = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.upgrade_moreCoins);
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;        
    }

    private void Start()
    {
        Text_LanguageRefresh();
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += Text_LanguageRefresh;
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate -= Text_LanguageRefresh;
    }
}
