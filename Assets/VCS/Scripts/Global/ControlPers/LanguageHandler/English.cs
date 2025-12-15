using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_English : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();

        text_keyToString[Text_Key.startText_desktop] = "PRESS ANY KEY"; //Press any key
        text_keyToString[Text_Key.startText_mobile] = "TAP ON THE SCREEN"; //Tap on the screen
        text_keyToString[Text_Key.loadingCloudData] = "LOADING CLOUD DATA"; //Loading data from cloud
        text_keyToString[Text_Key.upgrade_moreCoins] = "MORE COINS"; //More coins
        text_keyToString[Text_Key.upgrade_moreBonuses] = "MORE BONUSES"; //More bonuses
        text_keyToString[Text_Key.upgrade_coinMagnet] = "COIN MAGNET"; //Coin magnet
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "HE DIDN'T DIE"; //He didn't die
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "NOT ENOUGH COINS"; //Not enough coins
        text_keyToString[Text_Key.tutorial_desktop] = "To move, hold down the left mouse button, then pull the virtual stick in the direction of movement.";
        text_keyToString[Text_Key.tutorial_mobile] = "To move, hold your finger on the screen, then pull the virtual stick in the direction of movement.";
        text_keyToString[Text_Key.indicators_complete] = "KILOMETERS LEFT"; //Kilometers left
        text_keyToString[Text_Key.midscreen_gameOver] = "GAME OVER"; //Game over
        text_keyToString[Text_Key.midscreen_pause] = "PAUSE"; //Pause
        text_keyToString[Text_Key.midscreen_distanceRemain] = "Distance remain";
        text_keyToString[Text_Key.received_text] = "Received coins";
        text_keyToString[Text_Key.received_ad_text] = "Get x" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " for watching ads"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 Up"; //+1 Life
        text_keyToString[Text_Key.popUp_coin] = "+1 Coin";
        text_keyToString[Text_Key.popUp_coinRush] = "Coin Rush!";
        text_keyToString[Text_Key.gameBy] = "A GAME BY LUNAR HOWL"; //A game by
        text_keyToString[Text_Key.statistics_reviveNumber] = "REVIVE NUMBER"; //Revive number
        text_keyToString[Text_Key.statistics_coinsTotal] = "COINS TOTAL"; //Coins total
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "ÑOINS SPENT ON REVIVALS"; //Coins spent on revivals
        text_keyToString[Text_Key.statistics_defeats] = "DEFEATS"; //Defeats
        text_keyToString[Text_Key.statistics_totalDrivings] = "TOTAL RIDES"; //Total rides
        text_keyToString[Text_Key.statistics_best] = "BEST RESULT"; //Best result
        text_keyToString[Text_Key.statistics_newRecord] = "New record!";
        text_keyToString[Text_Key.statistics_gameCompleted] = "Game completed";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Hello.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Hey, handsome.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "I got an extra box of toothpicks on sale. You can just...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Come and take it...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "Interested?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Already driving.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Where are you?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "I'm already burning with impatience!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Almost there.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "Oh, crap!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "Are you okay?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Can you hear me?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>radio</color>] «It's exactly midnight on the clock. You're tuned to the Free Road Radio...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>radio</color>] «Don't lose <color={HEX_MAGENTA}>speed</color>»"; //Don't lose speed
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>radio</color>] «Another overtaking. Another <color={HEX_MAGENTA}>girlfriend</color>»"; //Another overtaking. Another girlfriend
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>radio</color>] «Night — time to <color={HEX_MAGENTA}>accelerate</color>»"; //Night — time to accelerate
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>radio</color>] «The <color={HEX_MAGENTA}>faster</color> — the better»"; //The faster — the better
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>radio</color>] «Kickstart your <color={HEX_MAGENTA}>heart</color>!»"; //Kickstart your heart!
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>radio</color>] «Once you <color={HEX_MAGENTA}>rev up</color>, you can't <color={HEX_CYAN}>stop</color>»"; //Once you rev up, you can't stop
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>radio</color>] «You're still <color={HEX_CYAN}>alive</color>!»"; //You're still alive!
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>radio</color>] «You have a habit of playing with <color={HEX_MAGENTA}>death</color>»"; //You have a habit of playing with death
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_MAGENTA}>She</color>'ll quickly find a replacement»"; //She'll quickly find a replacement
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_CYAN}>Live</color> fast, <color={HEX_MAGENTA}>die</color> fast»"; //Live fast,die fast
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>radio</color>] «What are <color={HEX_CYAN}>brakes</color>?»"; //What are brakes?
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_MAGENTA}>Adrenaline</color> — the best fuel»"; //Adrenaline — the best fuel
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>radio</color>] «Speed will set you <color={HEX_MAGENTA}>free</color>»"; //Speed will set you free
    }
}
