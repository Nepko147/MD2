using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Upgrades_Upgrade_Local_CoinMagnet_Entity 
: AppScreen_UICanvas_Menu_Upgrades_Upgrade_General_Entity_Parent
{
    public static AppScreen_UICanvas_Menu_Upgrades_Upgrade_Local_CoinMagnet_Entity SingleOnScene { get; private set; }

    [SerializeField] private Text text_bonusName;

    public void Text_LanguageRefresh()
    {
        text_bonusName.text = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.upgrade_coinMagnet);
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
