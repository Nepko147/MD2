using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_Kazakh : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();

        text_keyToString[Text_Key.startText_desktop] = "КЕЗ КЕЛГЕН ПЕРНЕНІ БАСЫҢЫЗ"; //Press any key
        text_keyToString[Text_Key.startText_mobile] = "ЭКРАНДЫ ТҮРТІҢІЗ"; //Tap on the screen
        text_keyToString[Text_Key.loadingCloudData] = "БҰЛТТАН ДЕРЕКТЕР ЖҮКТЕЛУДЕ"; //Loading data from cloud
        text_keyToString[Text_Key.upgrade_moreCoins] = "КӨБІРЕК МОНЕТАЛАР"; //More coins
        text_keyToString[Text_Key.upgrade_moreBonuses] = "ҚОСЫМША БОНУСТАР"; //More bonuses
        text_keyToString[Text_Key.upgrade_coinMagnet] = "МОНЕТА МАГНИТІ"; //Coin magnet
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "ОЛ ӨЛГЕН ЖОҚ"; //He didn't die
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "ТИЫНДАР ЖЕТКІЛІКСІЗ"; //Not enough coins
        text_keyToString[Text_Key.tutorial_desktop] = "Жылжыту үшін тінтуірдің сол жақ түймесін басып тұрыңыз, содан кейін виртуалды таяқшаны қозғалыс бағытына қарай тартыңыз.";
        text_keyToString[Text_Key.tutorial_mobile] = "Жылжыту үшін саусағыңызды экранда ұстаңыз, содан кейін виртуалды таяқшаны қозғалыс бағытына қарай тартыңыз.";
        text_keyToString[Text_Key.indicators_complete] = "КИЛОМЕТРЛЕР ҚАЛДЫ"; //Kilometers left
        text_keyToString[Text_Key.midscreen_gameOver] = "ОЙЫН АЯҚТАЛДЫ"; //Game over
        text_keyToString[Text_Key.midscreen_pause] = "КІДІРТУ"; //Pause
        text_keyToString[Text_Key.midscreen_distanceRemain] = "Қашықтық қалады";
        text_keyToString[Text_Key.received_text] = "Алынған монеталар";
        text_keyToString[Text_Key.received_ad_text] = "Жарнамалар көру үшін x" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " алыңыз"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 Өмір"; //+1 Life
        text_keyToString[Text_Key.popUp_coin] = "+1 Монета";
        text_keyToString[Text_Key.popUp_coinRush] = "Монета безгегі!";
        text_keyToString[Text_Key.gameBy] = "LUNAR HOWL ойыны"; //A game by LUNAR HOWL
        text_keyToString[Text_Key.statistics_reviveNumber] = "ҚАЙТА ТІРІЛУ САНЫ"; //Number of resurrections
        text_keyToString[Text_Key.statistics_coinsTotal] = "COINS TOTAL"; //Coins total
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "ҚАЙТА ТІРІЛУГЕ ​​ЖҰМСАЛҒАН ТИЫНДАР"; //Coins spent on resurrections
        text_keyToString[Text_Key.statistics_defeats] = "ЖЕҢІЛІСТЕР"; //Defeats
        text_keyToString[Text_Key.statistics_totalDrivings] = "ЖАЛПЫ САПАРЛАР"; //Total rides
        text_keyToString[Text_Key.statistics_best] = "ЕҢ ЖАҚСЫ НӘТИЖЕ"; //Best result
        text_keyToString[Text_Key.statistics_newRecord] = "Жаңа рекорд!";
        text_keyToString[Text_Key.statistics_gameCompleted] = "Ойын аяқталды";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Сәлем.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Эй, әдемі.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "Мен сатылымда қосымша бір қорап тіс тазалағыш алдым. Сіз жай ғана...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Келіңіз және алыңыз...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "Қызықты ма?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Мен қазірдің өзінде айдап жатырмын.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Сен қайдасың?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "Мен онсыз да шыдамсыздықтан күйіп жатырмын!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Жақында.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "О, сұмдық!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "Сенімен бәрі жақсы ма?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Сен мені ести аласың ба?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>радио</color>] «Сағат бойынша дәл түн ортасы. Сіз Free Road радиосына бейімделдіңіз...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>радио</color>] «<color={HEX_MAGENTA}>Жылдамдықты</color> жоғалтпаңыз»"; //Don't lose speed
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>радио</color>] «Тағы бір басып озу. Басқа <color={HEX_MAGENTA}>қыз</color>»"; //Another overtaking. Another girlfriend
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>радио</color>] «Түн – <color={HEX_MAGENTA}>тездету</color> уақыты»"; //Night — time to accelerate
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>радио</color>] «<color={HEX_MAGENTA}>Тезірек</color> - соғұрлым жақсы»"; //The faster — the better
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>радио</color>] «<color={HEX_MAGENTA}>Жүрегіңізді</color> іске қосыңыз!»";
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>радио</color>] «Бір рет <color={HEX_MAGENTA}>көтерілгенде</color>, сіз <color={HEX_CYAN}>тоқтай</color> алмайсыз»"; //Once you rev up, you can't stop
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>радио</color>] «Сен әлі <color={HEX_CYAN}>тірісің</color>!»"; //You're still alive!
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>радио</color>] «<color={HEX_MAGENTA}>Өліммен</color> ойнайтын әдетің бар»"; //You have a habit of playing with death
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>радио</color>] «<color={HEX_MAGENTA}>Ол</color> тез арада алмастырушы табады»"; //She'll quickly find a replacement
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>радио</color>] «Тез <color={HEX_CYAN}>өмір</color> сүр, тез <color={HEX_MAGENTA}>өл</color>»"; //Live fast,die fast
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>радио</color>] «<color={HEX_CYAN}>Тежегіштер</color> дегеніміз не?»"; //What are brakes?
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>радио</color>] «<color={HEX_MAGENTA}>Адреналин</color> - ең жақсы отын»"; //Adrenaline — the best fuel
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>радио</color>] «Жылдамдық сізді <color={HEX_MAGENTA}>босатады</color>»"; //Speed will set you free
        /////////////////////////////////////////////////////////////////////////////////
    }
}
