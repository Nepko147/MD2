using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_Italian : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();

        text_keyToString[Text_Key.startText_desktop] = "PREMI UN TASTO QUALSIASI"; //Press any key
        text_keyToString[Text_Key.startText_mobile] = "TOCCA LO SCHERMO"; //Tap on the screen
        text_keyToString[Text_Key.loadingCloudData] = "CARICAMENTO DEI DATI DAL CLOUD"; //Loading data from cloud
        text_keyToString[Text_Key.upgrade_moreCoins] = "ALTRE MONETE"; //More coins
        text_keyToString[Text_Key.upgrade_moreBonuses] = "ALTRI BONUS"; //More bonuses
        text_keyToString[Text_Key.upgrade_coinMagnet] = "MAGNETE PER MONETE"; //Coin magnet
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "NON È MORTO"; //He didn't die
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "NON CI SONO ABBASTANZA MONETE"; //Not enough coins
        text_keyToString[Text_Key.tutorial_desktop] = "Per muoverti, tieni premuto il tasto sinistro del mouse, quindi tira la levetta virtuale nella direzione del movimento.";
        text_keyToString[Text_Key.tutorial_mobile] = "Per muoverti, tieni premuto il dito sullo schermo, quindi tira la levetta virtuale nella direzione del movimento.";
        text_keyToString[Text_Key.indicators_complete] = "CHILOMETRI RIMASTI"; //Kilometers left
        text_keyToString[Text_Key.midscreen_gameOver] = "GAME OVER"; //Game over
        text_keyToString[Text_Key.midscreen_pause] = "PAUSA"; //Pause
        text_keyToString[Text_Key.midscreen_distanceRemain] = "Distanza rimanente";
        text_keyToString[Text_Key.received_text] = "Monete ricevute";
        text_keyToString[Text_Key.received_ad_text] = "Ottieni x" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " per guardare gli annunci"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 Vita"; //+1 Life
        text_keyToString[Text_Key.popUp_coin] = "+1 Moneta";
        text_keyToString[Text_Key.popUp_coinRush] = "Corsa alle monete!";
        text_keyToString[Text_Key.gameBy] = "UN GIOCO DI LUNAR HOWL"; //A game by LUNAR HOWL
        text_keyToString[Text_Key.statistics_reviveNumber] = "RIANIMA IL NUMERO"; //Revive number
        text_keyToString[Text_Key.statistics_coinsTotal] = "TOTALE MONETE"; //Coins total
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "MONETE SPESE PER I RISVEGLI"; //Coins spent on revivals
        text_keyToString[Text_Key.statistics_defeats] = "SCONFITTE"; //Defeats
        text_keyToString[Text_Key.statistics_totalDrivings] = "NUMERO TOTALE DI CORSE"; //Total rides
        text_keyToString[Text_Key.statistics_best] = "MIGLIOR RISULTATO"; //Best result
        text_keyToString[Text_Key.statistics_newRecord] = "Nuovo record!";
        text_keyToString[Text_Key.statistics_gameCompleted] = "Gioco completato";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Ciao.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Ehi, bello.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "Ho una scatola di stuzzicadenti in più in saldo. Puoi semplicemente...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Vieni a prenderlo...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "Interessato?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Sto già guidando.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Dove sei?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "Sto già bruciando dall'impazienza!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Ci siamo quasi.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "Oh, cavolo!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "Stai bene?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Riesci a sentirmi?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>radio</color>] «È esattamente mezzanotte. Sei sintonizzato su Free Road Radio...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>radio</color>] «Non perdere <color={HEX_MAGENTA}>velocità</color>»"; //Don't lose speed
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>radio</color>] «Un altro sorpasso. Un'altra <color={HEX_MAGENTA}>fidanzata</color>»"; //Another overtaking. Another girlfriend
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>radio</color>] «Notte: è il momento di <color={HEX_MAGENTA}>accelerare</color>»"; //Night — time to accelerate
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>radio</color>] «Più <color={HEX_MAGENTA}>veloce</color> è, meglio è»"; //The faster — the better
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>radio</color>] «Dai una scossa al tuo <color={HEX_MAGENTA}>cuore</color>!»";
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>radio</color>] «Una volta che si <color={HEX_MAGENTA}>accelera</color>, non si può più <color={HEX_CYAN}>fermarsi</color>»"; //Once you rev up, you can't stop
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>radio</color>] «Sei ancora <color={HEX_CYAN}>vivo</color>!»"; //You're still alive!
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>radio</color>] «Hai l'abitudine di giocare con la <color={HEX_MAGENTA}>morte</color>»"; //You have a habit of playing with death
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_MAGENTA}>Troverà</color> rapidamente un sostituto»"; //She'll quickly find a replacement
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_CYAN}>Vivi</color> velocemente, <color={HEX_MAGENTA}>muori</color> velocemente»"; //Live fast,die fast
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>radio</color>] «Cosa sono i <color={HEX_CYAN}>freni</color>?»"; //What are brakes?
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_MAGENTA}>Adrenalina</color>: il miglior carburante»"; //Adrenaline — the best fuel
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>radio</color>] «La velocità ti renderà <color={HEX_MAGENTA}>libero</color>»"; //Speed will set you free
    }
}
