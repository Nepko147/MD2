using UnityEngine;
using static ControlPers_LanguageHandler_Entity;

public class ControlPers_LanguageHandler_Turkish : ControlPers_LanguageHandler_Parent
{
    protected override void Awake()
    {
        base.Awake();

        text_keyToString[Text_Key.startText_desktop] = "HERHANGI BIR TUŞA BASIN"; //Press any key
        text_keyToString[Text_Key.startText_mobile] = "EKRANA DOKUNUN"; //Tap on the screen
        text_keyToString[Text_Key.loadingCloudData] = "BULUTTAN VERI YÜKLEME"; //Loading data from cloud
        text_keyToString[Text_Key.upgrade_moreCoins] = "DAHA FAZLA MADENI PARA"; //More coins
        text_keyToString[Text_Key.upgrade_moreBonuses] = "DAHA FAZLA BONUS"; //More bonuses
        text_keyToString[Text_Key.upgrade_coinMagnet] = "MADENI PARA MIKNATISI"; //Coin magnet
        text_keyToString[Text_Key.upgrade_heDidNotDie] = "O ÖLMEDI"; //He didn't die
        text_keyToString[Text_Key.popUpMessage_notEnoughCoins] = "YETERLI PARA YOK"; //Not enough coins
        text_keyToString[Text_Key.tutorial_desktop] = "Hareket etmek için sol fare tuşunu basılı tutun, ardından sanal çubuğu hareket yönünde çekin.";
        text_keyToString[Text_Key.tutorial_mobile] = "Hareket etmek için parmağınızı ekrana basılı tutun, ardından sanal çubuğu hareket yönünde çekin.";
        text_keyToString[Text_Key.indicators_complete] = "KALAN KILOMETRE"; //Kilometers left
        text_keyToString[Text_Key.midscreen_gameOver] = "OYUN BITTI"; //Game over
        text_keyToString[Text_Key.midscreen_pause] = "DURAKLAT"; //Pause
        text_keyToString[Text_Key.midscreen_distanceRemain] = "Mesafe kalır";
        text_keyToString[Text_Key.received_text] = "Alınan paralar";
        text_keyToString[Text_Key.received_ad_text] = "Reklamları izlemek için x" + ControlPers_BuildSettings.PLATFORMTYPE_WEB_YANDEXGAMES_AD_MULT + " kazanın"; //Get x3 for watching ads
        text_keyToString[Text_Key.popUp_up] = "+1 Hayat"; //+1 Life
        text_keyToString[Text_Key.popUp_coin] = "+1 Madeni Para";
        text_keyToString[Text_Key.popUp_coinRush] = "Paraya Hücum!";
        text_keyToString[Text_Key.gameBy] = "Bir oyun tarafından LUNAR HOWL"; //A game by LUNAR HOWL
        text_keyToString[Text_Key.statistics_reviveNumber] = "DIRILIŞ SAYISI"; //Number of resurrections
        text_keyToString[Text_Key.statistics_coinsTotal] = "TOPLAM MADENI PARALAR"; //Coins total
        text_keyToString[Text_Key.statistics_coinsSpentOnRevivals] = "DIRILIŞLERE HARCANAN PARALAR"; //Coins spent on resurrections
        text_keyToString[Text_Key.statistics_defeats] = "YENILGILER"; //Defeats
        text_keyToString[Text_Key.statistics_totalDrivings] = "TOPLAM YOLCULUK SAYISI"; //Total rides
        text_keyToString[Text_Key.statistics_best] = "EN IYI SONUÇ"; //Best result
        text_keyToString[Text_Key.statistics_newRecord] = "Yeni rekor!";
        text_keyToString[Text_Key.statistics_gameCompleted] = "Oyun tamamlandı";
        text_keyToString[Text_Key.dialogue_start_string_1] = "Merhaba.";
        text_keyToString[Text_Key.dialogue_start_string_2] = "Hey, yakışıklı.";
        text_keyToString[Text_Key.dialogue_start_string_3] = "İndirimde fazladan bir kutu kürdanım var. Sadece...";
        text_keyToString[Text_Key.dialogue_start_string_4] = "Gel al...";
        text_keyToString[Text_Key.dialogue_start_string_5] = "İlginizi çekti mi?";
        text_keyToString[Text_Key.dialogue_start_string_6] = "Zaten araba kullanıyorum.";
        text_keyToString[Text_Key.dialogue_end_string_1] = "Neredesin?";
        text_keyToString[Text_Key.dialogue_end_string_2] = "Zaten sabırsızlıktan kavruluyorum!";
        text_keyToString[Text_Key.dialogue_end_string_3] = "Neredeyse geldi.";
        text_keyToString[Text_Key.dialogue_end_string_4] = "Aman Tanrım!";
        text_keyToString[Text_Key.dialogue_end_string_5] = "İyi misin?";
        text_keyToString[Text_Key.dialogue_end_string_6] = "…";
        text_keyToString[Text_Key.dialogue_end_string_7] = "Beni duyabiliyor musun?";
        text_keyToString[Text_Key.radio_string_early_1] = $"[<color={HEX_CYAN}>radyo</color>] «Saat tam gece yarısı. Free Road Radyosu'ndasınız...»";
        text_keyToString[Text_Key.radio_string_early_2] = $"[<color={HEX_CYAN}>radyo</color>] «Hızınızı <color={HEX_MAGENTA}>kaybetmeyin</color>»"; //Don't lose speed
        text_keyToString[Text_Key.radio_string_early_3] = $"[<color={HEX_CYAN}>radyo</color>] «Bir sollama daha. Bir <color={HEX_MAGENTA}>kız</color> arkadaş daha»"; //Another overtaking. Another girlfriend
        text_keyToString[Text_Key.radio_string_early_4] = $"[<color={HEX_CYAN}>radyo</color>] «Gece — <color={HEX_MAGENTA}>hızlanma</color> zamanı»"; //Night — time to accelerate
        text_keyToString[Text_Key.radio_string_early_5] = $"[<color={HEX_CYAN}>radyo</color>] «Ne kadar <color={HEX_MAGENTA}>hızlı</color> olursa o kadar iyi»"; //The faster — the better
        text_keyToString[Text_Key.radio_string_early_6] = $"[<color={HEX_CYAN}>radyo</color>] «<color={HEX_MAGENTA}>Kalbinizi</color> harekete geçirin!»";
        text_keyToString[Text_Key.radio_string_early_7] = $"[<color={HEX_CYAN}>radyo</color>] «Bir kere <color={HEX_MAGENTA}>gaza</color> bastığınızda, <color={HEX_CYAN}>duramazsınız</color>»"; //Once you rev up, you can't stop
        text_keyToString[Text_Key.radio_string_late_1] = $"[<color={HEX_MAGENTA}>radyo</color>] «Sen hala <color={HEX_CYAN}>hayattasın</color>!»"; //You're still alive!
        text_keyToString[Text_Key.radio_string_late_2] = $"[<color={HEX_MAGENTA}>radyo</color>] «<color={HEX_MAGENTA}>Ölümle</color>» oynama alışkanlığın var»"; //You have a habit of playing with death
        text_keyToString[Text_Key.radio_string_late_3] = $"[<color={HEX_MAGENTA}>radyo</color>] «Hemen bir <color={HEX_MAGENTA}>yedek</color> bulacaktır»"; //She'll quickly find a replacement
        text_keyToString[Text_Key.radio_string_late_4] = $"[<color={HEX_MAGENTA}>radyo</color>] «Hızlı <color={HEX_CYAN}>yaşa</color>, hızlı <color={HEX_MAGENTA}>öl</color>»"; //Live fast,die fast
        text_keyToString[Text_Key.radio_string_late_5] = $"[<color={HEX_MAGENTA}>radyo</color>] «<color={HEX_CYAN}>Frenler</color> nedir?»"; //What are brakes?
        text_keyToString[Text_Key.radio_string_late_6] = $"[<color={HEX_MAGENTA}>radyo</color>] «<color={HEX_MAGENTA}>Adrenalin</color> - en iyi yakıt»"; //Adrenaline — the best fuel
        text_keyToString[Text_Key.radio_string_late_7] = $"[<color={HEX_MAGENTA}>radyo</color>] «Hız sizi <color={HEX_MAGENTA}>özgürleştirecek</color>»"; //Speed will set you free
    }
}
