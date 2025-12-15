using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_Polish : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();

        text_keyToString[Text_Key.startText_desktop] = "NACIŚNIJ DOWOLNY KLAWISZ"; //Press any key
        text_keyToString[Text_Key.startText_mobile] = "DOTKNIJ EKRANU"; //Tap on the screen
        text_keyToString[Text_Key.loadingCloudData] = "ŁADOWANIE DANYCH Z CHMURY"; //Loading data from cloud
        text_keyToString[Text_Key.upgrade_moreCoins] = "WIĘCEJ MONET"; //More coins
        text_keyToString[Text_Key.upgrade_moreBonuses] = "WIĘCEJ BONUSÓW"; //More bonuses
        text_keyToString[Text_Key.upgrade_coinMagnet] = "MAGNES NA MONETY"; //Coin magnet
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "ON NIE UMARŁ"; //He didn't die
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "ZA MAŁO MONET"; //Not enough coins
        text_keyToString[Text_Key.tutorial_desktop] = "Aby się poruszać, przytrzymaj lewy przycisk myszy, a następnie pociągnij wirtualny drążek w kierunku ruchu.";
        text_keyToString[Text_Key.tutorial_mobile] = "Aby się poruszać, przytrzymaj palec na ekranie, a następnie pociągnij wirtualny drążek w kierunku ruchu.";
        text_keyToString[Text_Key.indicators_complete] = "POZOSTAŁO KILOMETRÓW"; //Kilometers left
        text_keyToString[Text_Key.midscreen_gameOver] = "KONIEC GRY"; //Game over
        text_keyToString[Text_Key.midscreen_pause] = "PAUZA"; //Pause
        text_keyToString[Text_Key.midscreen_distanceRemain] = "Pozostała odległość"; //Distance remain
        text_keyToString[Text_Key.received_text] = "Otrzymane monety";
        text_keyToString[Text_Key.received_ad_text] = "Otrzymaj x" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " za oglądanie reklam"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 Życie"; //+1 Life
        text_keyToString[Text_Key.popUp_coin] = "+1 Moneta";
        text_keyToString[Text_Key.popUp_coinRush] = "Gorączka monet!";
        text_keyToString[Text_Key.gameBy] = "Gra stworzona przez LUNAR HOWL"; //A game by LUNAR HOWL
        text_keyToString[Text_Key.statistics_reviveNumber] = "Liczba zmartwychwstań"; //Revive number 
        text_keyToString[Text_Key.statistics_coinsTotal] = "ŁĄCZNA LICZBA MONET"; //Coins total
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "MONETY WYDANE NA ODRODZENIA"; //Coins spent on revivals
        text_keyToString[Text_Key.statistics_defeats] = "PORAŻKI"; //Defeats
        text_keyToString[Text_Key.statistics_totalDrivings] = "ŁĄCZNA LICZBA PRZEJAZDÓW"; //Total rides
        text_keyToString[Text_Key.statistics_best] = "NAJLEPSZY WYNIK"; //Best result
        text_keyToString[Text_Key.statistics_newRecord] = "NOWY REKORD!";
        text_keyToString[Text_Key.statistics_gameCompleted] = "Gra ukończona";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Cześć.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Hej, przystojniaku.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "Mam w promocji dodatkowe pudełko wykałaczek. Możesz po prostu...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Przyjdź i weź to...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "Zainteresowany?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Już jadę.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Gdzie jesteś?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "Już płonę z niecierpliwości!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Prawie na miejscu.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "O cholera!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "Czy wszystko w porządku?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Czy mnie słyszysz?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>radio</color>] «Jest dokładnie północ. Słuchasz radia Free Road...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>radio</color>] «Nie trać <color={HEX_MAGENTA}>prędkości</color>"; //Don't lose speed
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>radio</color>] «Kolejne wyprzedzanie. Kolejna <color={HEX_MAGENTA}>dziewczyna</color>»"; //Another overtaking. Another girlfriend
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>radio</color>] «Noc – czas <color={HEX_MAGENTA}>przyspieszyć</color>»"; //Night — time to accelerate
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>radio</color>] «Im <color={HEX_MAGENTA}>szybciej</color>, tym lepiej»"; //The faster — the better
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>radio</color>] «Pobudź swoje serce!»";
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>radio</color>] «Gdy już się <color={HEX_MAGENTA}>rozpędzisz</color>, nie możesz <color={HEX_CYAN}>przestać</color>»"; //Once you rev up, you can't stop
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>radio</color>] «Nadal <color={HEX_CYAN}>żyjesz</color>!»"; //You're still alive!
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>radio</color>] «Masz zwyczaj igrać ze <color={HEX_MAGENTA}>śmiercią</color>»"; //You have a habit of playing with death
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_MAGENTA}>Szybko</color> znajdzie zastępcę»"; //She'll quickly find a replacement
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_CYAN}>Żyj</color> szybko, <color={HEX_MAGENTA}>umieraj</color> szybko»"; //Live fast,die fast
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>radio</color>] «Czym są <color={HEX_CYAN}>hamulce</color>?»"; //What are brakes?
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_MAGENTA}>Adrenalina</color> – najlepsze paliwo»"; //Adrenaline — the best fuel
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>radio</color>] «Prędkość cię <color={HEX_MAGENTA}>wyzwoli</color>»"; //Speed will set you free
    }
}
