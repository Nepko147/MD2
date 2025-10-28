﻿using UnityEngine;
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
        switch_es,
        switch_pt,
        switch_de,
        switch_id,
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

    [SerializeField] private Sprite button_en_switch_es_idle;
    [SerializeField] private Sprite button_en_switch_es_pointed;
    [SerializeField] private Sprite button_en_switch_es_pressed;

    [SerializeField] private Sprite button_en_switch_pt_idle;
    [SerializeField] private Sprite button_en_switch_pt_pointed;
    [SerializeField] private Sprite button_en_switch_pt_pressed;

    [SerializeField] private Sprite button_en_switch_de_idle;
    [SerializeField] private Sprite button_en_switch_de_pointed;
    [SerializeField] private Sprite button_en_switch_de_pressed;

    [SerializeField] private Sprite button_en_switch_id_idle;
    [SerializeField] private Sprite button_en_switch_id_pointed;
    [SerializeField] private Sprite button_en_switch_id_pressed;

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

    [SerializeField] private Sprite button_ru_switch_es_idle;
    [SerializeField] private Sprite button_ru_switch_es_pointed;
    [SerializeField] private Sprite button_ru_switch_es_pressed;

    [SerializeField] private Sprite button_ru_switch_pt_idle;
    [SerializeField] private Sprite button_ru_switch_pt_pointed;
    [SerializeField] private Sprite button_ru_switch_pt_pressed;

    [SerializeField] private Sprite button_ru_switch_de_idle;
    [SerializeField] private Sprite button_ru_switch_de_pointed;
    [SerializeField] private Sprite button_ru_switch_de_pressed;

    [SerializeField] private Sprite button_ru_switch_id_idle;
    [SerializeField] private Sprite button_ru_switch_id_pointed;
    [SerializeField] private Sprite button_ru_switch_id_pressed;

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

    #region Spanish

    [SerializeField] private Sprite button_es_play_idle;
    [SerializeField] private Sprite button_es_play_pointed;
    [SerializeField] private Sprite button_es_play_pressed;

    [SerializeField] private Sprite button_es_upgrades_idle;
    [SerializeField] private Sprite button_es_upgrades_pointed;
    [SerializeField] private Sprite button_es_upgrades_pressed;

    [SerializeField] private Sprite button_es_settings_idle;
    [SerializeField] private Sprite button_es_settings_pointed;
    [SerializeField] private Sprite button_es_settings_pressed;

    [SerializeField] private Sprite button_es_quit_idle;
    [SerializeField] private Sprite button_es_quit_pointed;
    [SerializeField] private Sprite button_es_quit_pressed;

    [SerializeField] private Sprite button_es_switch_en_idle;
    [SerializeField] private Sprite button_es_switch_en_pointed;
    [SerializeField] private Sprite button_es_switch_en_pressed;

    [SerializeField] private Sprite button_es_switch_ru_idle;
    [SerializeField] private Sprite button_es_switch_ru_pointed;
    [SerializeField] private Sprite button_es_switch_ru_pressed;

    [SerializeField] private Sprite button_es_switch_es_idle;
    [SerializeField] private Sprite button_es_switch_es_pointed;
    [SerializeField] private Sprite button_es_switch_es_pressed;

    [SerializeField] private Sprite button_es_switch_pt_idle;
    [SerializeField] private Sprite button_es_switch_pt_pointed;
    [SerializeField] private Sprite button_es_switch_pt_pressed;

    [SerializeField] private Sprite button_es_switch_de_idle;
    [SerializeField] private Sprite button_es_switch_de_pointed;
    [SerializeField] private Sprite button_es_switch_de_pressed;

    [SerializeField] private Sprite button_es_switch_id_idle;
    [SerializeField] private Sprite button_es_switch_id_pointed;
    [SerializeField] private Sprite button_es_switch_id_pressed;

    [SerializeField] private Sprite button_es_idle_buy;
    [SerializeField] private Sprite button_es_idle_improve;
    [SerializeField] private Sprite button_es_pointed_buy;
    [SerializeField] private Sprite button_es_pointed_improve;
    [SerializeField] private Sprite button_es_received;

    [SerializeField] private Sprite button_es_ok_idle;
    [SerializeField] private Sprite button_es_ok_pointed;
    [SerializeField] private Sprite button_es_ok_pressed;

    [SerializeField] private Sprite button_es_menu_idle;
    [SerializeField] private Sprite button_es_menu_pointed;
    [SerializeField] private Sprite button_es_menu_pressed;

    [SerializeField] private Sprite button_es_resume_idle;
    [SerializeField] private Sprite button_es_resume_pointed;
    [SerializeField] private Sprite button_es_resume_pressed;

    [SerializeField] private Sprite button_es_revive_idle;
    [SerializeField] private Sprite button_es_revive_pointed;
    [SerializeField] private Sprite button_es_revive_pressed;

    [SerializeField] private Sprite button_es_restart_idle;
    [SerializeField] private Sprite button_es_restart_pointed;
    [SerializeField] private Sprite button_es_restart_pressed;

    #endregion

    #region Portuguese

    [SerializeField] private Sprite button_pt_play_idle;
    [SerializeField] private Sprite button_pt_play_pointed;
    [SerializeField] private Sprite button_pt_play_pressed;

    [SerializeField] private Sprite button_pt_upgrades_idle;
    [SerializeField] private Sprite button_pt_upgrades_pointed;
    [SerializeField] private Sprite button_pt_upgrades_pressed;

    [SerializeField] private Sprite button_pt_settings_idle;
    [SerializeField] private Sprite button_pt_settings_pointed;
    [SerializeField] private Sprite button_pt_settings_pressed;

    [SerializeField] private Sprite button_pt_quit_idle;
    [SerializeField] private Sprite button_pt_quit_pointed;
    [SerializeField] private Sprite button_pt_quit_pressed;

    [SerializeField] private Sprite button_pt_switch_en_idle;
    [SerializeField] private Sprite button_pt_switch_en_pointed;
    [SerializeField] private Sprite button_pt_switch_en_pressed;

    [SerializeField] private Sprite button_pt_switch_ru_idle;
    [SerializeField] private Sprite button_pt_switch_ru_pointed;
    [SerializeField] private Sprite button_pt_switch_ru_pressed;

    [SerializeField] private Sprite button_pt_switch_es_idle;
    [SerializeField] private Sprite button_pt_switch_es_pointed;
    [SerializeField] private Sprite button_pt_switch_es_pressed;

    [SerializeField] private Sprite button_pt_switch_pt_idle;
    [SerializeField] private Sprite button_pt_switch_pt_pointed;
    [SerializeField] private Sprite button_pt_switch_pt_pressed;

    [SerializeField] private Sprite button_pt_switch_de_idle;
    [SerializeField] private Sprite button_pt_switch_de_pointed;
    [SerializeField] private Sprite button_pt_switch_de_pressed;

    [SerializeField] private Sprite button_pt_switch_id_idle;
    [SerializeField] private Sprite button_pt_switch_id_pointed;
    [SerializeField] private Sprite button_pt_switch_id_pressed;

    [SerializeField] private Sprite button_pt_idle_buy;
    [SerializeField] private Sprite button_pt_idle_improve;
    [SerializeField] private Sprite button_pt_pointed_buy;
    [SerializeField] private Sprite button_pt_pointed_improve;
    [SerializeField] private Sprite button_pt_received;

    [SerializeField] private Sprite button_pt_ok_idle;
    [SerializeField] private Sprite button_pt_ok_pointed;
    [SerializeField] private Sprite button_pt_ok_pressed;

    [SerializeField] private Sprite button_pt_menu_idle;
    [SerializeField] private Sprite button_pt_menu_pointed;
    [SerializeField] private Sprite button_pt_menu_pressed;

    [SerializeField] private Sprite button_pt_resume_idle;
    [SerializeField] private Sprite button_pt_resume_pointed;
    [SerializeField] private Sprite button_pt_resume_pressed;

    [SerializeField] private Sprite button_pt_revive_idle;
    [SerializeField] private Sprite button_pt_revive_pointed;
    [SerializeField] private Sprite button_pt_revive_pressed;

    [SerializeField] private Sprite button_pt_restart_idle;
    [SerializeField] private Sprite button_pt_restart_pointed;
    [SerializeField] private Sprite button_pt_restart_pressed;

    #endregion

    #region German

    [SerializeField] private Sprite button_de_play_idle;
    [SerializeField] private Sprite button_de_play_pointed;
    [SerializeField] private Sprite button_de_play_pressed;

    [SerializeField] private Sprite button_de_upgrades_idle;
    [SerializeField] private Sprite button_de_upgrades_pointed;
    [SerializeField] private Sprite button_de_upgrades_pressed;

    [SerializeField] private Sprite button_de_settings_idle;
    [SerializeField] private Sprite button_de_settings_pointed;
    [SerializeField] private Sprite button_de_settings_pressed;

    [SerializeField] private Sprite button_de_quit_idle;
    [SerializeField] private Sprite button_de_quit_pointed;
    [SerializeField] private Sprite button_de_quit_pressed;

    [SerializeField] private Sprite button_de_switch_en_idle;
    [SerializeField] private Sprite button_de_switch_en_pointed;
    [SerializeField] private Sprite button_de_switch_en_pressed;

    [SerializeField] private Sprite button_de_switch_ru_idle;
    [SerializeField] private Sprite button_de_switch_ru_pointed;
    [SerializeField] private Sprite button_de_switch_ru_pressed;

    [SerializeField] private Sprite button_de_switch_es_idle;
    [SerializeField] private Sprite button_de_switch_es_pointed;
    [SerializeField] private Sprite button_de_switch_es_pressed;

    [SerializeField] private Sprite button_de_switch_pt_idle;
    [SerializeField] private Sprite button_de_switch_pt_pointed;
    [SerializeField] private Sprite button_de_switch_pt_pressed;

    [SerializeField] private Sprite button_de_switch_de_idle;
    [SerializeField] private Sprite button_de_switch_de_pointed;
    [SerializeField] private Sprite button_de_switch_de_pressed;

    [SerializeField] private Sprite button_de_switch_id_idle;
    [SerializeField] private Sprite button_de_switch_id_pointed;
    [SerializeField] private Sprite button_de_switch_id_pressed;

    [SerializeField] private Sprite button_de_idle_buy;
    [SerializeField] private Sprite button_de_idle_improve;
    [SerializeField] private Sprite button_de_pointed_buy;
    [SerializeField] private Sprite button_de_pointed_improve;
    [SerializeField] private Sprite button_de_received;

    [SerializeField] private Sprite button_de_ok_idle;
    [SerializeField] private Sprite button_de_ok_pointed;
    [SerializeField] private Sprite button_de_ok_pressed;

    [SerializeField] private Sprite button_de_menu_idle;
    [SerializeField] private Sprite button_de_menu_pointed;
    [SerializeField] private Sprite button_de_menu_pressed;

    [SerializeField] private Sprite button_de_resume_idle;
    [SerializeField] private Sprite button_de_resume_pointed;
    [SerializeField] private Sprite button_de_resume_pressed;

    [SerializeField] private Sprite button_de_revive_idle;
    [SerializeField] private Sprite button_de_revive_pointed;
    [SerializeField] private Sprite button_de_revive_pressed;

    [SerializeField] private Sprite button_de_restart_idle;
    [SerializeField] private Sprite button_de_restart_pointed;
    [SerializeField] private Sprite button_de_restart_pressed;

    #endregion

    #region Indonesian

    [SerializeField] private Sprite button_id_play_idle;
    [SerializeField] private Sprite button_id_play_pointed;
    [SerializeField] private Sprite button_id_play_pressed;

    [SerializeField] private Sprite button_id_upgrades_idle;
    [SerializeField] private Sprite button_id_upgrades_pointed;
    [SerializeField] private Sprite button_id_upgrades_pressed;

    [SerializeField] private Sprite button_id_settings_idle;
    [SerializeField] private Sprite button_id_settings_pointed;
    [SerializeField] private Sprite button_id_settings_pressed;

    [SerializeField] private Sprite button_id_quit_idle;
    [SerializeField] private Sprite button_id_quit_pointed;
    [SerializeField] private Sprite button_id_quit_pressed;

    [SerializeField] private Sprite button_id_switch_en_idle;
    [SerializeField] private Sprite button_id_switch_en_pointed;
    [SerializeField] private Sprite button_id_switch_en_pressed;

    [SerializeField] private Sprite button_id_switch_ru_idle;
    [SerializeField] private Sprite button_id_switch_ru_pointed;
    [SerializeField] private Sprite button_id_switch_ru_pressed;

    [SerializeField] private Sprite button_id_switch_es_idle;
    [SerializeField] private Sprite button_id_switch_es_pointed;
    [SerializeField] private Sprite button_id_switch_es_pressed;

    [SerializeField] private Sprite button_id_switch_pt_idle;
    [SerializeField] private Sprite button_id_switch_pt_pointed;
    [SerializeField] private Sprite button_id_switch_pt_pressed;

    [SerializeField] private Sprite button_id_switch_de_idle;
    [SerializeField] private Sprite button_id_switch_de_pointed;
    [SerializeField] private Sprite button_id_switch_de_pressed;

    [SerializeField] private Sprite button_id_switch_id_idle;
    [SerializeField] private Sprite button_id_switch_id_pointed;
    [SerializeField] private Sprite button_id_switch_id_pressed;

    [SerializeField] private Sprite button_id_idle_buy;
    [SerializeField] private Sprite button_id_idle_improve;
    [SerializeField] private Sprite button_id_pointed_buy;
    [SerializeField] private Sprite button_id_pointed_improve;
    [SerializeField] private Sprite button_id_received;

    [SerializeField] private Sprite button_id_ok_idle;
    [SerializeField] private Sprite button_id_ok_pointed;
    [SerializeField] private Sprite button_id_ok_pressed;

    [SerializeField] private Sprite button_id_menu_idle;
    [SerializeField] private Sprite button_id_menu_pointed;
    [SerializeField] private Sprite button_id_menu_pressed;

    [SerializeField] private Sprite button_id_resume_idle;
    [SerializeField] private Sprite button_id_resume_pointed;
    [SerializeField] private Sprite button_id_resume_pressed;

    [SerializeField] private Sprite button_id_revive_idle;
    [SerializeField] private Sprite button_id_revive_pointed;
    [SerializeField] private Sprite button_id_revive_pressed;

    [SerializeField] private Sprite button_id_restart_idle;
    [SerializeField] private Sprite button_id_restart_pointed;
    [SerializeField] private Sprite button_id_restart_pressed;

    #endregion

    public Sprite[] Buttons_GetSprites(ButtonName _buttonName, int _numberOfSprites)
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

                    case ButtonName.switch_es:

                        _spriteArray[0] = button_en_switch_es_idle;
                        _spriteArray[1] = button_en_switch_es_pointed;
                        _spriteArray[2] = button_en_switch_es_pressed;

                    break;

                    case ButtonName.switch_pt:

                        _spriteArray[0] = button_en_switch_pt_idle;
                        _spriteArray[1] = button_en_switch_pt_pointed;
                        _spriteArray[2] = button_en_switch_pt_pressed;

                    break;

                    case ButtonName.switch_de:

                        _spriteArray[0] = button_en_switch_de_idle;
                        _spriteArray[1] = button_en_switch_de_pointed;
                        _spriteArray[2] = button_en_switch_de_pressed;

                    break;

                    case ButtonName.switch_id:

                        _spriteArray[0] = button_en_switch_id_idle;
                        _spriteArray[1] = button_en_switch_id_pointed;
                        _spriteArray[2] = button_en_switch_id_pressed;

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

                    case ButtonName.switch_es:

                        _spriteArray[0] = button_ru_switch_es_idle;
                        _spriteArray[1] = button_ru_switch_es_pointed;
                        _spriteArray[2] = button_ru_switch_es_pressed;

                    break;

                    case ButtonName.switch_pt:

                        _spriteArray[0] = button_ru_switch_pt_idle;
                        _spriteArray[1] = button_ru_switch_pt_pointed;
                        _spriteArray[2] = button_ru_switch_pt_pressed;

                    break;

                    case ButtonName.switch_de:

                        _spriteArray[0] = button_ru_switch_de_idle;
                        _spriteArray[1] = button_ru_switch_de_pointed;
                        _spriteArray[2] = button_ru_switch_de_pressed;

                    break;

                    case ButtonName.switch_id:

                        _spriteArray[0] = button_ru_switch_id_idle;
                        _spriteArray[1] = button_ru_switch_id_pointed;
                        _spriteArray[2] = button_ru_switch_id_pressed;

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
            case GameLanguage.spanish:

                switch (_buttonName)
                {
                    case ButtonName.play:

                        _spriteArray[0] = button_es_play_idle;
                        _spriteArray[1] = button_es_play_pointed;
                        _spriteArray[2] = button_es_play_pressed;

                    break;

                    case ButtonName.upgrades:

                        _spriteArray[0] = button_es_upgrades_idle;
                        _spriteArray[1] = button_es_upgrades_pointed;
                        _spriteArray[2] = button_es_upgrades_pressed;

                    break;

                    case ButtonName.settings:

                        _spriteArray[0] = button_es_settings_idle;
                        _spriteArray[1] = button_es_settings_pointed;
                        _spriteArray[2] = button_es_settings_pressed;

                    break;

                    case ButtonName.quit:

                        _spriteArray[0] = button_es_quit_idle;
                        _spriteArray[1] = button_es_quit_pointed;
                        _spriteArray[2] = button_es_quit_pressed;

                    break;

                    case ButtonName.switch_en:

                        _spriteArray[0] = button_es_switch_en_idle;
                        _spriteArray[1] = button_es_switch_en_pointed;
                        _spriteArray[2] = button_es_switch_en_pressed;

                    break;

                    case ButtonName.switch_ru:

                        _spriteArray[0] = button_es_switch_ru_idle;
                        _spriteArray[1] = button_es_switch_ru_pointed;
                        _spriteArray[2] = button_es_switch_ru_pressed;

                    break;

                    case ButtonName.switch_es:

                        _spriteArray[0] = button_es_switch_es_idle;
                        _spriteArray[1] = button_es_switch_es_pointed;
                        _spriteArray[2] = button_es_switch_es_pressed;

                    break;

                    case ButtonName.switch_pt:

                        _spriteArray[0] = button_es_switch_pt_idle;
                        _spriteArray[1] = button_es_switch_pt_pointed;
                        _spriteArray[2] = button_es_switch_pt_pressed;

                    break;

                    case ButtonName.switch_de:

                        _spriteArray[0] = button_es_switch_de_idle;
                        _spriteArray[1] = button_es_switch_de_pointed;
                        _spriteArray[2] = button_es_switch_de_pressed;

                    break;

                    case ButtonName.switch_id:

                        _spriteArray[0] = button_es_switch_id_idle;
                        _spriteArray[1] = button_es_switch_id_pointed;
                        _spriteArray[2] = button_es_switch_id_pressed;

                    break;

                    case ButtonName.upgrade:

                        _spriteArray[0] = button_es_idle_buy;
                        _spriteArray[1] = button_es_idle_improve;
                        _spriteArray[2] = button_es_pointed_buy;
                        _spriteArray[3] = button_es_pointed_improve;
                        _spriteArray[4] = button_es_received;

                    break;

                    case ButtonName.ok:

                        _spriteArray[0] = button_es_ok_idle;
                        _spriteArray[1] = button_es_ok_pointed;
                        _spriteArray[2] = button_es_ok_pressed;

                    break;

                    case ButtonName.menu:

                        _spriteArray[0] = button_es_menu_idle;
                        _spriteArray[1] = button_es_menu_pointed;
                        _spriteArray[2] = button_es_menu_pressed;

                    break;

                    case ButtonName.resume:

                        _spriteArray[0] = button_es_resume_idle;
                        _spriteArray[1] = button_es_resume_pointed;
                        _spriteArray[2] = button_es_resume_pressed;

                    break;

                    case ButtonName.revive:

                        _spriteArray[0] = button_es_revive_idle;
                        _spriteArray[1] = button_es_revive_pointed;
                        _spriteArray[2] = button_es_revive_pressed;

                    break;

                    case ButtonName.restart:

                        _spriteArray[0] = button_es_restart_idle;
                        _spriteArray[1] = button_es_restart_pointed;
                        _spriteArray[2] = button_es_restart_pressed;

                    break;
                }

            break;
            case GameLanguage.portuguese:

                switch (_buttonName)
                {
                    case ButtonName.play:

                        _spriteArray[0] = button_pt_play_idle;
                        _spriteArray[1] = button_pt_play_pointed;
                        _spriteArray[2] = button_pt_play_pressed;

                    break;

                    case ButtonName.upgrades:

                        _spriteArray[0] = button_pt_upgrades_idle;
                        _spriteArray[1] = button_pt_upgrades_pointed;
                        _spriteArray[2] = button_pt_upgrades_pressed;

                    break;

                    case ButtonName.settings:

                        _spriteArray[0] = button_pt_settings_idle;
                        _spriteArray[1] = button_pt_settings_pointed;
                        _spriteArray[2] = button_pt_settings_pressed;

                    break;

                    case ButtonName.quit:

                        _spriteArray[0] = button_pt_quit_idle;
                        _spriteArray[1] = button_pt_quit_pointed;
                        _spriteArray[2] = button_pt_quit_pressed;

                    break;

                    case ButtonName.switch_en:

                        _spriteArray[0] = button_pt_switch_en_idle;
                        _spriteArray[1] = button_pt_switch_en_pointed;
                        _spriteArray[2] = button_pt_switch_en_pressed;

                    break;

                    case ButtonName.switch_ru:

                        _spriteArray[0] = button_pt_switch_ru_idle;
                        _spriteArray[1] = button_pt_switch_ru_pointed;
                        _spriteArray[2] = button_pt_switch_ru_pressed;

                    break;

                    case ButtonName.switch_es:

                        _spriteArray[0] = button_pt_switch_es_idle;
                        _spriteArray[1] = button_pt_switch_es_pointed;
                        _spriteArray[2] = button_pt_switch_es_pressed;

                    break;

                    case ButtonName.switch_pt:

                        _spriteArray[0] = button_pt_switch_pt_idle;
                        _spriteArray[1] = button_pt_switch_pt_pointed;
                        _spriteArray[2] = button_pt_switch_pt_pressed;

                    break;

                    case ButtonName.switch_de:

                        _spriteArray[0] = button_pt_switch_de_idle;
                        _spriteArray[1] = button_pt_switch_de_pointed;
                        _spriteArray[2] = button_pt_switch_de_pressed;

                    break;

                    case ButtonName.switch_id:

                        _spriteArray[0] = button_pt_switch_id_idle;
                        _spriteArray[1] = button_pt_switch_id_pointed;
                        _spriteArray[2] = button_pt_switch_id_pressed;

                    break;

                    case ButtonName.upgrade:

                        _spriteArray[0] = button_pt_idle_buy;
                        _spriteArray[1] = button_pt_idle_improve;
                        _spriteArray[2] = button_pt_pointed_buy;
                        _spriteArray[3] = button_pt_pointed_improve;
                        _spriteArray[4] = button_pt_received;

                    break;

                    case ButtonName.ok:

                        _spriteArray[0] = button_pt_ok_idle;
                        _spriteArray[1] = button_pt_ok_pointed;
                        _spriteArray[2] = button_pt_ok_pressed;

                    break;

                    case ButtonName.menu:

                        _spriteArray[0] = button_pt_menu_idle;
                        _spriteArray[1] = button_pt_menu_pointed;
                        _spriteArray[2] = button_pt_menu_pressed;

                    break;

                    case ButtonName.resume:

                        _spriteArray[0] = button_pt_resume_idle;
                        _spriteArray[1] = button_pt_resume_pointed;
                        _spriteArray[2] = button_pt_resume_pressed;

                    break;

                    case ButtonName.revive:

                        _spriteArray[0] = button_pt_revive_idle;
                        _spriteArray[1] = button_pt_revive_pointed;
                        _spriteArray[2] = button_pt_revive_pressed;

                    break;

                    case ButtonName.restart:

                        _spriteArray[0] = button_pt_restart_idle;
                        _spriteArray[1] = button_pt_restart_pointed;
                        _spriteArray[2] = button_pt_restart_pressed;

                    break;
                }

            break;
            case GameLanguage.german:

                switch (_buttonName)
                {
                    case ButtonName.play:

                        _spriteArray[0] = button_de_play_idle;
                        _spriteArray[1] = button_de_play_pointed;
                        _spriteArray[2] = button_de_play_pressed;

                    break;

                    case ButtonName.upgrades:

                        _spriteArray[0] = button_de_upgrades_idle;
                        _spriteArray[1] = button_de_upgrades_pointed;
                        _spriteArray[2] = button_de_upgrades_pressed;

                    break;

                    case ButtonName.settings:

                        _spriteArray[0] = button_de_settings_idle;
                        _spriteArray[1] = button_de_settings_pointed;
                        _spriteArray[2] = button_de_settings_pressed;

                    break;

                    case ButtonName.quit:

                        _spriteArray[0] = button_de_quit_idle;
                        _spriteArray[1] = button_de_quit_pointed;
                        _spriteArray[2] = button_de_quit_pressed;

                    break;

                    case ButtonName.switch_en:

                        _spriteArray[0] = button_de_switch_en_idle;
                        _spriteArray[1] = button_de_switch_en_pointed;
                        _spriteArray[2] = button_de_switch_en_pressed;

                    break;

                    case ButtonName.switch_ru:

                        _spriteArray[0] = button_de_switch_ru_idle;
                        _spriteArray[1] = button_de_switch_ru_pointed;
                        _spriteArray[2] = button_de_switch_ru_pressed;

                    break;

                    case ButtonName.switch_es:

                        _spriteArray[0] = button_de_switch_es_idle;
                        _spriteArray[1] = button_de_switch_es_pointed;
                        _spriteArray[2] = button_de_switch_es_pressed;

                    break;

                    case ButtonName.switch_pt:

                        _spriteArray[0] = button_de_switch_pt_idle;
                        _spriteArray[1] = button_de_switch_pt_pointed;
                        _spriteArray[2] = button_de_switch_pt_pressed;

                    break;

                    case ButtonName.switch_de:

                        _spriteArray[0] = button_de_switch_de_idle;
                        _spriteArray[1] = button_de_switch_de_pointed;
                        _spriteArray[2] = button_de_switch_de_pressed;

                    break;

                    case ButtonName.switch_id:

                        _spriteArray[0] = button_en_switch_id_idle;
                        _spriteArray[1] = button_en_switch_id_pointed;
                        _spriteArray[2] = button_en_switch_id_pressed;

                    break;

                    case ButtonName.upgrade:

                        _spriteArray[0] = button_de_idle_buy;
                        _spriteArray[1] = button_de_idle_improve;
                        _spriteArray[2] = button_de_pointed_buy;
                        _spriteArray[3] = button_de_pointed_improve;
                        _spriteArray[4] = button_de_received;

                    break;

                    case ButtonName.ok:

                        _spriteArray[0] = button_de_ok_idle;
                        _spriteArray[1] = button_de_ok_pointed;
                        _spriteArray[2] = button_de_ok_pressed;

                    break;

                    case ButtonName.menu:

                        _spriteArray[0] = button_de_menu_idle;
                        _spriteArray[1] = button_de_menu_pointed;
                        _spriteArray[2] = button_de_menu_pressed;

                    break;

                    case ButtonName.resume:

                        _spriteArray[0] = button_de_resume_idle;
                        _spriteArray[1] = button_de_resume_pointed;
                        _spriteArray[2] = button_de_resume_pressed;

                    break;

                    case ButtonName.revive:

                        _spriteArray[0] = button_de_revive_idle;
                        _spriteArray[1] = button_de_revive_pointed;
                        _spriteArray[2] = button_de_revive_pressed;

                    break;

                    case ButtonName.restart:

                        _spriteArray[0] = button_de_restart_idle;
                        _spriteArray[1] = button_de_restart_pointed;
                        _spriteArray[2] = button_de_restart_pressed;

                    break;
                }

            break;
            case GameLanguage.indonesian:

                switch (_buttonName)
                {
                    case ButtonName.play:

                        _spriteArray[0] = button_id_play_idle;
                        _spriteArray[1] = button_id_play_pointed;
                        _spriteArray[2] = button_id_play_pressed;

                    break;

                    case ButtonName.upgrades:

                        _spriteArray[0] = button_id_upgrades_idle;
                        _spriteArray[1] = button_id_upgrades_pointed;
                        _spriteArray[2] = button_id_upgrades_pressed;

                    break;

                    case ButtonName.settings:

                        _spriteArray[0] = button_id_settings_idle;
                        _spriteArray[1] = button_id_settings_pointed;
                        _spriteArray[2] = button_id_settings_pressed;

                    break;

                    case ButtonName.quit:

                        _spriteArray[0] = button_id_quit_idle;
                        _spriteArray[1] = button_id_quit_pointed;
                        _spriteArray[2] = button_id_quit_pressed;

                    break;

                    case ButtonName.switch_en:

                        _spriteArray[0] = button_id_switch_en_idle;
                        _spriteArray[1] = button_id_switch_en_pointed;
                        _spriteArray[2] = button_id_switch_en_pressed;

                    break;

                    case ButtonName.switch_ru:

                        _spriteArray[0] = button_id_switch_ru_idle;
                        _spriteArray[1] = button_id_switch_ru_pointed;
                        _spriteArray[2] = button_id_switch_ru_pressed;

                    break;

                    case ButtonName.switch_es:

                        _spriteArray[0] = button_id_switch_es_idle;
                        _spriteArray[1] = button_id_switch_es_pointed;
                        _spriteArray[2] = button_id_switch_es_pressed;

                    break;

                    case ButtonName.switch_pt:

                        _spriteArray[0] = button_id_switch_pt_idle;
                        _spriteArray[1] = button_id_switch_pt_pointed;
                        _spriteArray[2] = button_id_switch_pt_pressed;

                    break;

                    case ButtonName.switch_de:

                        _spriteArray[0] = button_id_switch_de_idle;
                        _spriteArray[1] = button_id_switch_de_pointed;
                        _spriteArray[2] = button_id_switch_de_pressed;

                    break;

                    case ButtonName.switch_id:

                        _spriteArray[0] = button_id_switch_id_idle;
                        _spriteArray[1] = button_id_switch_id_pointed;
                        _spriteArray[2] = button_id_switch_id_pressed;

                    break;

                    case ButtonName.upgrade:

                        _spriteArray[0] = button_id_idle_buy;
                        _spriteArray[1] = button_id_idle_improve;
                        _spriteArray[2] = button_id_pointed_buy;
                        _spriteArray[3] = button_id_pointed_improve;
                        _spriteArray[4] = button_id_received;

                    break;

                    case ButtonName.ok:

                        _spriteArray[0] = button_id_ok_idle;
                        _spriteArray[1] = button_id_ok_pointed;
                        _spriteArray[2] = button_id_ok_pressed;

                    break;

                    case ButtonName.menu:

                        _spriteArray[0] = button_id_menu_idle;
                        _spriteArray[1] = button_id_menu_pointed;
                        _spriteArray[2] = button_id_menu_pressed;

                    break;

                    case ButtonName.resume:

                        _spriteArray[0] = button_id_resume_idle;
                        _spriteArray[1] = button_id_resume_pointed;
                        _spriteArray[2] = button_id_resume_pressed;

                    break;

                    case ButtonName.revive:

                        _spriteArray[0] = button_id_revive_idle;
                        _spriteArray[1] = button_id_revive_pointed;
                        _spriteArray[2] = button_id_revive_pressed;

                    break;

                    case ButtonName.restart:

                        _spriteArray[0] = button_id_restart_idle;
                        _spriteArray[1] = button_id_restart_pointed;
                        _spriteArray[2] = button_id_restart_pressed;

                    break;
                }

            break;
        }        

        return _spriteArray;
    }

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
        tutorial,
        indicators_complete,
        midscreen_gameOver,
        midscreen_pause,
        midscreen_distanceRemain,
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
        radio_string_1,
        radio_string_2,
        radio_string_3,
        radio_string_4,
        radio_string_5,
        radio_string_6,
        radio_string_7,
        radio_string_8,
        radio_string_9,
        radio_string_10,
        radio_string_11,
        radio_string_12,
        radio_string_13,
        radio_string_14
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
        [Text_Key.tutorial] = "Use the virtual stick to select direction.",
        [Text_Key.indicators_complete] = "KILOMETERS LEFT",
        [Text_Key.midscreen_gameOver] = "GAME OVER",
        [Text_Key.midscreen_pause] = "PAUSE",
        [Text_Key.midscreen_distanceRemain] = "Distance remain",
        [Text_Key.popUp_up] = "+1 UP",
        [Text_Key.popUp_coin] = "+1 Coin",
        [Text_Key.popUp_coinRush] = "Coin Rush!",
        [Text_Key.gameBy] = "A GAME BY LUNAR HOWL",
        [Text_Key.statistics_reviveNumber] = "Revive number",
        [Text_Key.statistics_coinsTotal] = "Coins total",
        [Text_Key.statistics_coinsSpentOnRevivals] = "Сoins spent on revivals",
        [Text_Key.statistics_defeats] = "Defeats",
        [Text_Key.statistics_totalDrivings] = "Total Drivings",
        [Text_Key.statistics_best] = "Best",
        [Text_Key.statistics_newRecord] = "New record!",
        [Text_Key.statistics_gameCompleted] = "Game completed",
        [Text_Key.dialogue_start_string_1] = "Hello.",
        [Text_Key.dialogue_start_string_2] = "Hey, handsome.",
        [Text_Key.dialogue_start_string_3] = "I got an extra box of toothpicks on sale. You can just...",
        [Text_Key.dialogue_start_string_4] = "Come and take it...",
        [Text_Key.dialogue_start_string_5] = "Interested?",
        [Text_Key.dialogue_start_string_6] = "Already driving.",
        [Text_Key.dialogue_end_string_1] = "Where are you?",
        [Text_Key.dialogue_end_string_2] = "I'm already burning with impatience!",
        [Text_Key.dialogue_end_string_3] = "Almost there.",
        [Text_Key.dialogue_end_string_4] = "Oh, crap!",
        [Text_Key.dialogue_end_string_5] = "Are you okay?",
        [Text_Key.dialogue_end_string_6] = "…",
        [Text_Key.dialogue_end_string_7] = "Can you hear me?",
        [Text_Key.radio_string_1] = "[interference] «It's exactly midnight on the clock. You're tuned to the Free Road Radio...»",
        [Text_Key.radio_string_2] = "[noise] «Don't lose speed»",
        [Text_Key.radio_string_3] = "[interference] «Another overtaking. Another chick»",
        [Text_Key.radio_string_4] = "[crackle] «Night — time to accelerate»",
        [Text_Key.radio_string_5] = "[noise] «The faster — the better»",
        [Text_Key.radio_string_6] = "[interference] «And the heart beats faster»",
        [Text_Key.radio_string_7] = "[crackle] «Once you rev up, you can't stop»",
        [Text_Key.radio_string_8] = "[bzz] «You're still alive!»",
        [Text_Key.radio_string_9] = "[screech] «You have a habit of playing with death»",
        [Text_Key.radio_string_10] = "[ear ringing] «She'll quickly find a replacement»",
        [Text_Key.radio_string_11] = "[bzz] «Live fast, die fast»",
        [Text_Key.radio_string_12] = "[screech] «What are brakes?»",
        [Text_Key.radio_string_13] = "[ear ringing] «Adrenaline — the best fuel»",
        [Text_Key.radio_string_14] = "[sound explosion] «Speed will set you free»"
    };

    private Dictionary<Text_Key, string> text_dictionary_ru = new Dictionary<Text_Key, string>()
    {
        [Text_Key.startText] = "НАЖМИТЕ ЛЮБУЮ КЛАВИШУ",
        [Text_Key.loadingCloudData] = "ЗАГРУЗКА ДАННЫХ",
        [Text_Key.upgrade_moreCoins] = "БОЛЬШЕ МОНЕТ",
        [Text_Key.upgrade_moreBonuses] = "БОЛЬШЕ БОНУСОВ",
        [Text_Key.upgrade_coinMagnet] = "МАГНИТ ДЛЯ МОНЕТ",
        [Text_Key.upgrade_heDidNotDie] = "ДА НЕ УМЕР ОН",
        [Text_Key.popUpMessage_notEnoughCoins] = "НЕДОСТАТОЧНО МОНЕТ",
        [Text_Key.tutorial] = "Используй виртуальный стик для выбора направления.",
        [Text_Key.indicators_complete] = "КМ ДО ЦЕЛИ",
        [Text_Key.midscreen_gameOver] = "КОНЕЦ ИГРЫ",
        [Text_Key.midscreen_pause] = "ПАУЗА",
        [Text_Key.midscreen_distanceRemain] = "Осталось до цели",
        [Text_Key.popUp_up] = "+1 Жизнь",
        [Text_Key.popUp_coin] = "+1 Монета",
        [Text_Key.popUp_coinRush] = "Монетная Лихорадка!",
        [Text_Key.gameBy] = "ИГРА LUNAR HOWL",
        [Text_Key.statistics_reviveNumber] = "Количество возрождений",
        [Text_Key.statistics_coinsTotal] = "Всего монет",
        [Text_Key.statistics_coinsSpentOnRevivals] = "Потрачено монет на возрождения",
        [Text_Key.statistics_defeats] = "Поражения",
        [Text_Key.statistics_totalDrivings] = "Всего Поездок",
        [Text_Key.statistics_best] = "Лучший",
        [Text_Key.statistics_newRecord] = "Новый рекорд!",
        [Text_Key.statistics_gameCompleted] = "Игра пройдена",
        [Text_Key.dialogue_start_string_1] = "Ало.",
        [Text_Key.dialogue_start_string_2] = "Привет красавчик.",
        [Text_Key.dialogue_start_string_3] = "Я тут получила лишнюю коробку зубочисток по акции. Можешь просто...",
        [Text_Key.dialogue_start_string_4] = "Прийти и взять её...",
        [Text_Key.dialogue_start_string_5] = "Интересует?",
        [Text_Key.dialogue_start_string_6] = "Уже еду.",
        [Text_Key.dialogue_end_string_1] = "Ты где?",
        [Text_Key.dialogue_end_string_2] = "Я уже сгораю от нетерпения!",
        [Text_Key.dialogue_end_string_3] = "Почти на месте.",
        [Text_Key.dialogue_end_string_4] = "О, черт!",
        [Text_Key.dialogue_end_string_5] = "Все в порядке?",
        [Text_Key.dialogue_end_string_6] = "…",
        [Text_Key.dialogue_end_string_7] = "Ты меня слышишь?",
        [Text_Key.radio_string_1] = "[помехи] «На часах ровно полночь. В эфире Радио Свободной Дороги...»",
        [Text_Key.radio_string_2] = "[шум] «Не теряй скорость»",
        [Text_Key.radio_string_3] = "[помехи] «Ещё один обгон. Еще одна подружка»",
        [Text_Key.radio_string_4] = "[треск] «Ночь — время ускорить темп»",
        [Text_Key.radio_string_5] = "[шум] «Чем быстрее — тем лучше»",
        [Text_Key.radio_string_6] = "[помехи] «И сердце бьётся чаще»",
        [Text_Key.radio_string_7] = "[треск] «Стоит разогнать и уже не остановить»",
        [Text_Key.radio_string_8] = "[бзз] «Ты всё еще жив!»",
        [Text_Key.radio_string_9] = "[скрежет] «У тебя привычка играть со смертью»",
        [Text_Key.radio_string_10] = "[звон в ушах] «Она быстро найдет замену»",
        [Text_Key.radio_string_11] = "[бзз] «Живи быстро, умри быстро»",
        [Text_Key.radio_string_12] = "[скрежет] «Что такое тормоза?»",
        [Text_Key.radio_string_13] = "[звон в ушах] «Адреналин — лучшее топливо»",
        [Text_Key.radio_string_14] = "[разрыв звука] «Скорость сделает тебя свободным»"
    };

    private Dictionary<Text_Key, string> text_dictionary_es = new Dictionary<Text_Key, string>()
    {
        [Text_Key.startText] = "PRESIONE CUALQUIER TECLA",
        [Text_Key.loadingCloudData] = "CARGANDO DATOS DE LA NUBE",
        [Text_Key.upgrade_moreCoins] = "MÁS MONEDAS",
        [Text_Key.upgrade_moreBonuses] = "MÁS BONOS",
        [Text_Key.upgrade_coinMagnet] = "IMÁN DE MONEDAS",
        [Text_Key.upgrade_heDidNotDie] = "ÉL NO MURIÓ",
        [Text_Key.popUpMessage_notEnoughCoins] = "NO HAY SUFICIENTES MONEDAS",
        [Text_Key.tutorial] = "Usa la palanca virtual para seleccionar la dirección.",
        [Text_Key.indicators_complete] = "KILÓMETROS RESTANTES",
        [Text_Key.midscreen_gameOver] = "JUEGO TERMINADO",
        [Text_Key.midscreen_pause] = "PAUSA",
        [Text_Key.midscreen_distanceRemain] = "La distancia permanece",
        [Text_Key.popUp_up] = "+1 VIDA",
        [Text_Key.popUp_coin] = "+ 1 Moneda",
        [Text_Key.popUp_coinRush] = "Fiebre de monedas!",
        [Text_Key.gameBy] = "Un juego de LUNAR HOWL",
        [Text_Key.statistics_reviveNumber] = "Número de revivires",
        [Text_Key.statistics_coinsTotal] = "Total de monedas",
        [Text_Key.statistics_coinsSpentOnRevivals] = "Monedas gastadas en revivires",
        [Text_Key.statistics_defeats] = "Derrotas",
        [Text_Key.statistics_totalDrivings] = "Viajes totales",
        [Text_Key.statistics_best] = "Mejor",
        [Text_Key.statistics_newRecord] = "¡Nuevo récord!",
        [Text_Key.statistics_gameCompleted] = "Juego completado",
        [Text_Key.dialogue_start_string_1] = "Hola.",
        [Text_Key.dialogue_start_string_2] = "Hola, guapo.",
        [Text_Key.dialogue_start_string_3] = "Tengo una caja extra de retales que compré en una venta de garaje. Puedes...",
        [Text_Key.dialogue_start_string_4] = "Ven y cógela...",
        [Text_Key.dialogue_start_string_5] = "¿Interesa?",
        [Text_Key.dialogue_start_string_6] = "Ya voy en camino.",
        [Text_Key.dialogue_end_string_1] = "¿Dónde estás?",
        [Text_Key.dialogue_end_string_2] = "¡Ya me estoy muriendo de impaciencia!",
        [Text_Key.dialogue_end_string_3] = "Casi llego.",
        [Text_Key.dialogue_end_string_4] = "¡Oh, mierda!",
        [Text_Key.dialogue_end_string_5] = "¿Estás bien?",
        [Text_Key.dialogue_end_string_6] = "…",
        [Text_Key.dialogue_end_string_7] = "¿Me oyes?",
        [Text_Key.radio_string_1] = "[interferencia] «Son exactamente medianoche en el reloj. Estás en la Radio de la Carretera Libre...»",
        [Text_Key.radio_string_2] = "[ruido] «No pierdas velocidad»",
        [Text_Key.radio_string_3] = "[interferencia] «Otra adelantada. Otra chica»",
        [Text_Key.radio_string_4] = "[crackle] «Noche — tiempo de acelerar»",
        [Text_Key.radio_string_5] = "[ruido] «Cuanto más rápido — mejor»",
        [Text_Key.radio_string_6] = "[interferencia] «Y el corazón late más rápido»",
        [Text_Key.radio_string_7] = "[crackle] «Una vez que aceleras, ya no puedes detenerte»",
        [Text_Key.radio_string_8] = "[bzz] «¡Todavía estás vivo!»",
        [Text_Key.radio_string_9] = "[chirrido] «Tienes la costumbre de jugar con la muerte»",
        [Text_Key.radio_string_10] = "[zumbido en el oído] «Ella encontrará un reemplazo rápidamente»",
        [Text_Key.radio_string_11] = "[bzz] «Vive rápido, muere joven»",
        [Text_Key.radio_string_12] = "[chirrido] «¿Qué son los frenos?»",
        [Text_Key.radio_string_13] = "[zumbido en el oído] «La adrenalina — el mejor combustible»",
        [Text_Key.radio_string_14] = "[explosión de sonido] «La velocidad te hará libre»"
    };

    private Dictionary<Text_Key, string> text_dictionary_pt = new Dictionary<Text_Key, string>()
    {
        [Text_Key.startText] = "PRESSIONE QUALQUER TECLA",
        [Text_Key.loadingCloudData] = "CARREGANDO DADOS DA NUVEM",
        [Text_Key.upgrade_moreCoins] = "MAIS MOEDAS",
        [Text_Key.upgrade_moreBonuses] = "MAIS BÔNUS",
        [Text_Key.upgrade_coinMagnet] = "ÍMÃ DE MOEDAS",
        [Text_Key.upgrade_heDidNotDie] = "ELE NÃO MORREU",
        [Text_Key.popUpMessage_notEnoughCoins] = "MOEDAS NÃO SUFICIENTES",
        [Text_Key.tutorial] = "Use o joystick virtual para selecionar a direção.",
        [Text_Key.indicators_complete] = "QUILÔMETROS RESTANTES",
        [Text_Key.midscreen_gameOver] = "FIM DO JOGO",
        [Text_Key.midscreen_pause] = "PAUSA",
        [Text_Key.midscreen_distanceRemain] = "Distância permanece",
        [Text_Key.popUp_up] = "+1 Vida",
        [Text_Key.popUp_coin] = "+1 Moeda",
        [Text_Key.popUp_coinRush] = "Corrida de moedas!",
        [Text_Key.gameBy] = "Um jogo de LUNAR HOWL",
        [Text_Key.statistics_reviveNumber] = "Número de revives",
        [Text_Key.statistics_coinsTotal] = "Total de moedas",
        [Text_Key.statistics_coinsSpentOnRevivals] = "Moedas gastas em revives",
        [Text_Key.statistics_defeats] = "Derrotas",
        [Text_Key.statistics_totalDrivings] = "Total de Viagens",
        [Text_Key.statistics_best] = "Melhor",
        [Text_Key.statistics_newRecord] = "Novo recorde!",
        [Text_Key.statistics_gameCompleted] = " Jogo concluído",
        [Text_Key.dialogue_start_string_1] = "Hola.",
        [Text_Key.dialogue_start_string_2] = "Oi, lindo.",
        [Text_Key.dialogue_start_string_3] = "Tenho uma caixa extra de retalhos que comprei em um bazar. Você pode...",
        [Text_Key.dialogue_start_string_4] = "Vir pegar...",
        [Text_Key.dialogue_start_string_5] = "Intresute?",
        [Text_Key.dialogue_start_string_6] = "Já estou indo.",
        [Text_Key.dialogue_end_string_1] = "Onde você está?",
        [Text_Key.dialogue_end_string_2] = "Já estou morrendo de ansiedade!",
        [Text_Key.dialogue_end_string_3] = "Quase lá.",
        [Text_Key.dialogue_end_string_4] = "Ah, droga!",
        [Text_Key.dialogue_end_string_5] = "Você está bem?",
        [Text_Key.dialogue_end_string_6] = "…",
        [Text_Key.dialogue_end_string_7] = "Você consegue me ouvir?",
        [Text_Key.radio_string_1] = "[interferência] «São exatamente meia-noite no relógio. Você está na Rádio da Estrada Livre...»",
        [Text_Key.radio_string_2] = "[ruído] «Não perca velocidade»",
        [Text_Key.radio_string_3] = "[interferência] «Mais uma ultrapassagem. Mais uma garota»",
        [Text_Key.radio_string_4] = "[crackle] «Noite — hora de acelerar»",
        [Text_Key.radio_string_5] = "[ruído] «Quanto mais rápido — melhor»",
        [Text_Key.radio_string_6] = "[interferência] «E o coração bate mais rápido»",
        [Text_Key.radio_string_7] = "[crackle] «Uma vez acelerando»",
        [Text_Key.radio_string_8] = "[bzz] «Ainda estás vivo!»",
        [Text_Key.radio_string_9] = "[moagem] «Tens o hábito de namoriscar com a morte»",
        [Text_Key.radio_string_10] = "[zumbido nos ouvidos] «Ela encontrará um substituto rapidamente»",
        [Text_Key.radio_string_11] = "[bzz] «Vive rápido, morre rápido»",
        [Text_Key.radio_string_12] = "[moagem] «O que são freios?»",
        [Text_Key.radio_string_13] = "[zumbido nos ouvidos] «A adrenalina é o melhor combustível»",
        [Text_Key.radio_string_14] = "[explosão sonora] «A velocidade libertá-lo-á»"
    };

    private Dictionary<Text_Key, string> text_dictionary_de = new Dictionary<Text_Key, string>()
    {
        [Text_Key.startText] = "DRÜCKEN SIE EINE BELIEBIGE TASTE",
        [Text_Key.loadingCloudData] = "CLOUD-DATEN LADEN",
        [Text_Key.upgrade_moreCoins] = "MEHR MÜNZEN",
        [Text_Key.upgrade_moreBonuses] = "MEHR BONI",
        [Text_Key.upgrade_coinMagnet] = "MÜNZMAGNET",
        [Text_Key.upgrade_heDidNotDie] = "ER IST NICHT GESTORBEN",
        [Text_Key.popUpMessage_notEnoughCoins] = "ES FEHLEN MÜNZEN",
        [Text_Key.tutorial] = "Sie den virtuellen Stick, um die Richtung auszuwählen.",
        [Text_Key.indicators_complete] = "KM ZUM ZIEL",
        [Text_Key.midscreen_gameOver] = "SPIEL VORBEI",
        [Text_Key.midscreen_pause] = "PAUSE",
        [Text_Key.midscreen_distanceRemain] = "Verbleibender Abstand",
        [Text_Key.popUp_up] = "+1 Leben",
        [Text_Key.popUp_coin] = "+ 1 Münze",
        [Text_Key.popUp_coinRush] = "Münzrausch!",
        [Text_Key.gameBy] = "Ein Spiel von LUNAR HOWL",
        [Text_Key.statistics_reviveNumber] = "Anzahl der Wiederbelebungen",
        [Text_Key.statistics_coinsTotal] = "Gesamtmünzen",
        [Text_Key.statistics_coinsSpentOnRevivals] = "Münzen zur Wiederbelebung",
        [Text_Key.statistics_defeats] = "Niederlagen",
        [Text_Key.statistics_totalDrivings] = "Gesamtfahrten",
        [Text_Key.statistics_best] = "Bestes",
        [Text_Key.statistics_newRecord] = "Neuer Rekord!",
        [Text_Key.statistics_gameCompleted] = "Spiel abgeschlossen",
        [Text_Key.dialogue_start_string_1] = "Hallo.",
        [Text_Key.dialogue_start_string_2] = "Hallo, hübscher.",
        [Text_Key.dialogue_start_string_3] = "Ich habe noch eine Kiste mit Resten, die ich auf einem Flohmarkt gekauft habe. Du kannst ...",
        [Text_Key.dialogue_start_string_4] = "Komm und hol sie...",
        [Text_Key.dialogue_start_string_5] = "Interessiert?",
        [Text_Key.dialogue_start_string_6] = "Bin schon unterwegs.",
        [Text_Key.dialogue_end_string_1] = "Wo bist du?",
        [Text_Key.dialogue_end_string_2] = "Ich platze vor Ungeduld!",
        [Text_Key.dialogue_end_string_3] = "Fast da.",
        [Text_Key.dialogue_end_string_4] = "Oh, Mist!",
        [Text_Key.dialogue_end_string_5] = "Bist du okay?",
        [Text_Key.dialogue_end_string_6] = "…",
        [Text_Key.dialogue_end_string_7] = "Hörst du mich?",
        [Text_Key.radio_string_1] = "[störungs] «Es ist genau Mitternacht. Hier läuft das Radio der Freien Straße...»",
        [Text_Key.radio_string_2] = "[lärm] «Verliere nicht an Geschwindigkeit»",
        [Text_Key.radio_string_3] = "[störungs] «Noch ein Überholmanöver. Noch ein Mädchen»",
        [Text_Key.radio_string_4] = "[knistern] «Nacht — Zeit, das Tempo zu erhöhen»",
        [Text_Key.radio_string_5] = "[lärm] «Je schneller, desto besser»",
        [Text_Key.radio_string_6] = "[Störung] «Und das Herz schlägt schneller»",
        [Text_Key.radio_string_7] = "[knistern] «Einmal Gas geben, und man kann nicht mehr aufhören»",
        [Text_Key.radio_string_8] = "[bzz] «Du lebst noch!»",
        [Text_Key.radio_string_9] = "[Kreischen] «Du hast die Angewohnheit, mit dem Tod zu spielen»",
        [Text_Key.radio_string_10] = "[ohrenklingeln] «Sie wird schnell einen Ersatz finden»",
        [Text_Key.radio_string_11] = "[bzz] «Lebe schnell, sterbe jung»",
        [Text_Key.radio_string_12] = "[kreischen] «Was sind Bremsen?»",
        [Text_Key.radio_string_13] = "[ohrenklingeln] «Adrenalin — der beste Treibstoff»",
        [Text_Key.radio_string_14] = "[soundexplosion] «Geschwindigkeit macht dich frei»"
    };

    private Dictionary<Text_Key, string> text_dictionary_id = new Dictionary<Text_Key, string>()
    {
        [Text_Key.startText] = "TEKAN TOMBOL APA SAJA",
        [Text_Key.loadingCloudData] = "MENGUNDUH DATA DARI CLOUD",
        [Text_Key.upgrade_moreCoins] = "LEBIH BANYAK KOIN",
        [Text_Key.upgrade_moreBonuses] = "LEBIH BANYAK BONUS",
        [Text_Key.upgrade_coinMagnet] = "KOIN MAGNET",
        [Text_Key.upgrade_heDidNotDie] = "DIA TIDAK MATI",
        [Text_Key.popUpMessage_notEnoughCoins] = "KOIN TIDAK CUKUP",
        [Text_Key.tutorial] = "Gunakan stik virtual untuk memilih arah.",
        [Text_Key.indicators_complete] = "KILOMETER TERSISA",
        [Text_Key.midscreen_gameOver] = "PERMAINAN BERAKHIR",
        [Text_Key.midscreen_pause] = "BERHENTI SEBENTAR",
        [Text_Key.midscreen_distanceRemain] = "Jarak yang tersisa",
        [Text_Key.popUp_up] = "+ 1 Kehidupan",
        [Text_Key.popUp_coin] = "+1 Koin",
        [Text_Key.popUp_coinRush] = "Demam Koin!",
        [Text_Key.gameBy] = "Sebuah permainan oleh LUNAR HOWL",
        [Text_Key.statistics_reviveNumber] = "Jumlah kelahiran kembali",
        [Text_Key.statistics_coinsTotal] = "Total koin",
        [Text_Key.statistics_coinsSpentOnRevivals] = "Koin untuk kebangkitan",
        [Text_Key.statistics_defeats] = "Kekalahan",
        [Text_Key.statistics_totalDrivings] = "Total Mengemudi",
        [Text_Key.statistics_best] = "Terbaik",
        [Text_Key.statistics_newRecord] = "Rekor baru!",
        [Text_Key.statistics_gameCompleted] = "Permainan selesai",
        [Text_Key.dialogue_start_string_1] = "Halo.",
        [Text_Key.dialogue_start_string_2] = "Hai, tampan.",
        [Text_Key.dialogue_start_string_3] = "Saya punya sekotak sisa barang bekas yang saya beli di obral pekarangan. Anda bisa...",
        [Text_Key.dialogue_start_string_4] = "Datang dan ambil...",
        [Text_Key.dialogue_start_string_5] = "Tertarik?",
        [Text_Key.dialogue_start_string_6] = "Sudah dalam perjalanan.",
        [Text_Key.dialogue_end_string_1] = "Di mana kamu?",
        [Text_Key.dialogue_end_string_2] = "Aku sudah terbakar oleh rasa tidak sabar!",
        [Text_Key.dialogue_end_string_3] = "Hampir sampai.",
        [Text_Key.dialogue_end_string_4] = "Oh, sial!",
        [Text_Key.dialogue_end_string_5] = "Apakah kamu baik-baik saja?",
        [Text_Key.dialogue_end_string_6] = "…",
        [Text_Key.dialogue_end_string_7] = "Kamu bisa mendengarku?",
        [Text_Key.radio_string_1] = "[gangguan] «Ini tepat tengah malam di jam. Kamu sedang mendengarkan Radio Jalan Bebas...»",
        [Text_Key.radio_string_2] = "[suara] «Jangan kehilangan kecepatan»",
        [Text_Key.radio_string_3] = "[gangguan] «Lagi satu menyalip. Lagi satu gadis»",
        [Text_Key.radio_string_4] = "[kretek] «Malam — waktu untuk mempercepat»",
        [Text_Key.radio_string_5] = "[suara] «Semakin cepat — semakin baik»",
        [Text_Key.radio_string_6] = "[gangguan] «Dan jantung berdetak lebih cepat»",
        [Text_Key.radio_string_7] = "[kretek] «Sekali gas, tak bisa berhenti lagi»",
        [Text_Key.radio_string_8] = "[bzz] «Kamu masih hidup!»",
        [Text_Key.radio_string_9] = "[geraman] «Kamu punya kebiasaan bermain dengan kematian»",
        [Text_Key.radio_string_10] = "[berdering di telinga] «Dia akan segera menemukan pengganti»",
        [Text_Key.radio_string_11] = "[bzz] «Hidup cepat, mati muda»",
        [Text_Key.radio_string_12] = "[geraman] «Apa itu rem?»",
        [Text_Key.radio_string_13] = "[berdering di telinga] «Adrenalin — bahan bakar terbaik»",
        [Text_Key.radio_string_14] = "[ledakan suara] «Kecepatan akan membebaskanmu»"
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
            case GameLanguage.spanish:
                _text = text_dictionary_es[_key];
                break;
            case GameLanguage.portuguese:
                _text = text_dictionary_pt[_key];
                break;
            case GameLanguage.german:
                _text = text_dictionary_de[_key];
                break;
            case GameLanguage.indonesian:
                _text = text_dictionary_id[_key];
                break;

        }

        return _text;
    }

    #endregion

    public enum GameLanguage
    {
        english,
        russian,
        spanish,
        portuguese,
        german,
        indonesian
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