using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_French : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();

        text_keyToString[Text_Key.startText_desktop] = "APPUYEZ SUR N'IMPORTE QUELLE TOUCHE"; //Press any key
        text_keyToString[Text_Key.startText_mobile] = "APPUYEZ SUR L'ÉCRAN"; //Tap on the screen
        text_keyToString[Text_Key.loadingCloudData] = "CHARGEMENT DES DONNÉES DEPUIS LE CLOUD"; //Loading data from cloud
        text_keyToString[Text_Key.upgrade_moreCoins] = "PLUS DE PIÈCES"; //More coins
        text_keyToString[Text_Key.upgrade_moreBonuses] = "MORE BONUSES"; //More bonuses
        text_keyToString[Text_Key.upgrade_coinMagnet] = "PLUS DE BONUS"; //Coin magnet
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "IL N'EST PAS MORT"; //He didn't die
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "PAS ASSEZ DE PIÈCES"; //Not enough coins
        text_keyToString[Text_Key.tutorial_desktop] = "Pour vous déplacer, maintenez le bouton gauche de la souris enfoncé, puis tirez le joystick virtuel dans la direction du déplacement.";
        text_keyToString[Text_Key.tutorial_mobile] = "Pour vous déplacer, maintenez votre doigt sur l'écran, puis tirez le joystick virtuel dans la direction du mouvement.";
        text_keyToString[Text_Key.indicators_complete] = "KILOMÈTRES RESTANTS"; //Kilometers left
        text_keyToString[Text_Key.midscreen_gameOver] = "JEU TERMINÉ"; //Game over
        text_keyToString[Text_Key.midscreen_pause] = "PAUSE"; //Pause
        text_keyToString[Text_Key.midscreen_distanceRemain] = "La distance reste";
        text_keyToString[Text_Key.received_text] = "Pièces reçues";
        text_keyToString[Text_Key.received_ad_text] = "Gagnez x" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " en regardant des publicités"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 Vie"; //+1 Life
        text_keyToString[Text_Key.popUp_coin] = "+1 pièce";
        text_keyToString[Text_Key.popUp_coinRush] = "Ruée vers les pièces !";
        text_keyToString[Text_Key.gameBy] = "UN JEU DE LUNAR HOWL"; //A game by LUNAR HOWL
        text_keyToString[Text_Key.statistics_reviveNumber] = "RÉANIMER LE NUMÉRO"; //Revive number
        text_keyToString[Text_Key.statistics_coinsTotal] = "TOTAL DES PIÈCES"; //Coins total
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "PIÈCES DÉPENSÉES POUR LA RÉSURRECTION"; //Coins spent on resurrection
        text_keyToString[Text_Key.statistics_defeats] = "DÉFAITES"; //Defeats
        text_keyToString[Text_Key.statistics_totalDrivings] = "TOTAL RIDES"; //Total rides
        text_keyToString[Text_Key.statistics_best] = "MEILLEUR RÉSULTAT"; //Best result
        text_keyToString[Text_Key.statistics_newRecord] = "NOUVEAU RECORD !";
        text_keyToString[Text_Key.statistics_gameCompleted] = "Partie terminée";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Bonjour.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Salut, beau gosse.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "J'ai acheté une boîte de cure-dents en plus, en promotion. Tu peux juste...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Venez le prendre...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "Intéressé?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Je conduis déjà.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Où es-tu?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "Je brûle déjà d'impatience !";
        text_keyToString[Text_Key.dialogue_end_string_3] = "On y est presque.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "Oh, mince !";
        text_keyToString[Text_Key.dialogue_end_string_5] = "Êtes-vous d'accord?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Pouvez-vous m'entendre?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>radio</color>] «Il est minuit pile. Vous écoutez Free Road Radio…»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>radio</color>] «Ne perdez pas de <color={HEX_MAGENTA}>vitesse</color>»"; //Don't lose speed
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>radio</color>] «Encore un dépassement. Encore une <color={HEX_MAGENTA}>petite amie</color>»"; //Another overtaking. Another girlfriend
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>radio</color>] «La nuit, il est temps <color={HEX_MAGENTA}>d'accélérer</color>»"; //Night — time to accelerate
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>radio</color>] «Plus <color={HEX_MAGENTA}>vite</color>, mieux c'est»"; //The faster — the better
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>radio</color>] «Faites travailler votre <color={HEX_MAGENTA}>cœur</color> !»"; //Kickstart your heart!
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>radio</color>] «Une fois <color={HEX_MAGENTA}>lancé</color>, impossible de <color={HEX_CYAN}>s'arrêter</color>»"; //Once you rev up, you can't stop
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>radio</color>] «Tu es toujours en <color={HEX_CYAN}>vie</color> !»"; //You're still alive!
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>radio</color>] «Vous avez la fâcheuse habitude de jouer avec la <color={HEX_MAGENTA}>mort</color>»"; //You have a habit of playing with death
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_MAGENTA}>Elle</color> trouvera rapidement un remplaçant»"; //She'll quickly find a replacement
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_CYAN}>Vivre</color> vite, <color={HEX_MAGENTA}>mourir</color> vite»"; //Live fast,die fast
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>radio</color>] «Que sont les <color={HEX_CYAN}>freins</color> ?»"; //What are brakes?
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_MAGENTA}>L'adrénaline</color> — le meilleur carburant»"; //Adrenaline — the best fuel
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>radio</color>] «La vitesse vous <color={HEX_MAGENTA}>libérera</color>»"; //Speed will set you free
    }
}
