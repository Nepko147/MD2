using UnityEngine;
using System;
using System.IO;
using System.Xml;
using YG;
using YG.Insides;

public class ControlPers_DataHandler : MonoBehaviour
{
    #region General

    public static ControlPers_DataHandler SingleOnScene { get; private set; }

    public bool IsDataLoaded { get; private set; }

    private const string DIRECTORY_NAME = "Midnight Drive";
    private string directory_path;
    private void Directory_CreateIfNotExists(string _path)
    {
        if (!Directory.Exists(_path)) //Проверяем наличие папки
        {
            Directory.CreateDirectory(_path); //Создаём папку
        }
    }

    #endregion

    #region Progress Data

    private struct ProgressData
    {
        public const string ORIGINNODE = "Progress";

        public struct Coins
        {
            public const string NODE = "Coins";
            public const string PATH = ORIGINNODE + "/" + NODE;
            public const int DEFAULTVALUE = 0;
            public static int value = DEFAULTVALUE;
        }

        public struct Upgrades
        {
            public const string NODE = "Upgrades";

            public struct MoreCoins
            {
                public const string NODE = "MoreCoins";
                public const string PATH = ORIGINNODE + "/" + Upgrades.NODE + "/" + NODE;
                public const ProgressData_Upgrades_State DEFAULTVALUE = ProgressData_Upgrades_State.buy;
                public static ProgressData_Upgrades_State value = DEFAULTVALUE;
            }

            public struct MoreBonuses
            {
                public const string NODE = "MoreBonuses";
                public const string PATH = ORIGINNODE + "/" + Upgrades.NODE + "/" + NODE;
                public const ProgressData_Upgrades_State DEFAULTVALUE = ProgressData_Upgrades_State.buy;
                public static ProgressData_Upgrades_State value = DEFAULTVALUE;
            }

            public struct CoinMagnet
            {
                public const string NODE = "CoinMagnet";
                public const string PATH = ORIGINNODE + "/" + Upgrades.NODE + "/" + NODE;
                public const ProgressData_Upgrades_State DEFAULTVALUE = ProgressData_Upgrades_State.buy;
                public static ProgressData_Upgrades_State value = DEFAULTVALUE;
            }

            public struct Revive
            {
                public const string NODE = "Revive";
                public const string PATH = ORIGINNODE + "/" + Upgrades.NODE + "/" + NODE;
                public const ProgressData_Upgrades_State DEFAULTVALUE = ProgressData_Upgrades_State.buy;
                public static ProgressData_Upgrades_State value = DEFAULTVALUE;
            }
        }
    }

    public enum ProgressData_Upgrades_State
    {
        buy,
        improve,
        received
    }

    private XmlDocument progressData_file;
    private const string PROGRESSDATA_FILE_NAME = "Progress.xml";
    private string progressData_file_path;

    public void ProgressData_Load()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.currentPlatformType)
        {
            case ControlPers_BuildSettings.CurrentPlatformType.windows:
                progressData_file = new XmlDocument();
                progressData_file_path = directory_path + PROGRESSDATA_FILE_NAME;

                if (!File.Exists(progressData_file_path))
                {
                    var _originNode = progressData_file.CreateElement(ProgressData.ORIGINNODE); //Создаем исходный раздел для XML документа
                    progressData_file.AppendChild(_originNode); //Записываем исходный раздел в XML документ

                    var _node_coins = progressData_file.CreateElement(ProgressData.Coins.NODE); //Создаем раздел Coins для XML документа
                    _node_coins.InnerText = ProgressData.Coins.value.ToString(); //Записываем значение в раздел Coins
                    _originNode.AppendChild(_node_coins); //Записываем раздел Coins в исходный раздел

                    var _node_upgrades = progressData_file.CreateElement(ProgressData.Upgrades.NODE); //Создаем раздел Upgrades для XML документа
                    _originNode.AppendChild(_node_upgrades); //Записываем раздел Upgrades в исходный раздел

                    var _node_moreCoins = progressData_file.CreateElement(ProgressData.Upgrades.MoreCoins.NODE); //Создаем раздел MoreCoins для XML документа
                    _node_moreCoins.InnerText = ProgressData.Upgrades.MoreCoins.DEFAULTVALUE.ToString(); //Записываем значение в раздел MoreCoins
                    _node_upgrades.AppendChild(_node_moreCoins); //Записываем раздел MoreCoins в раздел Upgrades

                    var _node_moreBonuses = progressData_file.CreateElement(ProgressData.Upgrades.MoreBonuses.NODE); //Создаем раздел MoreBonuses для XML документа
                    _node_moreBonuses.InnerText = ProgressData.Upgrades.MoreBonuses.DEFAULTVALUE.ToString(); //Записываем значение в раздел MoreBonuses
                    _node_upgrades.AppendChild(_node_moreBonuses); //Записываем раздел MoreBonuses в раздел Upgrades

                    var _node_coinMagnet = progressData_file.CreateElement(ProgressData.Upgrades.CoinMagnet.NODE); //Создаем раздел CoinMagnet для XML документа
                    _node_coinMagnet.InnerText = ProgressData.Upgrades.CoinMagnet.DEFAULTVALUE.ToString(); //Записываем значение в раздел CoinMagnet
                    _node_upgrades.AppendChild(_node_coinMagnet); //Записываем раздел CoinMagnet в раздел Upgrades

                    var _node_revive = progressData_file.CreateElement(ProgressData.Upgrades.Revive.NODE); //Создаем раздел Revive для XML документа
                    _node_revive.InnerText = ProgressData.Upgrades.Revive.DEFAULTVALUE.ToString(); //Записываем значение в раздел Revive
                    _node_upgrades.AppendChild(_node_revive); //Записываем раздел Revive в раздел Upgrades

                    progressData_file.Save(progressData_file_path); //Создаем файл XML документа
                }
                else
                {
                    progressData_file.Load(progressData_file_path);

                    var _coins_text = progressData_file.SelectSingleNode(ProgressData.Coins.PATH).InnerText;
                    ProgressData.Coins.value = int.Parse(_coins_text);

                    var _upgrades_moreCoins_text = progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreCoins.PATH).InnerText;
                    Enum.TryParse(_upgrades_moreCoins_text, out ProgressData.Upgrades.MoreCoins.value);

                    var _upgrades_moreBonuses_text = progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreBonuses.PATH).InnerText;
                    Enum.TryParse(_upgrades_moreBonuses_text, out ProgressData.Upgrades.MoreBonuses.value);

                    var _upgrades_coinMagnet_text = progressData_file.SelectSingleNode(ProgressData.Upgrades.CoinMagnet.PATH).InnerText;
                    Enum.TryParse(_upgrades_coinMagnet_text, out ProgressData.Upgrades.CoinMagnet.value);

                    var _upgrades_revive_text = progressData_file.SelectSingleNode(ProgressData.Upgrades.Revive.PATH).InnerText;
                    Enum.TryParse(_upgrades_revive_text, out ProgressData.Upgrades.Revive.value);
                }
            break;

            case ControlPers_BuildSettings.CurrentPlatformType.web_yandexGames_desktop:
            case ControlPers_BuildSettings.CurrentPlatformType.web_yandexGames_mobile_android:
                ProgressData.Coins.value = YG2.saves.ProgressData_Coins;
                ProgressData.Upgrades.MoreCoins.value = YG2.saves.ProgressData_Upgrades_MoreCoins;
                ProgressData.Upgrades.MoreBonuses.value = YG2.saves.ProgressData_Upgrades_MoreBonuses;
                ProgressData.Upgrades.CoinMagnet.value = YG2.saves.ProgressData_Upgrades_CoinMagnet;
                ProgressData.Upgrades.Revive.value = YG2.saves.ProgressData_Upgrades_Revive;
            break;
        }
    }

    public void ProgressData_Save()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.currentPlatformType)
        {
            case ControlPers_BuildSettings.CurrentPlatformType.windows:
                Directory_CreateIfNotExists(directory_path);

                progressData_file.SelectSingleNode(ProgressData.Coins.PATH).InnerText = ProgressData.Coins.value.ToString(); //Перезаписываем значение в разделе Coins
                progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreCoins.PATH).InnerText = ProgressData.Upgrades.MoreCoins.value.ToString(); //Перезаписываем значение в разделе Upgrades/MoreCoins
                progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreBonuses.PATH).InnerText = ProgressData.Upgrades.MoreBonuses.value.ToString(); //Перезаписываем значение в разделе Upgrades/MoreBonuses
                progressData_file.SelectSingleNode(ProgressData.Upgrades.CoinMagnet.PATH).InnerText = ProgressData.Upgrades.CoinMagnet.value.ToString(); //Перезаписываем значение в разделе Upgrades/CoinMagnet
                progressData_file.SelectSingleNode(ProgressData.Upgrades.Revive.PATH).InnerText = ProgressData.Upgrades.Revive.value.ToString(); //Перезаписываем значение в разделе Upgrades/Revive

                progressData_file.Save(progressData_file_path); //Создаем или перезаписываем файл XML документа
            break;

            case ControlPers_BuildSettings.CurrentPlatformType.web_yandexGames_desktop:
            case ControlPers_BuildSettings.CurrentPlatformType.web_yandexGames_mobile_android:
                YG2.saves.ProgressData_Coins = ProgressData.Coins.value;
                YG2.saves.ProgressData_Upgrades_MoreCoins = ProgressData.Upgrades.MoreCoins.value;
                YG2.saves.ProgressData_Upgrades_MoreBonuses = ProgressData.Upgrades.MoreBonuses.value;
                YG2.saves.ProgressData_Upgrades_CoinMagnet = ProgressData.Upgrades.CoinMagnet.value;
                YG2.saves.ProgressData_Upgrades_Revive = ProgressData.Upgrades.Revive.value;
                
                YG2.SaveProgress();
            break;
        }
    }

    public int ProgressData_Coins
    {
        get 
        { 
            return (ProgressData.Coins.value); 
        }
        set 
        { 
            ProgressData.Coins.value = value;
            ProgressData_Coins_OnChange?.Invoke(value);
        }
    }
    public delegate void ProgressData_Coins_Change(int _changedCoinsValue);
    public event ProgressData_Coins_Change ProgressData_Coins_OnChange;

    private bool ProgressData_Upgade_isBought(ProgressData_Upgrades_State _upgrade)
    {
        switch (_upgrade)
        {
            case ProgressData_Upgrades_State.improve:
            case ProgressData_Upgrades_State.received:
            return (true);

            default: 
            return (false);
        }
    }

    private bool ProgressData_Upgade_isImproved(ProgressData_Upgrades_State _upgrade)
    {
        switch (_upgrade)
        {
            case ProgressData_Upgrades_State.received:
            return (true);

            default:
            return (false);
        }
    }

    public void ProgressData_Upgrade_MoreCoins_Buy()
    {
        ProgressData.Upgrades.MoreCoins.value = ProgressData_Upgrades_State.improve;
    }
    public bool ProgressData_Upgrade_MoreCoins_IsBought()
    {
        return (ProgressData_Upgade_isBought(ProgressData.Upgrades.MoreCoins.value));
    }
    public void ProgressData_Upgrade_MoreCoins_Improve()
    {
        ProgressData.Upgrades.MoreCoins.value = ProgressData_Upgrades_State.received;
    }
    public bool ProgressData_Upgrade_MoreCoins_IsImproved()
    {
        return (ProgressData_Upgade_isImproved(ProgressData.Upgrades.MoreCoins.value));
    }

    public void ProgressData_Upgrade_MoreBonuses_Buy()
    {
        ProgressData.Upgrades.MoreBonuses.value = ProgressData_Upgrades_State.improve;
    }
    public bool ProgressData_Upgrade_MoreBonuses_IsBought()
    {
        return (ProgressData_Upgade_isBought(ProgressData.Upgrades.MoreBonuses.value));
    }
    public void ProgressData_Upgrade_MoreBonuses_Improve()
    {
        ProgressData.Upgrades.MoreBonuses.value = ProgressData_Upgrades_State.received;
    }
    public bool ProgressData_Upgrade_MoreBonuses_IsImproved()
    {
        return (ProgressData_Upgade_isImproved(ProgressData.Upgrades.MoreBonuses.value));
    }

    public void ProgressData_Upgrade_CoinMagnet_Buy()
    {
        ProgressData.Upgrades.CoinMagnet.value = ProgressData_Upgrades_State.improve;
    }
    public bool ProgressData_Upgrade_CoinMagnet_IsBought()
    {
        return (ProgressData_Upgade_isBought(ProgressData.Upgrades.CoinMagnet.value));
    }
    public void ProgressData_Upgrade_CoinMagnet_Improve()
    {
        ProgressData.Upgrades.CoinMagnet.value = ProgressData_Upgrades_State.received;
    }
    public bool ProgressData_Upgrade_CoinMagnet_IsImproved()
    {
        return (ProgressData_Upgade_isImproved(ProgressData.Upgrades.CoinMagnet.value));
    }

    public void ProgressData_Upgrade_Revive_Buy()
    {
        ProgressData.Upgrades.Revive.value = ProgressData_Upgrades_State.improve;
    }
    public bool ProgressData_Upgrade_Revive_IsBought()
    {
        return (ProgressData_Upgade_isBought(ProgressData.Upgrades.Revive.value));
    }
    public void ProgressData_Upgrade_Revive_Improve()
    {
        ProgressData.Upgrades.Revive.value = ProgressData_Upgrades_State.received;
    }
    public bool ProgressData_Upgrade_Revive_IsImproved()
    {
        return (ProgressData_Upgade_isImproved(ProgressData.Upgrades.Revive.value));
    }

    #endregion

    #region Settings Data

    private struct SettingsData
    {
        public const string ORIGINNODE = "Settings";

        public struct Audio
        {
            public const string NODE = "Audio";

            public struct Sound
            {
                public const string NODE = "Sound";
                public const string PATH = ORIGINNODE + "/" + Audio.NODE + "/" + NODE;
                public const float DEFAULTVALUE = 0.5f;
                public static float value = DEFAULTVALUE;
            }

            public struct Music
            {
                public const string NODE = "Music";
                public const string PATH = ORIGINNODE + "/" + Audio.NODE + "/" + NODE;
                public const float DEFAULTVALUE = 0.5f;
                public static float value = DEFAULTVALUE;
            }
        }
    }

    private XmlDocument settingsData_file;
    private const string SETTINGSDATA_FILE_NAME = "Settings.xml";
    private string settingsData_file_path;

    public const float SETTINGSDATA_AUDIO_SOUND_DEFAULTVALUE = SettingsData.Audio.Sound.DEFAULTVALUE;
    public const float SETTINGSDATA_AUDIO_MUSIC_DEFAULTVALUE = SettingsData.Audio.Music.DEFAULTVALUE;

    public void SettingsData_Load()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.currentPlatformType)
        {
            case ControlPers_BuildSettings.CurrentPlatformType.windows:
                settingsData_file = new XmlDocument();
                settingsData_file_path = directory_path + SETTINGSDATA_FILE_NAME;

                if (!File.Exists(settingsData_file_path))
                {
                    var _originNode = settingsData_file.CreateElement(SettingsData.ORIGINNODE); //Создаем раздел исходный раздел для XML документа
                    settingsData_file.AppendChild(_originNode); //Записываем раздел исходный раздел в XML документ

                    var _node_audio = settingsData_file.CreateElement(SettingsData.Audio.NODE); //Создаем раздел Audio для XML документа
                    _originNode.AppendChild(_node_audio); //Записываем раздел Audio в исходный раздел

                    var _node_sound = settingsData_file.CreateElement(SettingsData.Audio.Sound.NODE); //Создаем раздел Sound для XML документа
                    _node_sound.InnerText = SettingsData.Audio.Sound.DEFAULTVALUE.ToString(); //Записываем значение в раздел Sound
                    _node_audio.AppendChild(_node_sound); //Записываем раздел Sound в раздел Audio

                    var _node_musicValue = settingsData_file.CreateElement(SettingsData.Audio.Music.NODE); //Создаем раздел MusicValue для XML документа
                    _node_musicValue.InnerText = SettingsData.Audio.Music.DEFAULTVALUE.ToString(); //Записываем значение в раздел Music
                    _node_audio.AppendChild(_node_musicValue); //Записываем раздел Music в раздел Audio

                    settingsData_file.Save(settingsData_file_path); //Создаем файл XML документа
                }
                else
                {
                    settingsData_file.Load(settingsData_file_path);

                    var _audio_soundValue_text = settingsData_file.SelectSingleNode(SettingsData.Audio.Sound.PATH).InnerText;
                    SettingsData.Audio.Sound.value = float.Parse(_audio_soundValue_text);

                    var _audio_musicValue_text = settingsData_file.SelectSingleNode(SettingsData.Audio.Music.PATH).InnerText;
                    SettingsData.Audio.Music.value = float.Parse(_audio_musicValue_text);
                }
            break;

            case ControlPers_BuildSettings.CurrentPlatformType.web_yandexGames_desktop:
            case ControlPers_BuildSettings.CurrentPlatformType.web_yandexGames_mobile_android:
                SettingsData.Audio.Sound.value = YG2.saves.SettingsData_Audio_Sound;
                SettingsData.Audio.Music.value = YG2.saves.SettingsData_Audio_Music;
            break;
        }
    }

    public void SettingsData_Save()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.currentPlatformType)
        {
            case ControlPers_BuildSettings.CurrentPlatformType.windows:
                Directory_CreateIfNotExists(directory_path);

                settingsData_file.SelectSingleNode(SettingsData.Audio.Sound.PATH).InnerText = SettingsData.Audio.Sound.value.ToString(); //Перезаписываем значение в разделе Audio/Sound
                settingsData_file.SelectSingleNode(SettingsData.Audio.Music.PATH).InnerText = SettingsData.Audio.Music.value.ToString(); //Перезаписываем значение в разделе Audio/Music

                settingsData_file.Save(settingsData_file_path); //Создаем или перезаписываем файл XML документа
            break;

            case ControlPers_BuildSettings.CurrentPlatformType.web_yandexGames_desktop:
            case ControlPers_BuildSettings.CurrentPlatformType.web_yandexGames_mobile_android:
                YG2.saves.SettingsData_Audio_Sound = SettingsData.Audio.Sound.value;
                YG2.saves.SettingsData_Audio_Music = SettingsData.Audio.Music.value;

                YG2.SaveProgress();
            break;
        }
    }

    public float SettingsData_SoundValue
    {
        get { return (SettingsData.Audio.Sound.value); }
        set { SettingsData.Audio.Sound.value = Mathf.Clamp(value, 0, 1f); }
    }

    public float SettingsData_MusicValue
    {
        get { return (SettingsData.Audio.Music.value); }
        set { SettingsData.Audio.Music.value = Mathf.Clamp(value, 0, 1f); }
    }

    #endregion

    private void Awake()
    {
        SingleOnScene = this;

        IsDataLoaded = false;
    }

    private void Start()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.currentPlatformType)
        {
            case ControlPers_BuildSettings.CurrentPlatformType.windows:
                directory_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + DIRECTORY_NAME + @"\";
                Directory_CreateIfNotExists(directory_path);

                ProgressData_Load();
                SettingsData_Load();

                IsDataLoaded = true;
            break;

            case ControlPers_BuildSettings.CurrentPlatformType.web_yandexGames_desktop:
            case ControlPers_BuildSettings.CurrentPlatformType.web_yandexGames_mobile_android:
                YG2.onGetSDKData += () =>
                {
                    ProgressData_Load();
                    SettingsData_Load();

                    IsDataLoaded = true;
                };

                YGInsides.LoadProgress();
            break;
        }
    }
}
