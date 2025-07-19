using UnityEngine;

public class ControlPers_LanguageHandler : MonoBehaviour
{
    public static ControlPers_LanguageHandler SingleOnScene { get; private set; }

    public enum GameLanguage
    {
        english,
        russian
    }

    public GameLanguage CurrentGameLanguage { get; private set; }

    public void SetGameLanguage(GameLanguage _language)
    {
        CurrentGameLanguage = _language;

        foreach (var _item in FindObjectsByType<AppScreen_General_UICanvas_Button_Parent>(FindObjectsSortMode.None))
        {
            _item.Image_LanguageRefresh();
        }
        
        foreach (var _item in FindObjectsByType<AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_Button_Parent>(FindObjectsSortMode.None))
        {
            _item.Image_LanguageRefresh();
        }

        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_MoreCoins_Entity.SingleOnScene.Text_LanguageRefresh();
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_MoreBonuses_Entity.SingleOnScene.Text_LanguageRefresh();
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_CoinMagnet_Entity.SingleOnScene.Text_LanguageRefresh();
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_Local_Revive_Entity.SingleOnScene.Text_LanguageRefresh();

        ControlPers_DataHandler.SingleOnScene.SettingsData_LanguageValue = CurrentGameLanguage;
    }

    private void Awake()
    {
        SingleOnScene = this;
    }

    private void Start()
    {        
        CurrentGameLanguage = ControlPers_DataHandler.SingleOnScene.SettingsData_LanguageValue;

        foreach (var _item in FindObjectsByType<AppScreen_General_UICanvas_Button_Parent>(FindObjectsSortMode.None))
        {
            _item.Image_LanguageRefresh();
        }
    }
}
