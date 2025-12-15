using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Upgrades_Upgrade_Local_Revive_Entity
: AppScreen_UICanvas_Menu_Upgrades_Upgrade_General_Entity_Parent
{
    public static AppScreen_UICanvas_Menu_Upgrades_Upgrade_Local_Revive_Entity SingleOnScene { get; private set; }

    [SerializeField] private Text text_bonusName;

    public void Text_LanguageRefresh()
    {
        text_bonusName.text = ControlPers_LanguageHandler_Entity.SingleOnScene.Text_Get(ControlPers_LanguageHandler_Entity.Text_Key.upgrade_heDidNotDie);
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    private void Start()
    {
        Text_LanguageRefresh();
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate += Text_LanguageRefresh;
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate -= Text_LanguageRefresh;
    }
}
