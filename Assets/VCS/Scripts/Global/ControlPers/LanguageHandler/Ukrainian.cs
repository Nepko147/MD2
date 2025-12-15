using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_Ukrainian : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();

        text_keyToString[Text_Key.startText_desktop] = "НАТИСНІТЬ БУДЬ-ЯКУ КЛАВІШУ"; //Press any key
        text_keyToString[Text_Key.startText_mobile] = "НАТИСНІТЬ НА ЕКРАН"; //Tap on the screen
        text_keyToString[Text_Key.loadingCloudData] = "ЗАВАНТАЖЕННЯ ДАНИХ"; //Loading data from cloud
        text_keyToString[Text_Key.upgrade_moreCoins] = "БІЛЬШЕ МОНЕТ"; //More coins
        text_keyToString[Text_Key.upgrade_moreBonuses] = "БІЛЬШЕ БОНУСІВ"; //More bonuses
        text_keyToString[Text_Key.upgrade_coinMagnet] = "МАГНІТ ДЛЯ МОНЕТ"; //Coin magnet
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "ТА НЕ ПОМЕР ВІН"; //He didn't die
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "НЕДОСТАТНЬО МОНЕТ"; //Not enough coins
        text_keyToString[Text_Key.tutorial_desktop] = "Для руху затисніть ліву кнопку миші, потім потягніть віртуальний стік в напрямку руху.";
        text_keyToString[Text_Key.tutorial_mobile] = "Для переміщення утримуйте палець на екрані, потім потягніть віртуальний стік в напрямку руху.";
        text_keyToString[Text_Key.indicators_complete] = "КМ ДО МЕТИ"; //Kilometers left
        text_keyToString[Text_Key.midscreen_gameOver] = "КІНЕЦЬ ГРИ"; //Game over
        text_keyToString[Text_Key.midscreen_pause] = "ПАУЗА"; //Pause
        text_keyToString[Text_Key.midscreen_distanceRemain] = "Залишилося до мети";
        text_keyToString[Text_Key.received_text] = "Отримано монет";
        text_keyToString[Text_Key.received_ad_text] = "Отримати х" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " за перегляд реклами"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 Життя"; //+1 Life
        text_keyToString[Text_Key.popUp_coin] = "+1 Монета";
        text_keyToString[Text_Key.popUp_coinRush] = "Монетна Лихоманка!";
        text_keyToString[Text_Key.gameBy] = "ГРА LUNAR HOWL"; //A game by LUNAR HOWL
        text_keyToString[Text_Key.statistics_reviveNumber] = "КІЛЬКІСТЬ ВІДРОДЖЕНЬ"; //Revive number
        text_keyToString[Text_Key.statistics_coinsTotal] = "ВСЬОГО МОНЕТ"; //Coins total
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "ВИТРАЧЕНО МОНЕТ НА ВІДРОДЖЕННЯ"; //Coins spent on revivals
        text_keyToString[Text_Key.statistics_defeats] = "ПОРАЖЕНИЯ"; //Defeats
        text_keyToString[Text_Key.statistics_totalDrivings] = "ВСЬОГО ПОЇЗДОК"; //Total rides
        text_keyToString[Text_Key.statistics_best] = "КРАЩИЙ"; //Best result
        text_keyToString[Text_Key.statistics_newRecord] = "Новий рекорд!";
        text_keyToString[Text_Key.statistics_gameCompleted] = "Гра пройдена";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Ало.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Привіт, красунчик.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "Я тут отримала зайву коробку зубочисток по акції. Можеш просто...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Прийти і взяти її...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "Цікавить?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Вже їду.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Ти де?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "Я вже згораю від нетерпіння!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Майже на місці.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "О, чорт!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "Все гаразд?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Ти мене чуєш?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>радіо</color>] «На годиннику рівно опівночі. В ефірі Радіо Вільної Дороги...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>радіо</color>] «Не втрачайте <color={HEX_MAGENTA}>швидкість</color>»"; //Don't lose speed
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>радіо</color>] «Ще один обгін. Ще одна <color={HEX_MAGENTA}>подружка</color>»"; //Another overtaking. Another girlfriend
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>радіо</color>] «Ніч-час прискорити <color={HEX_MAGENTA}>прискорити темп</color>»"; //Night — time to accelerate
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>радіо</color>] «Чим <color={HEX_MAGENTA}>швидше</color> — тим краще»"; //The faster — the better
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>радіо</color>] «Змусьте <color={HEX_MAGENTA}>серце</color> битися частіше!»";
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>радіо</color>] «Варто <color={HEX_MAGENTA}>розігнати</color> і вже не <color={HEX_CYAN}>зупинити</color>»"; //Once you rev up, you can't stop
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>радіо</color>] «Ти все ще <color={HEX_CYAN}>живий</color>!»"; //You're still alive!
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>радіо</color>] «У тебе звичка грати зі <color={HEX_MAGENTA}>смертю</color>»"; //You have a habit of playing with death
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>радіо</color>] «<color={HEX_MAGENTA}>Вона</color> швидко знайде заміну»"; //She'll quickly find a replacement
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>радіо</color>] «<color={HEX_CYAN}>Живи</color> швидко, <color={HEX_MAGENTA}>помри</color> швидко»"; //Live fast,die fast
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>радіо</color>] «Що таке <color={HEX_CYAN}>гальма</color>?»"; //What are brakes?
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>радіо</color>] «<color={HEX_MAGENTA}>Адреналін</color> — найкраще паливо»"; //Adrenaline — the best fuel
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>радіо</color>] «Швидкість зробить вас <color={HEX_MAGENTA}>вільним</color>»"; //Speed will set you free
    }
}
