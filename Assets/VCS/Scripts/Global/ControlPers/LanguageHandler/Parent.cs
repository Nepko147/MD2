using System.Collections.Generic;
using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_Parent : MonoBehaviour
{
    #region General

    protected const string HEX_CYAN = "#9aeff3";
    protected const string HEX_MAGENTA = "#ff2373";

    #endregion

    #region Text

    protected Dictionary<Text_Key, string> text_keyToString = new Dictionary<Text_Key, string>();

    public string Text_Get(Text_Key _key)
    { 
        return (text_keyToString[_key]);
    }

    #endregion

    #region Sprite

    protected Dictionary<Sprite_Key, Sprite> sprite_keyToSprite = new Dictionary<Sprite_Key, Sprite>();

    public Sprite Sprite_Get(Sprite_Key _key)
    {
        return (sprite_keyToSprite[_key]);
    }

    [SerializeField] private Sprite sprite_button_play_idle;
    [SerializeField] private Sprite sprite_button_play_pointed;
    [SerializeField] private Sprite sprite_button_play_pressed;

    [SerializeField] private Sprite sprite_button_upgrades_idle;
    [SerializeField] private Sprite sprite_button_upgrades_pointed;
    [SerializeField] private Sprite sprite_button_upgrades_pressed;

    [SerializeField] private Sprite sprite_button_settings_idle;
    [SerializeField] private Sprite sprite_button_settings_pointed;
    [SerializeField] private Sprite sprite_button_settings_pressed;

    [SerializeField] private Sprite sprite_button_quit_idle;
    [SerializeField] private Sprite sprite_button_quit_pointed;
    [SerializeField] private Sprite sprite_button_quit_pressed;

    [SerializeField] private Sprite sprite_button_upgrade_buy_idle;
    [SerializeField] private Sprite sprite_button_upgrade_buy_pointed;
    [SerializeField] private Sprite sprite_button_upgrade_improve_idle;
    [SerializeField] private Sprite sprite_button_upgrade_improve_pointed;
    [SerializeField] private Sprite sprite_button_upgrade_received;

    [SerializeField] private Sprite sprite_button_ok_idle;
    [SerializeField] private Sprite sprite_button_ok_pointed;
    [SerializeField] private Sprite sprite_button_ok_pressed;

    [SerializeField] private Sprite sprite_button_menu_idle;
    [SerializeField] private Sprite sprite_button_menu_pointed;
    [SerializeField] private Sprite sprite_button_menu_pressed;

    [SerializeField] private Sprite sprite_button_resume_idle;
    [SerializeField] private Sprite sprite_button_resume_pointed;
    [SerializeField] private Sprite sprite_button_resume_pressed;

    [SerializeField] private Sprite sprite_button_revive_idle;
    [SerializeField] private Sprite sprite_button_revive_pointed;
    [SerializeField] private Sprite sprite_button_revive_pressed;

    [SerializeField] private Sprite sprite_button_restart_idle;
    [SerializeField] private Sprite sprite_button_restart_pointed;
    [SerializeField] private Sprite sprite_button_restart_pressed;

    #endregion

    protected virtual void Awake()
    {
        sprite_keyToSprite[Sprite_Key.button_play_idle] = sprite_button_play_idle;
        sprite_keyToSprite[Sprite_Key.button_play_pointed] = sprite_button_play_pointed;
        sprite_keyToSprite[Sprite_Key.button_play_pressed] = sprite_button_play_pressed;

        sprite_keyToSprite[Sprite_Key.button_upgrades_idle] = sprite_button_upgrades_idle;
        sprite_keyToSprite[Sprite_Key.button_upgrades_pointed] = sprite_button_upgrades_pointed;
        sprite_keyToSprite[Sprite_Key.button_upgrades_pressed] = sprite_button_upgrades_pressed;

        sprite_keyToSprite[Sprite_Key.button_settings_idle] = sprite_button_settings_idle;
        sprite_keyToSprite[Sprite_Key.button_settings_pointed] = sprite_button_settings_pointed;
        sprite_keyToSprite[Sprite_Key.button_settings_pressed] = sprite_button_settings_pressed;

        sprite_keyToSprite[Sprite_Key.button_quit_idle] = sprite_button_quit_idle;
        sprite_keyToSprite[Sprite_Key.button_quit_pointed] = sprite_button_quit_pointed;
        sprite_keyToSprite[Sprite_Key.button_quit_pressed] = sprite_button_quit_pressed;

        sprite_keyToSprite[Sprite_Key.button_upgrade_buy_idle] = sprite_button_upgrade_buy_idle;
        sprite_keyToSprite[Sprite_Key.button_upgrade_buy_pointed] = sprite_button_upgrade_buy_pointed;
        sprite_keyToSprite[Sprite_Key.button_upgrade_improve_idle] = sprite_button_upgrade_improve_idle;
        sprite_keyToSprite[Sprite_Key.button_upgrade_improve_pointed] = sprite_button_upgrade_improve_pointed;
        sprite_keyToSprite[Sprite_Key.button_upgrade_received] = sprite_button_upgrade_received;

        sprite_keyToSprite[Sprite_Key.button_ok_idle] = sprite_button_ok_idle;
        sprite_keyToSprite[Sprite_Key.button_ok_pointed] = sprite_button_ok_pointed;
        sprite_keyToSprite[Sprite_Key.button_ok_pressed] = sprite_button_ok_pressed;

        sprite_keyToSprite[Sprite_Key.button_menu_idle] = sprite_button_menu_idle;
        sprite_keyToSprite[Sprite_Key.button_menu_pointed] = sprite_button_menu_pointed;
        sprite_keyToSprite[Sprite_Key.button_menu_pressed] = sprite_button_menu_pressed;

        sprite_keyToSprite[Sprite_Key.button_resume_idle] = sprite_button_resume_idle;
        sprite_keyToSprite[Sprite_Key.button_resume_pointed] = sprite_button_resume_pointed;
        sprite_keyToSprite[Sprite_Key.button_resume_pressed] = sprite_button_resume_pressed;

        sprite_keyToSprite[Sprite_Key.button_revive_idle] = sprite_button_revive_idle;
        sprite_keyToSprite[Sprite_Key.button_revive_pointed] = sprite_button_revive_pointed;
        sprite_keyToSprite[Sprite_Key.button_revive_pressed] = sprite_button_revive_pressed;

        sprite_keyToSprite[Sprite_Key.button_restart_idle] = sprite_button_restart_idle;
        sprite_keyToSprite[Sprite_Key.button_restart_pointed] = sprite_button_restart_pointed;
        sprite_keyToSprite[Sprite_Key.button_restart_pressed] = sprite_button_restart_pressed;
    }
}
