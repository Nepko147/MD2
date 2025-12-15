using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_Russian : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();
        
        text_keyToString[Text_Key.startText_desktop] = "НАЖМИТЕ ЛЮБУЮ КЛАВИШУ";
        text_keyToString[Text_Key.startText_mobile] = "НАЖМИТЕ НА ЭКРАН";
        text_keyToString[Text_Key.loadingCloudData] = "ЗАГРУЗКА ДАННЫХ ИЗ ОБЛАКА";
        text_keyToString[Text_Key.upgrade_moreCoins] = "БОЛЬШЕ МОНЕТ";
        text_keyToString[Text_Key.upgrade_moreBonuses] = "БОЛЬШЕ БОНУСОВ";
        text_keyToString[Text_Key.upgrade_coinMagnet] = "МАГНИТ ДЛЯ МОНЕТ";
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "ДА НЕ УМЕР ОН";
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "НЕДОСТАТОЧНО МОНЕТ";
        text_keyToString[Text_Key.tutorial_desktop] = "Для движения зажмите левую кнопку мыши, затем потяните виртуальный стик в направлении движения.";
        text_keyToString[Text_Key.tutorial_mobile] = "Для перемещения удерживайте палец на экране, затем потяните виртуальный стик в направлении движения.";
        text_keyToString[Text_Key.indicators_complete] = "КМ ДО ЦЕЛИ";
        text_keyToString[Text_Key.midscreen_gameOver] = "КОНЕЦ ИГРЫ";
        text_keyToString[Text_Key.midscreen_pause] = "ПАУЗА";
        text_keyToString[Text_Key.midscreen_distanceRemain] = "Осталось до цели";
        text_keyToString[Text_Key.received_text] = "Полученные монеты";
        text_keyToString[Text_Key.received_ad_text] = "Получить х" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " за просмотр рекламы"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 Жизнь";
        text_keyToString[Text_Key.popUp_coin] = "+1 Монета";
        text_keyToString[Text_Key.popUp_coinRush] = "Монетная Лихорадка!";
        text_keyToString[Text_Key.gameBy] = "ИГРА ОТ LUNAR HOWL";
        text_keyToString[Text_Key.statistics_reviveNumber] = "КОЛИЧЕСТВО ВОЗРОЖДЕНИЙ";
        text_keyToString[Text_Key.statistics_coinsTotal] = "ОБЩЕЕ ЧИСЛО МОНЕТ";
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "МОНЕТ ПОТРАЧЕНО НА ВОЗРОЖДЕНИЯ";
        text_keyToString[Text_Key.statistics_defeats] = "ПОРАЖЕНИЙ";
        text_keyToString[Text_Key.statistics_totalDrivings] = "ОБЩЕЕ ЧИСЛО ЗАЕЗДОВ";
        text_keyToString[Text_Key.statistics_best] = "ЛУЧШИЙ РЕЗУЛЬТАТ";
        text_keyToString[Text_Key.statistics_newRecord] = "Новый рекорд!";
        text_keyToString[Text_Key.statistics_gameCompleted] = "Игра пройдена";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Ало.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Привет красавчик.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "Я тут получила лишнюю коробку зубочисток по акции. Можешь просто...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Прийти и взять её...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "Интересует?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Уже еду.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Ты где?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "Я уже сгораю от нетерпения!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Почти на месте.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "О, черт!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "Все в порядке?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Ты меня слышишь?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>радио</color>] «На часах ровно полночь. В эфире Радио Свободной Дороги...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>радио</color>] «Не теряй <color={HEX_MAGENTA}>скорость</color>»";
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>радио</color>] «Ещё один обгон. Еще одна <color={HEX_MAGENTA}>подружка</color>»";
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>радио</color>] «Ночь — время <color={HEX_MAGENTA}>ускорить темп</color>»";
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>радио</color>] «Чем <color={HEX_MAGENTA}>быстрее</color> — тем лучше»";
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>радио</color>] «Заставь <color={HEX_MAGENTA}>сердце</color> биться чаще!»";
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>радио</color>] «Стоит <color={HEX_MAGENTA}>разогнать</color> и уже не <color={HEX_CYAN}>остановить</color>»";
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>радио</color>] «Ты всё еще <color={HEX_CYAN}>жив</color>!»";
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>радио</color>] «У тебя привычка играть со <color={HEX_MAGENTA}>смертью</color>»";
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>радио</color>] «<color={HEX_MAGENTA}>Она</color> быстро найдет замену»";
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>радио</color>] «<color={HEX_CYAN}>Живи</color> быстро, <color={HEX_MAGENTA}>умри</color> быстро»";
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>радио</color>] «Что такое <color={HEX_CYAN}>тормоза</color>?»";
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>радио</color>] «<color={HEX_MAGENTA}>Адреналин</color> — лучшее топливо»";
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>радио</color>] «Скорость сделает тебя <color={HEX_MAGENTA}>свободным</color>»";
    }
}
