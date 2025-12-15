using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_Indonesian : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();
        
        text_keyToString[Text_Key.startText_desktop] = "TEKAN TOMBOL APA SAJA";
        text_keyToString[Text_Key.startText_mobile] = "KETUK LAYAR";
        text_keyToString[Text_Key.loadingCloudData] = "MEMUAT DATA DARI CLOUD";
        text_keyToString[Text_Key.upgrade_moreCoins] = "LEBIH BANYAK KOIN";
        text_keyToString[Text_Key.upgrade_moreBonuses] = "LEBIH BANYAK BONUS";
        text_keyToString[Text_Key.upgrade_coinMagnet] = "KOIN MAGNET";
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "DIA TIDAK MATI";
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "KOIN TIDAK CUKUP";
        text_keyToString[Text_Key.tutorial_desktop] = "Untuk bergerak, tahan tombol kiri mouse, lalu tarik stik virtual searah gerakan.";
        text_keyToString[Text_Key.tutorial_mobile] = "Untuk bergerak, tahan jari Anda pada layar, lalu tarik stik virtual searah gerakan.";
        text_keyToString[Text_Key.indicators_complete] = "KILOMETER TERSISA";
        text_keyToString[Text_Key.midscreen_gameOver] = "PERMAINAN BERAKHIR";
        text_keyToString[Text_Key.midscreen_pause] = "BERHENTI SEBENTAR";
        text_keyToString[Text_Key.midscreen_distanceRemain] = "Jarak yang tersisa";
        text_keyToString[Text_Key.received_text] = "Menerima";
        text_keyToString[Text_Key.received_ad_text] = "Dapatkan x" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " untuk menonton iklan"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+ 1 Kehidupan";
        text_keyToString[Text_Key.popUp_coin] = "+1 Koin";
        text_keyToString[Text_Key.popUp_coinRush] = "Demam Koin!";
        text_keyToString[Text_Key.gameBy] = "Permainan oleh LUNAR HOWL";
        text_keyToString[Text_Key.statistics_reviveNumber] = "JUMLAH KELAHIRAN KEMBALI";
        text_keyToString[Text_Key.statistics_coinsTotal] = "TOTAL KOIN";
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "KOIN UNTUK KEBANGKITAN";
        text_keyToString[Text_Key.statistics_defeats] = "KEKALAHAN";
        text_keyToString[Text_Key.statistics_totalDrivings] = "TOTAL PERJALANAN";
        text_keyToString[Text_Key.statistics_best] = "HASIL TERBAIK";
        text_keyToString[Text_Key.statistics_newRecord] = "Rekor baru!";
        text_keyToString[Text_Key.statistics_gameCompleted] = "Permainan selesai";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Halo.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Hai, tampan.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "Saya punya sekotak sisa barang bekas yang saya beli di obral pekarangan. Anda bisa...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Datang dan ambil...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "Tertarik?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Sudah dalam perjalanan.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Di mana kamu?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "Aku sudah terbakar oleh rasa tidak sabar!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Hampir sampai.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "Oh, sial!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "Apakah kamu baik-baik saja?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Kamu bisa mendengarku?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>radio</color>] «Ini tepat tengah malam di jam. Kamu sedang mendengarkan Radio Jalan Bebas...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>radio</color>] «Jangan kehilangan <color={HEX_MAGENTA}>kecepatan</color>»";
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>radio</color>] «Lagi satu menyalip. Lagi satu <color={HEX_MAGENTA}>gadis</color>»";
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>radio</color>] «Malam — waktu untuk <color={HEX_MAGENTA}>mempercepat</color>»";
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>radio</color>] «Semakin <color={HEX_MAGENTA}>cepat</color> — semakin baik»";
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>radio</color>] «Pacu <color={HEX_MAGENTA}>jantung</color> Anda!»";
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>radio</color>] «Sekali <color={HEX_MAGENTA}>gas</color>, tak bisa berhenti <color={HEX_CYAN}>lagi</color>»";
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>radio</color>] «Kamu masih <color={HEX_CYAN}>hidup</color>!»";
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>radio</color>] «Kamu punya kebiasaan bermain dengan <color={HEX_MAGENTA}>kematian</color>»";
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>radio/color>] «<color={HEX_MAGENTA}>Dia</color> akan segera menemukan pengganti»";
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_CYAN}>Hidup</color> cepat, <color={HEX_MAGENTA}>mati</color> muda»";
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>radio</color>] «Apa itu <color={HEX_CYAN}>rem</color>?»";
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>radio</color>] «<color={HEX_MAGENTA}>Adrenalin</color> — bahan bakar terbaik»";
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>radio</color>] «Kecepatan akan <color={HEX_CYAN}>membebaskanmu</color>»";
    }
}
