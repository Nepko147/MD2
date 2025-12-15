using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_Spanish : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();
        
        text_keyToString[Text_Key.startText_desktop] = "PRESIONE CUALQUIER TECLA";
        text_keyToString[Text_Key.startText_mobile] = "TOCA LA PANTALLA";
        text_keyToString[Text_Key.loadingCloudData] = "CARICAMENTO DEI DATI DAL CLOUD";
        text_keyToString[Text_Key.upgrade_moreCoins] = "MÁS MONEDAS";
        text_keyToString[Text_Key.upgrade_moreBonuses] = "MÁS BONOS";
        text_keyToString[Text_Key.upgrade_coinMagnet] = "IMÁN DE MONEDAS";
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "ÉL NO MURIÓ";
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "NO HAY SUFICIENTES MONEDAS";
        text_keyToString[Text_Key.tutorial_desktop] = "Para moverse, mantenga presionado el botón izquierdo del mouse y luego tire del joystick virtual en la dirección del movimiento.";
        text_keyToString[Text_Key.tutorial_mobile] = "Para moverse, mantenga el dedo sobre la pantalla y luego tire del joystick virtual en la dirección del movimiento.";
        text_keyToString[Text_Key.indicators_complete] = "KILÓMETROS RESTANTES";
        text_keyToString[Text_Key.midscreen_gameOver] = "JUEGO TERMINADO";
        text_keyToString[Text_Key.midscreen_pause] = "PAUSA";
        text_keyToString[Text_Key.midscreen_distanceRemain] = "La distancia permanece";
        text_keyToString[Text_Key.received_text] = "Monedas recibidas";
        text_keyToString[Text_Key.received_ad_text] = "Consigue х" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " por ver anuncios"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 VIDA";
        text_keyToString[Text_Key.popUp_coin] = "+ 1 Moneda";
        text_keyToString[Text_Key.popUp_coinRush] = "Fiebre de monedas!";
        text_keyToString[Text_Key.gameBy] = "Un juego de LUNAR HOWL";
        text_keyToString[Text_Key.statistics_reviveNumber] = "NÚMERO DE REVIVIRES";
        text_keyToString[Text_Key.statistics_coinsTotal] = "TOTAL DE MONEDAS";
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "MONEDAS GASTADAS EN REVIVIRES";
        text_keyToString[Text_Key.statistics_defeats] = "DERROTAS";
        text_keyToString[Text_Key.statistics_totalDrivings] = "TOTAL DE VIAJES";
        text_keyToString[Text_Key.statistics_best] = "MEJOR RESULTADO";
        text_keyToString[Text_Key.statistics_newRecord] = "¡Nuevo récord!";
        text_keyToString[Text_Key.statistics_gameCompleted] = "Juego completado";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Hola.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Hola, guapo.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "Tengo una caja extra de retales que compré en una venta de garaje. Puedes...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Ven y cógela...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "¿Interesa?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Ya voy en camino.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "¿Dónde estás?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "¡Ya me estoy muriendo de impaciencia!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Casi llego.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "¡Oh, mierda!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "¿Estás bien?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "¿Me oyes?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>radio</color>] «Son exactamente medianoche en el reloj. Estás en la Radio de la Carretera Libre...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>radio</color>] «No pierdas <color={HEX_MAGENTA}>velocidad</color>»";
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>radio</color>] «Otra adelantada. Otra <color={HEX_MAGENTA}>chica</color>»";
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>radio</color>] «Noche — tiempo de <color={HEX_MAGENTA}>acelerar</color>»";
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>radio</color>] «Cuanto más <color={HEX_MAGENTA}>rápido</color> — mejor»";
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>radio</color>] «¡Pon en marcha tu <color={HEX_MAGENTA}>corazón</color>!»";
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>radio</color>] «Una vez que aceleras, ya no puedes detenerte»";
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>radio</color>] «¡Todavía estás <color={HEX_CYAN}>vivo</color>!»";
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>radio</color>] «Tienes la costumbre de jugar con la <color={HEX_MAGENTA}>muerte</color>»";
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_MAGENTA}>Ella</color> encontrará un reemplazo rápidamente»";
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_CYAN}>Vive</color> rápido, <color={HEX_MAGENTA}>muere</color> joven»";
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>radio</color>] «¿Qué son los <color={HEX_CYAN}>frenos</color>?»";
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>radio</color>] «La <color={HEX_MAGENTA}>adrenalina</color> — el mejor combustible»";
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>radio</color>] «La velocidad te hará <color={HEX_MAGENTA}>libre</color>»";
    }
}
