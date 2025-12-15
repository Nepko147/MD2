using UnityEngine;
using System.Collections.Generic;

public class ControlPers_LanguageHandler_Entity : MonoBehaviour
{
    #region General

    public static ControlPers_LanguageHandler_Entity SingleOnScene { get; private set; }

    #endregion

    #region GameLanguage
    
    public enum GameLanguage_State
    {
        english,
        russian,
        spanish,
        portuguese,
        german,
        french,
        italian,
        polish,
        turkish,
        kazakh,
        belarusian,
        ukrainian,
        uzbek,
        indonesian
    }
    
    public GameLanguage_State GameLanguage_State_Current { get; private set; }

    [SerializeField] private ControlPers_LanguageHandler_English gameLanguage_gameObject_english;
    [SerializeField] private ControlPers_LanguageHandler_Russian gameLanguage_gameObject_russian;
    [SerializeField] private ControlPers_LanguageHandler_Spanish gameLanguage_gameObject_spanish;
    [SerializeField] private ControlPers_LanguageHandler_Portuguese gameLanguage_gameObject_portuguese;
    [SerializeField] private ControlPers_LanguageHandler_German gameLanguage_gameObject_german;
    [SerializeField] private ControlPers_LanguageHandler_French gameLanguage_gameObject_french;
    [SerializeField] private ControlPers_LanguageHandler_Italian gameLanguage_gameObject_italian;
    [SerializeField] private ControlPers_LanguageHandler_Polish gameLanguage_gameObject_polish;
    [SerializeField] private ControlPers_LanguageHandler_Turkish gameLanguage_gameObject_turkish;
    [SerializeField] private ControlPers_LanguageHandler_Kazakh gameLanguage_gameObject_kazakh;
    [SerializeField] private ControlPers_LanguageHandler_Belarusian gameLanguage_gameObject_belarusian;
    [SerializeField] private ControlPers_LanguageHandler_Ukrainian gameLanguage_gameObject_ukrainian;
    [SerializeField] private ControlPers_LanguageHandler_Uzbek gameLanguage_gameObject_uzbek;
    [SerializeField] private ControlPers_LanguageHandler_Indonesian gameLanguage_gameObject_indonesian;

    private ControlPers_LanguageHandler_Parent gameLanguage_gameObject_current;

    private Dictionary<GameLanguage_State, ControlPers_LanguageHandler_Parent> gameLanguage_stateToGameObject = new Dictionary<GameLanguage_State, ControlPers_LanguageHandler_Parent>();

    public void GameLanguage_State_Set(GameLanguage_State _state)
    {
        GameLanguage_State_Current = _state;
        gameLanguage_gameObject_current = gameLanguage_stateToGameObject[_state];

        ControlPers_DataHandler.SingleOnScene.SettingsData_LanguageValue = GameLanguage_State_Current;

        GameLanguage_OnUpdate();      
    }

    public delegate void GameLanguage_Update();
    public event GameLanguage_Update GameLanguage_OnUpdate;

    #endregion

    #region Text

    public string Text_Get(Text_Key _key)
    {
        return (gameLanguage_gameObject_current.Text_Get(_key));
    }

    public enum Text_Key
    {
        startText_desktop,
        startText_mobile,
        loadingCloudData,
        upgrade_moreCoins,
        upgrade_moreBonuses,
        upgrade_coinMagnet,
        upgrade_heDidNotDie,
        popUpMessage_notEnoughCoins,
        tutorial_desktop,
        tutorial_mobile,
        indicators_complete,
        midscreen_gameOver,
        midscreen_pause,
        midscreen_distanceRemain,
        received_text,
        received_ad_text,
        popUp_up,
        popUp_coin,
        popUp_coinRush,
        gameBy,
        statistics_reviveNumber,
        statistics_coinsTotal,
        statistics_coinsSpentOnRevivals,
        statistics_defeats,
        statistics_totalDrivings,
        statistics_best,
        statistics_newRecord,
        statistics_gameCompleted,
        dialogue_start_string_1,
        dialogue_start_string_2,
        dialogue_start_string_3,
        dialogue_start_string_4,
        dialogue_start_string_5,
        dialogue_start_string_6,
        dialogue_end_string_1,
        dialogue_end_string_2,
        dialogue_end_string_3,
        dialogue_end_string_4,
        dialogue_end_string_5,
        dialogue_end_string_6,
        dialogue_end_string_7,
        radio_string_early_1,
        radio_string_early_2,
        radio_string_early_3,
        radio_string_early_4,
        radio_string_early_5,
        radio_string_early_6,
        radio_string_early_7,
        radio_string_late_1,
        radio_string_late_2,
        radio_string_late_3,
        radio_string_late_4,
        radio_string_late_5,
        radio_string_late_6,
        radio_string_late_7
    }

    #endregion

    #region Sprite

    public Sprite Sprite_Get(Sprite_Key _key)
    {
        return (gameLanguage_gameObject_current.Sprite_Get(_key));
    }

    public enum Sprite_Key
    {
        button_play_idle,
        button_play_pointed,
        button_play_pressed,
        button_upgrades_idle,
        button_upgrades_pointed,
        button_upgrades_pressed,
        button_settings_idle,
        button_settings_pointed,
        button_settings_pressed,
        button_quit_idle,
        button_quit_pointed,
        button_quit_pressed,
        button_upgrade_buy_idle,
        button_upgrade_buy_pointed,
        button_upgrade_improve_idle,
        button_upgrade_improve_pointed,
        button_upgrade_received,
        button_ok_idle,
        button_ok_pointed,
        button_ok_pressed,
        button_menu_idle,
        button_menu_pointed,
        button_menu_pressed,
        button_resume_idle,
        button_resume_pointed,
        button_resume_pressed,
        button_restart_idle,
        button_restart_pointed,
        button_restart_pressed,
        button_revive_idle,
        button_revive_pointed,
        button_revive_pressed
    }

    #endregion
   
    private void Awake()
    {
        SingleOnScene = this;

        gameLanguage_gameObject_current = gameLanguage_gameObject_english;

        gameLanguage_stateToGameObject[GameLanguage_State.english] = gameLanguage_gameObject_english;
        gameLanguage_stateToGameObject[GameLanguage_State.russian] = gameLanguage_gameObject_russian;
        gameLanguage_stateToGameObject[GameLanguage_State.spanish] = gameLanguage_gameObject_spanish;
        gameLanguage_stateToGameObject[GameLanguage_State.portuguese] = gameLanguage_gameObject_portuguese;
        gameLanguage_stateToGameObject[GameLanguage_State.german] = gameLanguage_gameObject_german;
        gameLanguage_stateToGameObject[GameLanguage_State.french] = gameLanguage_gameObject_french;
        gameLanguage_stateToGameObject[GameLanguage_State.italian] = gameLanguage_gameObject_italian;
        gameLanguage_stateToGameObject[GameLanguage_State.polish] = gameLanguage_gameObject_polish;
        gameLanguage_stateToGameObject[GameLanguage_State.turkish] = gameLanguage_gameObject_turkish;
        gameLanguage_stateToGameObject[GameLanguage_State.kazakh] = gameLanguage_gameObject_kazakh;
        gameLanguage_stateToGameObject[GameLanguage_State.belarusian] = gameLanguage_gameObject_belarusian;
        gameLanguage_stateToGameObject[GameLanguage_State.ukrainian] = gameLanguage_gameObject_ukrainian;
        gameLanguage_stateToGameObject[GameLanguage_State.uzbek] = gameLanguage_gameObject_uzbek;
        gameLanguage_stateToGameObject[GameLanguage_State.indonesian] = gameLanguage_gameObject_indonesian;
    }

    private void Start()
    {
        GameLanguage_State_Set(ControlPers_DataHandler.SingleOnScene.SettingsData_LanguageValue);

        GameLanguage_OnUpdate();
    }
}
