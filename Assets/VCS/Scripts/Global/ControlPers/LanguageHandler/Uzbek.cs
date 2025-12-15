using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_Uzbek : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();

        text_keyToString[Text_Key.startText_desktop] = "ISTALGAN TUGMANI BOSING"; //Press any key
        text_keyToString[Text_Key.startText_mobile] = "EKRANNI BOSING"; //Tap on the screen
        text_keyToString[Text_Key.loadingCloudData] = "MA'LUMOTLARNI YUKLASH"; //Loading data from cloud
        text_keyToString[Text_Key.upgrade_moreCoins] = "KO'PROQ TANGALAR"; //More coins
        text_keyToString[Text_Key.upgrade_moreBonuses] = "KO'PROQ BONUSLAR"; //More bonuses
        text_keyToString[Text_Key.upgrade_coinMagnet] = "TANGA MAGNITI"; //Coin magnet
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "U O'LMADI"; //He didn't die
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "TANGALAR ETARLI EMAS"; //Not enough coins
        text_keyToString[Text_Key.tutorial_desktop] = "Harakat qilish uchun sichqonchaning chap tugmasini bosib ushlab turing, so'ngra virtual joystikni harakat yo'nalishi bo'yicha torting.";
        text_keyToString[Text_Key.tutorial_mobile] = "Harakat qilish uchun barmog'ingizni ekranda ushlab turing, so'ngra virtual joystikni harakat yo'nalishi bo'yicha torting.";
        text_keyToString[Text_Key.indicators_complete] = "MAQSADGA KM"; //Kilometers left
        text_keyToString[Text_Key.midscreen_gameOver] = "O'YIN TUGADI"; //Game over
        text_keyToString[Text_Key.midscreen_pause] = "PAUZA"; //Pause
        text_keyToString[Text_Key.midscreen_distanceRemain] = "Qolgan masofa";
        text_keyToString[Text_Key.received_text] = "Olingan";
        text_keyToString[Text_Key.received_ad_text] = "Reklama ko'rish uchun x" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " oling"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 Hayot"; //+1 Life
        text_keyToString[Text_Key.popUp_coin] = "+1 Tanga";
        text_keyToString[Text_Key.popUp_coinRush] = "Tangalar shoshilinchligi!";
        text_keyToString[Text_Key.gameBy] = "LUNAR HOWL TOMONIDAN O'YIN"; //A game by LUNAR HOWL
        text_keyToString[Text_Key.statistics_reviveNumber] = "QAYTA TIKLANISHLAR SONI"; //Revive number
        text_keyToString[Text_Key.statistics_coinsTotal] = "TANGALAR JAMI"; //Coins total
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "QAYTA TIKLASH UCHUN SARFLANGAN TANGALAR"; //Coins spent on revivals
        text_keyToString[Text_Key.statistics_defeats] = "MAG'LUBATLAR"; //Defeats
        text_keyToString[Text_Key.statistics_totalDrivings] = "JAMI SAYOHATLAR"; //Total rides
        text_keyToString[Text_Key.statistics_best] = "ENG YAXSHI NATIJA"; //Best result
        text_keyToString[Text_Key.statistics_newRecord] = "Yangi rekord!";
        text_keyToString[Text_Key.statistics_gameCompleted] = "O'yin yakunlandi";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Alo.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Salom chiroyli.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "Men aksiyada qo'shimcha bir quti tish cho'tkasi oldim. Senga shunchaki...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Kelib olishing mumkin...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "Qiziqasizmi?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Allaqachon mashina haydayapman.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Qayerdasiz?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "Men allaqachon sabrsizlikdan yonib ketyapman!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Deyarli joyida.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "Oh, jahannam!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "Yaxshimisan?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Meni eshityapsanmi?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>radio</color>] «Soat bo'yicha aynan yarim tun. Erkin yo'l radiosi efirida...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>radio</color>] «<color={HEX_MAGENTA}>Tezlikni</color> yo'qotmang speed»"; //Don't lose speed
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>radio</color>] «Yana bir quvib o'tish. Yana bir <color={HEX_MAGENTA}>qiz do'sti</color>»"; //Another overtaking. Another girlfriend
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>radio</color>] «Kecha - <color={HEX_MAGENTA}>tezlashish</color> vaqti accelerate»"; //Night — time to accelerate
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>radio</color>] «Qanchalik <color={HEX_MAGENTA}>tez bo'lsa</color> — shuncha yaxshi»"; //The faster — the better
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>radio</color>] «<color={HEX_MAGENTA}>Yuragingizni</color> tezroq urishga majbur qiling!»";
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>radio</color>] «<color={HEX_MAGENTA}>Tezlikni</color> oshirgach, <color={HEX_CYAN}>to'xtab</color> bo'lmaydi»"; //Once you rev up, you can't stop
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>radio</color>] «Siz hali ham <color={HEX_CYAN}>tiriksiz</color>!»"; //You're still alive!
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>radio</color>] «Sizda <color={HEX_MAGENTA}>o'lim</color> bilan o'ynash odati bor»"; //You have a habit of playing with death
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_MAGENTA}>U ayol</color> tezda o'rnini topadi»"; //She'll quickly find a replacement
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>radio</color>] «Tez <color={HEX_CYAN}>yashang</color>, tez <color={HEX_MAGENTA}>o'ling</color>»"; //Live fast,die fast
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_CYAN}>Tormozlar</color> nima?»"; //What are brakes?
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_MAGENTA}>Adrenalin</color> — eng yaxshi yoqilg'idir»"; //Adrenaline — the best fuel
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>radio</color>] «Tezlik sizni <color={HEX_MAGENTA}>ozod qiladi</color>»"; //Speed will set you free
    }
}
