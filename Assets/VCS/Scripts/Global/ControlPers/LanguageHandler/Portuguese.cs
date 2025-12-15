using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_Portuguese : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();
        
        text_keyToString[Text_Key.startText_desktop] = "PRESSIONE QUALQUER TECLA";
        text_keyToString[Text_Key.startText_mobile] = "TOQUE NA TELA";
        text_keyToString[Text_Key.loadingCloudData] = "CARREGANDO DADOS DA NUVEM";
        text_keyToString[Text_Key.upgrade_moreCoins] = "MAIS MOEDAS";
        text_keyToString[Text_Key.upgrade_moreBonuses] = "MAIS BÔNUS";
        text_keyToString[Text_Key.upgrade_coinMagnet] = "ÍMÃ DE MOEDAS";
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "ELE NÃO MORREU";
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "MOEDAS NÃO SUFICIENTES";
        text_keyToString[Text_Key.tutorial_desktop] = "Para mover, mantenha pressionado o botão esquerdo do mouse e, em seguida, arraste o joystick virtual na direção do movimento.";
        text_keyToString[Text_Key.tutorial_mobile] = "Para se mover, mantenha o dedo pressionado na tela e, em seguida, arraste o joystick virtual na direção do movimento.";
        text_keyToString[Text_Key.indicators_complete] = "QUILÔMETROS RESTANTES";
        text_keyToString[Text_Key.midscreen_gameOver] = "FIM DO JOGO";
        text_keyToString[Text_Key.midscreen_pause] = "PAUSA";
        text_keyToString[Text_Key.midscreen_distanceRemain] = "Distância permanece";
        text_keyToString[Text_Key.received_text] = "Moedas recebidas";
        text_keyToString[Text_Key.received_ad_text] = "Ganhe x" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " ao assistir anúncios"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 Vida";
        text_keyToString[Text_Key.popUp_coin] = "+1 Moeda";
        text_keyToString[Text_Key.popUp_coinRush] = "Corrida de moedas!";
        text_keyToString[Text_Key.gameBy] = "Um jogo de LUNAR HOWL";
        text_keyToString[Text_Key.statistics_reviveNumber] = "NÚMERO DE REVIVES";
        text_keyToString[Text_Key.statistics_coinsTotal] = "TOTAL DE MOEDAS";
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "MOEDAS GASTAS EM REVIVES";
        text_keyToString[Text_Key.statistics_defeats] = "DERROTAS";
        text_keyToString[Text_Key.statistics_totalDrivings] = "TOTAL DE VIAGENS";
        text_keyToString[Text_Key.statistics_best] = "MELHOR RESULTADO";
        text_keyToString[Text_Key.statistics_newRecord] = "Novo recorde!";
        text_keyToString[Text_Key.statistics_gameCompleted] = " Jogo concluído";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Hola.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Oi, lindo.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "Tenho uma caixa extra de retalhos que comprei em um bazar. Você pode...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Vir pegar...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "Intresute?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Já estou indo.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Onde você está?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "Já estou morrendo de ansiedade!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Quase lá.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "Ah, droga!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "Você está bem?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Você consegue me ouvir?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>rádio</color>] «São exatamente meia-noite no relógio. Você está na Rádio da Estrada Livre...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>rádio</color>] «Não perca <color={HEX_MAGENTA}>velocidade</color>»";
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>rádio</color>] «Mais uma ultrapassagem. Mais uma <color={HEX_MAGENTA}>garota</color>»";
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>rádio</color>] «Noite — hora de <color={HEX_MAGENTA}>acelerar</color>»";
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>rádio</color>] «Quanto mais <color={HEX_MAGENTA}>rápido</color> — melhor»";
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>rádio</color>] «Dê um impulso ao seu coração!»";
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>rádio</color>] «Uma vez <color={HEX_MAGENTA}>acelerando</color>, e não podes <color={HEX_CYAN}>parar</color>»";
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>rádio</color>] «Ainda estás <color={HEX_CYAN}>vivo</color>!»";
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>rádio</color>] «Tens o hábito de namoriscar com a <color={HEX_MAGENTA}>morte</color>»";
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>rádio</color>] «<color={HEX_MAGENTA}>Ela</color> encontrará um substituto rapidamente»";
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>rádio</color>] «<color={HEX_CYAN}>Vive</color> rápido, <color={HEX_MAGENTA}>morre rápido</color>»";
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>rádio</color>] «O que são <color={HEX_CYAN}>freios</color>?»";
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>rádio</color>] «A <color={HEX_MAGENTA}>adrenalina</color> é o melhor combustível»";
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>rádio</color>] «A velocidade <color={HEX_CYAN}>libertá-lo-á</color>»";
    }
}
