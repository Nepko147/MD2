using UnityEngine;
using System.Collections.Generic;

public class ControlPers_LanguageHandler : MonoBehaviour
{
    public static ControlPers_LanguageHandler SingleOnScene { get; private set; }

    #region Buttons

    public enum ButtonName
    {
        play,
        upgrades,
        settings,
        quit,
        switch_en,
        switch_ru,
        upgrade,
        ok,
        menu,
        resume,
        revive,
        restart
    }

    #region English

    [SerializeField] private Sprite button_en_play_idle;
    [SerializeField] private Sprite button_en_play_pointed;
    [SerializeField] private Sprite button_en_play_pressed;

    [SerializeField] private Sprite button_en_upgrades_idle;
    [SerializeField] private Sprite button_en_upgrades_pointed;
    [SerializeField] private Sprite button_en_upgrades_pressed;

    [SerializeField] private Sprite button_en_settings_idle;
    [SerializeField] private Sprite button_en_settings_pointed;
    [SerializeField] private Sprite button_en_settings_pressed;

    [SerializeField] private Sprite button_en_quit_idle;
    [SerializeField] private Sprite button_en_quit_pointed;
    [SerializeField] private Sprite button_en_quit_pressed;

    [SerializeField] private Sprite button_en_switch_en_idle;
    [SerializeField] private Sprite button_en_switch_en_pointed;
    [SerializeField] private Sprite button_en_switch_en_pressed;

    [SerializeField] private Sprite button_en_switch_ru_idle;
    [SerializeField] private Sprite button_en_switch_ru_pointed;
    [SerializeField] private Sprite button_en_switch_ru_pressed;

    [SerializeField] private Sprite button_en_idle_buy;
    [SerializeField] private Sprite button_en_idle_improve;
    [SerializeField] private Sprite button_en_pointed_buy;
    [SerializeField] private Sprite button_en_pointed_improve;
    [SerializeField] private Sprite button_en_received;

    [SerializeField] private Sprite button_en_ok_idle;
    [SerializeField] private Sprite button_en_ok_pointed;
    [SerializeField] private Sprite button_en_ok_pressed;

    [SerializeField] private Sprite button_en_menu_idle;
    [SerializeField] private Sprite button_en_menu_pointed;
    [SerializeField] private Sprite button_en_menu_pressed;

    [SerializeField] private Sprite button_en_resume_idle;
    [SerializeField] private Sprite button_en_resume_pointed;
    [SerializeField] private Sprite button_en_resume_pressed;

    [SerializeField] private Sprite button_en_revive_idle;
    [SerializeField] private Sprite button_en_revive_pointed;
    [SerializeField] private Sprite button_en_revive_pressed;

    [SerializeField] private Sprite button_en_restart_idle;
    [SerializeField] private Sprite button_en_restart_pointed;
    [SerializeField] private Sprite button_en_restart_pressed;

    #endregion

    #region Russian

    [SerializeField] private Sprite button_ru_play_idle;
    [SerializeField] private Sprite button_ru_play_pointed;
    [SerializeField] private Sprite button_ru_play_pressed;

    [SerializeField] private Sprite button_ru_upgrades_idle;
    [SerializeField] private Sprite button_ru_upgrades_pointed;
    [SerializeField] private Sprite button_ru_upgrades_pressed;

    [SerializeField] private Sprite button_ru_settings_idle;
    [SerializeField] private Sprite button_ru_settings_pointed;
    [SerializeField] private Sprite button_ru_settings_pressed;

    [SerializeField] private Sprite button_ru_quit_idle;
    [SerializeField] private Sprite button_ru_quit_pointed;
    [SerializeField] private Sprite button_ru_quit_pressed;

    [SerializeField] private Sprite button_ru_switch_en_idle;
    [SerializeField] private Sprite button_ru_switch_en_pointed;
    [SerializeField] private Sprite button_ru_switch_en_pressed;

    [SerializeField] private Sprite button_ru_switch_ru_idle;
    [SerializeField] private Sprite button_ru_switch_ru_pointed;
    [SerializeField] private Sprite button_ru_switch_ru_pressed;

    [SerializeField] private Sprite button_ru_idle_buy;
    [SerializeField] private Sprite button_ru_idle_improve;
    [SerializeField] private Sprite button_ru_pointed_buy;
    [SerializeField] private Sprite button_ru_pointed_improve;
    [SerializeField] private Sprite button_ru_received;

    [SerializeField] private Sprite button_ru_ok_idle;
    [SerializeField] private Sprite button_ru_ok_pointed;
    [SerializeField] private Sprite button_ru_ok_pressed;

    [SerializeField] private Sprite button_ru_menu_idle;
    [SerializeField] private Sprite button_ru_menu_pointed;
    [SerializeField] private Sprite button_ru_menu_pressed;

    [SerializeField] private Sprite button_ru_resume_idle;
    [SerializeField] private Sprite button_ru_resume_pointed;
    [SerializeField] private Sprite button_ru_resume_pressed;

    [SerializeField] private Sprite button_ru_revive_idle;
    [SerializeField] private Sprite button_ru_revive_pointed;
    [SerializeField] private Sprite button_ru_revive_pressed;

    [SerializeField] private Sprite button_ru_restart_idle;
    [SerializeField] private Sprite button_ru_restart_pointed;
    [SerializeField] private Sprite button_ru_restart_pressed;

    #endregion

    #endregion

    #region Text

    public enum Text_Key
    {
        startText,
        loadingCloudData,
        upgrade_moreCoins,
        upgrade_moreBonuses,
        upgrade_coinMagnet,
        upgrade_heDidNotDie,
        popUpMessage_notEnoughCoins,
        indicators_complete,
        midscreen_gameOver,
        midscreen_pause,
        midscreen_distanceRemain,
        popUp_up,
        popUp_coin,
        popUp_coinRush
    }

    private Dictionary<Text_Key, string> text_dictionary_en = new Dictionary<Text_Key, string>()
    {
        [Text_Key.startText] = "PRESS ANY KEY",
        [Text_Key.loadingCloudData] = "LOADING CLOUD DATA",
        [Text_Key.upgrade_moreCoins] = "MORE COINS",
        [Text_Key.upgrade_moreBonuses] = "MORE BONUSES",
        [Text_Key.upgrade_coinMagnet] = "COIN MAGNET",
        [Text_Key.upgrade_heDidNotDie] = "HE DIDN'T DIE",
        [Text_Key.popUpMessage_notEnoughCoins] = "NOT ENOUGH COINS",
        [Text_Key.indicators_complete] = "KILOMETERS LEFT",
        [Text_Key.midscreen_gameOver] = "GAME OVER",
        [Text_Key.midscreen_pause] = "PAUSE",
        [Text_Key.midscreen_distanceRemain] = "Distance remain",
        [Text_Key.popUp_up] = "+1 UP",
        [Text_Key.popUp_coin] = "+1 Coin",
        [Text_Key.popUp_coinRush] = "Coin Rush!"
    };

    private Dictionary<Text_Key, string> text_dictionary_ru = new Dictionary<Text_Key, string>()
    {
        [Text_Key.startText] = "Õ¿∆Ã»“≈ Àﬁ¡”ﬁ  À¿¬»ÿ”",
        [Text_Key.loadingCloudData] = "«¿√–”« ¿ ƒ¿ÕÕ€’",
        [Text_Key.upgrade_moreCoins] = "¡ŒÀ‹ÿ≈ ÃŒÕ≈“",
        [Text_Key.upgrade_moreBonuses] = "¡ŒÀ‹ÿ≈ ¡ŒÕ”—Œ¬",
        [Text_Key.upgrade_coinMagnet] = "Ã¿√Õ»“ ƒÀﬂ ÃŒÕ≈“",
        [Text_Key.upgrade_heDidNotDie] = "ƒ¿ Õ≈ ”Ã≈– ŒÕ",
        [Text_Key.popUpMessage_notEnoughCoins] = "Õ≈ƒŒ—“¿“Œ◊ÕŒ ÃŒÕ≈“",
        [Text_Key.indicators_complete] = " Ã ƒŒ ÷≈À»",
        [Text_Key.midscreen_gameOver] = " ŒÕ≈÷ »√–€",
        [Text_Key.midscreen_pause] = "œ¿”«¿",
        [Text_Key.midscreen_distanceRemain] = "ŒÒÚ‡ÎÓÒ¸ ‰Ó ˆÂÎË",
        [Text_Key.popUp_up] = "+1 ∆ËÁÌ¸",
        [Text_Key.popUp_coin] = "+1 ÃÓÌÂÚ‡",
        [Text_Key.popUp_coinRush] = "ÃÓÌÂÚÌ‡ˇ ÀËıÓ‡‰Í‡!"
    };

    public string Text_Get(Text_Key _key)
    {
        var _text = string.Empty;

        switch (GameLanguage_Current)
        {
            case GameLanguage.english:
                _text = text_dictionary_en[_key];
                break;
            case GameLanguage.russian:
                _text = text_dictionary_ru[_key];
                break;
        }

        return _text;
    }

    #endregion

    #region Dialogue    

    public List<string[]> Dialogue_Get(string _actor_player, string _actor_npc)
    {  
        var _dialogue = new List<string[]>();

        switch (GameLanguage_Current)
        {
            case GameLanguage.english:
                string[][] _dialogue_array_en = new string[][]
                {
                    new[] { _actor_player, "Hello." },
                    new[] { _actor_npc, "Hey, handsome." },
                    new[] { _actor_npc, "I got an extra box of toothpicks on sale. You can just..." },
                    new[] { _actor_npc, "Come and take it..." },
                    new[] { _actor_npc, "Interested?" },
                    new[] { _actor_player, "Already driving." }
                };

                foreach (var _string in _dialogue_array_en)
                {
                    _dialogue.Add(_string);
                }               
            break;
            case GameLanguage.russian:

                string[][] _dialogue_array_ru = new string[][]
                {
                    new[] { _actor_player, "¿ÎÓ." },
                    new[] { _actor_npc, "œË‚ÂÚ Í‡Ò‡‚˜ËÍ." },
                    new[] { _actor_npc, "ﬂ ÚÛÚ ÔÓÎÛ˜ËÎ‡ ÎË¯Ì˛˛ ÍÓÓ·ÍÛ ÁÛ·Ó˜ËÒÚÓÍ ÔÓ ‡ÍˆËË. ÃÓÊÂ¯¸ ÔÓÒÚÓ..." },
                    new[] { _actor_npc, "œËÈÚË Ë ‚ÁˇÚ¸ Â∏..." },
                    new[] { _actor_npc, "»ÌÚÂÂÒÛÂÚ?" },
                    new[] { _actor_player, "”ÊÂ Â‰Û." }
                };

                foreach (var _string in _dialogue_array_ru)
                {
                    _dialogue.Add(_string);
                }
            break;
        }

        return _dialogue;
    }

    #endregion

    public enum GameLanguage
    {
        english,
        russian
    }

    public GameLanguage GameLanguage_Current { get; private set; }

    public void SetGameLanguage(GameLanguage _language)
    {
        GameLanguage_Current = _language;

        GameLanguage_OnUpdate();

        ControlPers_DataHandler.SingleOnScene.SettingsData_LanguageValue = GameLanguage_Current;        
    }

    public delegate void GameLanguage_Update();
    public event GameLanguage_Update GameLanguage_OnUpdate;

    public Sprite[] GetSprites(ButtonName _buttonName, int _numberOfSprites)
    {
        Sprite[] _spriteArray = new Sprite[_numberOfSprites];

        switch (GameLanguage_Current)
        {
            case GameLanguage.english:
                
                switch (_buttonName)
                {
                    case ButtonName.play:

                        _spriteArray[0] = button_en_play_idle;
                        _spriteArray[1] = button_en_play_pointed;
                        _spriteArray[2] = button_en_play_pressed;

                    break;

                    case ButtonName.upgrades:

                        _spriteArray[0] = button_en_upgrades_idle;
                        _spriteArray[1] = button_en_upgrades_pointed;
                        _spriteArray[2] = button_en_upgrades_pressed;

                    break;

                    case ButtonName.settings:

                        _spriteArray[0] = button_en_settings_idle;
                        _spriteArray[1] = button_en_settings_pointed;
                        _spriteArray[2] = button_en_settings_pressed;

                    break;

                    case ButtonName.quit:

                        _spriteArray[0] = button_en_quit_idle;
                        _spriteArray[1] = button_en_quit_pointed;
                        _spriteArray[2] = button_en_quit_pressed;

                    break;

                    case ButtonName.switch_en:

                        _spriteArray[0] = button_en_switch_en_idle;
                        _spriteArray[1] = button_en_switch_en_pointed;
                        _spriteArray[2] = button_en_switch_en_pressed;

                    break;

                    case ButtonName.switch_ru:

                        _spriteArray[0] = button_en_switch_ru_idle;
                        _spriteArray[1] = button_en_switch_ru_pointed;
                        _spriteArray[2] = button_en_switch_ru_pressed;

                    break;

                    case ButtonName.upgrade:

                        _spriteArray[0] = button_en_idle_buy;
                        _spriteArray[1] = button_en_idle_improve;
                        _spriteArray[2] = button_en_pointed_buy;
                        _spriteArray[3] = button_en_pointed_improve;
                        _spriteArray[4] = button_en_received;

                    break;

                    case ButtonName.ok:

                        _spriteArray[0] = button_en_ok_idle;
                        _spriteArray[1] = button_en_ok_pointed;
                        _spriteArray[2] = button_en_ok_pressed;

                    break;

                    case ButtonName.menu:

                        _spriteArray[0] = button_en_menu_idle;
                        _spriteArray[1] = button_en_menu_pointed;
                        _spriteArray[2] = button_en_menu_pressed;

                    break;

                    case ButtonName.resume:
                        
                        _spriteArray[0] = button_en_resume_idle;
                        _spriteArray[1] = button_en_resume_pointed;
                        _spriteArray[2] = button_en_resume_pressed;
                        
                    break;

                    case ButtonName.revive:

                        _spriteArray[0] = button_en_revive_idle;
                        _spriteArray[1] = button_en_revive_pointed;
                        _spriteArray[2] = button_en_revive_pressed;

                    break;

                    case ButtonName.restart:

                        _spriteArray[0] = button_en_restart_idle;
                        _spriteArray[1] = button_en_restart_pointed;
                        _spriteArray[2] = button_en_restart_pressed;

                    break;
                }

                break;

            case GameLanguage.russian:

                switch (_buttonName)
                {
                    case ButtonName.play:

                        _spriteArray[0] = button_ru_play_idle;
                        _spriteArray[1] = button_ru_play_pointed;
                        _spriteArray[2] = button_ru_play_pressed;

                    break;

                    case ButtonName.upgrades:

                        _spriteArray[0] = button_ru_upgrades_idle;
                        _spriteArray[1] = button_ru_upgrades_pointed;
                        _spriteArray[2] = button_ru_upgrades_pressed;

                    break;

                    case ButtonName.settings:

                        _spriteArray[0] = button_ru_settings_idle;
                        _spriteArray[1] = button_ru_settings_pointed;
                        _spriteArray[2] = button_ru_settings_pressed;

                    break;

                    case ButtonName.quit:

                        _spriteArray[0] = button_ru_quit_idle;
                        _spriteArray[1] = button_ru_quit_pointed;
                        _spriteArray[2] = button_ru_quit_pressed;

                    break;

                    case ButtonName.switch_en:

                        _spriteArray[0] = button_ru_switch_en_idle;
                        _spriteArray[1] = button_ru_switch_en_pointed;
                        _spriteArray[2] = button_ru_switch_en_pressed;

                    break;

                    case ButtonName.switch_ru:

                        _spriteArray[0] = button_ru_switch_ru_idle;
                        _spriteArray[1] = button_ru_switch_ru_pointed;
                        _spriteArray[2] = button_ru_switch_ru_pressed;

                    break;

                    case ButtonName.upgrade:

                        _spriteArray[0] = button_ru_idle_buy;
                        _spriteArray[1] = button_ru_idle_improve;
                        _spriteArray[2] = button_ru_pointed_buy;
                        _spriteArray[3] = button_ru_pointed_improve;
                        _spriteArray[4] = button_ru_received;

                    break;

                    case ButtonName.ok:

                        _spriteArray[0] = button_ru_ok_idle;
                        _spriteArray[1] = button_ru_ok_pointed;
                        _spriteArray[2] = button_ru_ok_pressed;

                    break;

                    case ButtonName.menu:

                        _spriteArray[0] = button_ru_menu_idle;
                        _spriteArray[1] = button_ru_menu_pointed;
                        _spriteArray[2] = button_ru_menu_pressed;

                    break;

                    case ButtonName.resume:

                        _spriteArray[0] = button_ru_resume_idle;
                        _spriteArray[1] = button_ru_resume_pointed;
                        _spriteArray[2] = button_ru_resume_pressed;

                    break;

                    case ButtonName.revive:

                        _spriteArray[0] = button_ru_revive_idle;
                        _spriteArray[1] = button_ru_revive_pointed;
                        _spriteArray[2] = button_ru_revive_pressed;

                    break;

                    case ButtonName.restart:

                        _spriteArray[0] = button_ru_restart_idle;
                        _spriteArray[1] = button_ru_restart_pointed;
                        _spriteArray[2] = button_ru_restart_pressed;

                    break;
                }

                break;
        }        

        return _spriteArray;
    }
        
    private void Awake()
    {
        SingleOnScene = this;
    }

    private void Start()
    {
        GameLanguage_Current = ControlPers_DataHandler.SingleOnScene.SettingsData_LanguageValue;

        GameLanguage_OnUpdate();
    }
}
