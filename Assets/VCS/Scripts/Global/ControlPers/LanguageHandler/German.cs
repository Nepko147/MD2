using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_German : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();
        
        text_keyToString[Text_Key.startText_desktop] = "DRÜCKEN SIE EINE BELIEBIGE TASTE";
        text_keyToString[Text_Key.startText_mobile] = "AUF DEN BILDSCHIRM TIPPEN";
        text_keyToString[Text_Key.loadingCloudData] = "DATEN AUS DER CLOUD LADEN";
        text_keyToString[Text_Key.upgrade_moreCoins] = "MEHR MÜNZEN";
        text_keyToString[Text_Key.upgrade_moreBonuses] = "MEHR BONI";
        text_keyToString[Text_Key.upgrade_coinMagnet] = "MÜNZMAGNET";
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "ER IST NICHT GESTORBEN";
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "ES FEHLEN MÜNZEN";
        text_keyToString[Text_Key.tutorial_desktop] = "Zum Bewegen die linke Maustaste gedrückt halten und dann den virtuellen Stick in Bewegungsrichtung ziehen.";
        text_keyToString[Text_Key.tutorial_mobile] = "Zum Bewegen halten Sie Ihren Finger auf dem Bildschirm und ziehen Sie dann den virtuellen Stick in Bewegungsrichtung.";
        text_keyToString[Text_Key.indicators_complete] = "KM ZUM ZIEL";
        text_keyToString[Text_Key.midscreen_gameOver] = "SPIEL VORBEI";
        text_keyToString[Text_Key.midscreen_pause] = "PAUSE";
        text_keyToString[Text_Key.midscreen_distanceRemain] = "Verbleibender Abstand";
        text_keyToString[Text_Key.received_text] = "Münzen erhalten";
        text_keyToString[Text_Key.received_ad_text] = "Hol dir x" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " für das Ansehen von Werbung"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 Leben";
        text_keyToString[Text_Key.popUp_coin] = "+ 1 Münze";
        text_keyToString[Text_Key.popUp_coinRush] = "Münzrausch!";
        text_keyToString[Text_Key.gameBy] = "Ein Spiel von LUNAR HOWL";
        text_keyToString[Text_Key.statistics_reviveNumber] = "ANZAHL DER WIEDERBELEBUNGEN";
        text_keyToString[Text_Key.statistics_coinsTotal] = "GESAMTMÜNZEN";
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "MÜNZEN ZUR WIEDERBELEBUNG";
        text_keyToString[Text_Key.statistics_defeats] = "NIEDERLAGEN";
        text_keyToString[Text_Key.statistics_totalDrivings] = "GESAMTZAHL DER FAHRTEN";
        text_keyToString[Text_Key.statistics_best] = "BESTES ERGEBNIS";
        text_keyToString[Text_Key.statistics_newRecord] = "Neuer Rekord!";
        text_keyToString[Text_Key.statistics_gameCompleted] = "Spiel abgeschlossen";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Hallo.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Hallo, hübscher.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "Ich habe noch eine Kiste mit Resten, die ich auf einem Flohmarkt gekauft habe. Du kannst ...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Komm und hol sie...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "Interessiert?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Bin schon unterwegs.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Wo bist du?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "Ich platze vor Ungeduld!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Fast da.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "Oh, Mist!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "Bist du okay?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Hörst du mich?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>Radio</color>] «Es ist genau Mitternacht. Hier läuft das Radio der Freien Straße...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>Radio</color>] «Verliere nicht an <color={HEX_MAGENTA}>Geschwindigkeit</color>»";
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>Radio</color>] «Noch ein Überholmanöver. Noch ein <color={HEX_MAGENTA}>Mädchen</color>»";
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>Radio</color>] «Nacht — Zeit, das Tempo zu <color={HEX_MAGENTA}>erhöhen</color>»";
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>Radio</color>] «Je <color={HEX_MAGENTA}>schneller</color>, desto besser»";
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>Radio</color>] «Bring dein <color={HEX_MAGENTA}>Herz</color> wieder in Schwung!»";
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>Radio</color>] «Einmal Gas <color={HEX_MAGENTA}>geben</color>, und man kann nicht mehr <color={HEX_CYAN}>aufhören</color>»";
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>Radio</color>] «Du <color={HEX_CYAN}>lebst</color> noch!»";
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>Radio</color>] «Du hast die Angewohnheit, mit dem <color={HEX_CYAN}>Tod</color> zu spielen»";
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>Radio</color>] «<color={HEX_MAGENTA}>Sie</color> wird schnell einen Ersatz finden»";
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>Radio</color>] «<color={HEX_CYAN}>Lebe</color> schnell, <color={HEX_MAGENTA}>sterbe</color> jung»";
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>Radio</color>] «Was sind <color={HEX_CYAN}>Bremsen</color>?»";
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>Radio</color>] «<color={HEX_MAGENTA}>Adrenalin</color> — der beste Treibstoff»";
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>Radio</color>] «Geschwindigkeit macht dich <color={HEX_CYAN}>frei</color>»";
    }
}
