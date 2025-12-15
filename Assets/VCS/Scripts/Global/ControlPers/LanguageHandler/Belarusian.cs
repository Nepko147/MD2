using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_Belarusian : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();

        text_keyToString[Text_Key.startText_desktop] = "НАЦІСНІЦЕ ЛЮБУЮ КЛАВІШУ"; //Press any key
        text_keyToString[Text_Key.startText_mobile] = "НАЦІСНІЦЕ НА ЭКРАН"; //Tap on the screen
        text_keyToString[Text_Key.loadingCloudData] = "ЗАГРУЗКА ХМАРНЫХ ДАДЗЕНЫХ"; //Loading data from cloud
        text_keyToString[Text_Key.upgrade_moreCoins] = "БОЛЬШ МАНЕТ"; //More coins
        text_keyToString[Text_Key.upgrade_moreBonuses] = "БОЛЬШ БОНУСАЎ"; //More bonuses
        text_keyToString[Text_Key.upgrade_coinMagnet] = "CМАГНІТ ДЛЯ МАНЕТ"; //Coin magnet
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "ДЫ НЕ ПАМЁР ЁН"; //He didn't die
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "НЕ ХАПАЕ МАНЕТ"; //Not enough coins
        text_keyToString[Text_Key.tutorial_desktop] = "Для перамяшчэння, ўтрымлівайце націснутай левую кнопку мышы, затым пацягніце віртуальны джойсцік ў кірунку руху.";
        text_keyToString[Text_Key.tutorial_mobile] = "Для перамяшчэння, ўтрымлівайце палец на экране, а затым пацягніце віртуальны джойсцік ў кірунку руху.";
        text_keyToString[Text_Key.indicators_complete] = "КІЛАМЕТРАЎ ДА ЦЭЛІ"; //Kilometers left
        text_keyToString[Text_Key.midscreen_gameOver] = "ГУЛЬНЯ СКОНЧАНА"; //Game over
        text_keyToString[Text_Key.midscreen_pause] = "ПАЎЗА"; //Pause
        text_keyToString[Text_Key.midscreen_distanceRemain] = "Засталося да мэты"; //Distance remain
        text_keyToString[Text_Key.received_text] = "Атрыманыя манеты";
        text_keyToString[Text_Key.received_ad_text] = "Атрымайце x" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " за прагляд рэкламы"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 Жыццё"; //+1 Life
        text_keyToString[Text_Key.popUp_coin] = "+1 Манета";
        text_keyToString[Text_Key.popUp_coinRush] = "Манетная Ліхаманка!";
        text_keyToString[Text_Key.gameBy] = "ГУЛЬНЯ LUNAR HOWL"; //A game by LUNAR HOWL
        text_keyToString[Text_Key.statistics_reviveNumber] = "КОЛЬКАСЦЬ АДРАДЖЭННЯЎ"; //Revive number
        text_keyToString[Text_Key.statistics_coinsTotal] = "УСЯГО МАНЕТ"; //Coins total
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "ВЫДАТКАВАНА МАНЕТ НА АДРАДЖЭННЯ"; //Coins spent on revivals
        text_keyToString[Text_Key.statistics_defeats] = "ПАРАЖЭННЯЎ"; //Defeats
        text_keyToString[Text_Key.statistics_totalDrivings] = "УСЯГО ПАЕЗДАК"; //Total rides
        text_keyToString[Text_Key.statistics_best] = "ЛЕПШЫ"; //Best result
        text_keyToString[Text_Key.statistics_newRecord] = "Новы рэкорд!";
        text_keyToString[Text_Key.statistics_gameCompleted] = "Гульня завершана";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Ало.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Прывітанне, прыгажунчык.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "Я тут атрымала лішнюю скрынку калыпкоў па акцыі. Можаш проста...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Прыйсці і ўзяць яе...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "Цікавіць?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Ужо еду.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Ты дзе?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "Я ўжо згараю ад нецярпення!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Амаль на месцы.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "Аб, чорт!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "Усё ў парадку?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Ты мяне чуеш?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>радыё</color>] «На гадзінніку роўна поўнач. У эфіры Радыё Свабоднай Дарогі...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>радыё</color>] «Не губляй <color={HEX_MAGENTA}>хуткасць</color>»"; //Don't lose speed
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>радыё</color>] «Яшчэ адзін абгон. Яшчэ адна <color={HEX_MAGENTA}>сяброўка</color>»"; //Another overtaking. Another girlfriend
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>радыё</color>] «Ноч — час паскорыць <color={HEX_MAGENTA}>тэмп</color>»"; //Night — time to accelerate
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>радыё</color>] «Чым <color={HEX_MAGENTA}>хутчэй</color> — тым лепш»"; //The faster — the better
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>радыё</color>] «Прымусь сваё <color={HEX_MAGENTA}>сэрца</color> біцца часцей!»";
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>радыё</color>] «Варта <color={HEX_MAGENTA}>разагнаць</color>, і ўжо не <color={HEX_CYAN}>спыніць</color>»"; //Once you rev up, you can't stop
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>радыё</color>] «Ты ўсё яшчэ <color={HEX_CYAN}>жывы</color>!»"; //You're still alive!
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>радыё</color>] «У цябе звычка гуляць са <color={HEX_MAGENTA}>смерцю</color>»"; //You have a habit of playing with death
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>радыё</color>] «<color={HEX_MAGENTA}>Яна</color> хутка знойдзе замену»"; //She'll quickly find a replacement
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>радыё</color>] «<color={HEX_CYAN}>Жыві</color> хутка, <color={HEX_MAGENTA}>памры</color> хутка"; //Live fast,die fast
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>радыё</color>] «Што такое <color={HEX_CYAN}>тормазы</color>?»"; //What are brakes?
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>радыё</color>] «<color={HEX_MAGENTA}>Адрэналін</color> — лепшае паліва»"; //Adrenaline — the best fuel
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>радыё</color>] «Хуткасць зробіць цябе <color={HEX_MAGENTA}>свабодным</color>»"; //Speed will set you free
    }
}
