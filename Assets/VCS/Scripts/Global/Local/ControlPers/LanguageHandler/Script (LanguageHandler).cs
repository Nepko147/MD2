using UnityEngine;

public class ControlPers_LanguageHandler : MonoBehaviour
{
    public static ControlPers_LanguageHandler SingleOnScene { get; private set; }

    #region Buttons

    public const string BUTTON_NAME_PLAY = "play";
    public const string BUTTON_NAME_UPGRADES = "upgrades";
    public const string BUTTON_NAME_SETTINGS = "settings";
    public const string BUTTON_NAME_QUIT = "quit";
    public const string BUTTON_NAME_SWITCH_EN = "switch en";
    public const string BUTTON_NAME_SWITCH_RU = "switch ru";
    public const string BUTTON_NAME_UPGRADE = "upgrede";
    public const string BUTTON_NAME_MENU = "menu";
    public const string BUTTON_NAME_RESUME = "resume";
    public const string BUTTON_NAME_REVIVE = "revive";
    public const string BUTTON_NAME_RESTART = "restart";

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

    public Sprite[] GetSprites(string _buttonName, int _numberOfSprites)
    {
        Sprite[] _spriteArray = new Sprite[_numberOfSprites];

        switch (GameLanguage_Current)
        {
            case GameLanguage.english:
                
                switch (_buttonName)
                {
                    case BUTTON_NAME_PLAY:

                        _spriteArray[0] = button_en_play_idle;
                        _spriteArray[1] = button_en_play_pointed;
                        _spriteArray[2] = button_en_play_pressed;

                    break;

                    case BUTTON_NAME_UPGRADES:

                        _spriteArray[0] = button_en_upgrades_idle;
                        _spriteArray[1] = button_en_upgrades_pointed;
                        _spriteArray[2] = button_en_upgrades_pressed;

                    break;

                    case BUTTON_NAME_SETTINGS:

                        _spriteArray[0] = button_en_settings_idle;
                        _spriteArray[1] = button_en_settings_pointed;
                        _spriteArray[2] = button_en_settings_pressed;

                    break;

                    case BUTTON_NAME_QUIT:

                        _spriteArray[0] = button_en_quit_idle;
                        _spriteArray[1] = button_en_quit_pointed;
                        _spriteArray[2] = button_en_quit_pressed;

                    break;

                    case BUTTON_NAME_SWITCH_EN:

                        _spriteArray[0] = button_en_switch_en_idle;
                        _spriteArray[1] = button_en_switch_en_pointed;
                        _spriteArray[2] = button_en_switch_en_pressed;

                    break;

                    case BUTTON_NAME_SWITCH_RU:

                        _spriteArray[0] = button_en_switch_ru_idle;
                        _spriteArray[1] = button_en_switch_ru_pointed;
                        _spriteArray[2] = button_en_switch_ru_pressed;

                    break;

                    case BUTTON_NAME_UPGRADE:

                        _spriteArray[0] = button_en_idle_buy;
                        _spriteArray[1] = button_en_idle_improve;
                        _spriteArray[2] = button_en_pointed_buy;
                        _spriteArray[3] = button_en_pointed_improve;
                        _spriteArray[4] = button_en_received;

                    break;

                    case BUTTON_NAME_MENU:

                        _spriteArray[0] = button_en_menu_idle;
                        _spriteArray[1] = button_en_menu_pointed;
                        _spriteArray[2] = button_en_menu_pressed;

                    break;

                    case BUTTON_NAME_RESUME:
                        
                        _spriteArray[0] = button_en_resume_idle;
                        _spriteArray[1] = button_en_resume_pointed;
                        _spriteArray[2] = button_en_resume_pressed;
                        
                    break;

                    case BUTTON_NAME_REVIVE:

                        _spriteArray[0] = button_en_revive_idle;
                        _spriteArray[1] = button_en_revive_pointed;
                        _spriteArray[2] = button_en_revive_pressed;

                    break;

                    case BUTTON_NAME_RESTART:

                        _spriteArray[0] = button_en_restart_idle;
                        _spriteArray[1] = button_en_restart_pointed;
                        _spriteArray[2] = button_en_restart_pressed;

                    break;
                }

                break;

            case GameLanguage.russian:

                switch (_buttonName)
                {
                    case BUTTON_NAME_PLAY:

                        _spriteArray[0] = button_ru_play_idle;
                        _spriteArray[1] = button_ru_play_pointed;
                        _spriteArray[2] = button_ru_play_pressed;

                    break;

                    case BUTTON_NAME_UPGRADES:

                        _spriteArray[0] = button_ru_upgrades_idle;
                        _spriteArray[1] = button_ru_upgrades_pointed;
                        _spriteArray[2] = button_ru_upgrades_pressed;

                    break;

                    case BUTTON_NAME_SETTINGS:

                        _spriteArray[0] = button_ru_settings_idle;
                        _spriteArray[1] = button_ru_settings_pointed;
                        _spriteArray[2] = button_ru_settings_pressed;

                    break;

                    case BUTTON_NAME_QUIT:

                        _spriteArray[0] = button_ru_quit_idle;
                        _spriteArray[1] = button_ru_quit_pointed;
                        _spriteArray[2] = button_ru_quit_pressed;

                    break;

                    case BUTTON_NAME_SWITCH_EN:

                        _spriteArray[0] = button_ru_switch_en_idle;
                        _spriteArray[1] = button_ru_switch_en_pointed;
                        _spriteArray[2] = button_ru_switch_en_pressed;

                    break;

                    case BUTTON_NAME_SWITCH_RU:

                        _spriteArray[0] = button_ru_switch_ru_idle;
                        _spriteArray[1] = button_ru_switch_ru_pointed;
                        _spriteArray[2] = button_ru_switch_ru_pressed;

                    break;

                    case BUTTON_NAME_UPGRADE:

                        _spriteArray[0] = button_ru_idle_buy;
                        _spriteArray[1] = button_ru_idle_improve;
                        _spriteArray[2] = button_ru_pointed_buy;
                        _spriteArray[3] = button_ru_pointed_improve;
                        _spriteArray[4] = button_ru_received;

                    break;

                    case BUTTON_NAME_MENU:

                        _spriteArray[0] = button_ru_menu_idle;
                        _spriteArray[1] = button_ru_menu_pointed;
                        _spriteArray[2] = button_ru_menu_pressed;

                    break;

                    case BUTTON_NAME_RESUME:

                        _spriteArray[0] = button_ru_resume_idle;
                        _spriteArray[1] = button_ru_resume_pointed;
                        _spriteArray[2] = button_ru_resume_pressed;

                    break;

                    case BUTTON_NAME_REVIVE:

                        _spriteArray[0] = button_ru_revive_idle;
                        _spriteArray[1] = button_ru_revive_pointed;
                        _spriteArray[2] = button_ru_revive_pressed;

                    break;

                    case BUTTON_NAME_RESTART:

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
