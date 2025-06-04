using UnityEngine;
using System.IO;
using System.Xml;
using System;

public class ControlPers_DataHandler : MonoBehaviour
{
    #region General

    public static ControlPers_DataHandler SingleOnScene { get; private set; }

    private const string DIRECTORY_NAME = "Midnight Drive";
    private string directory_path;
    private void Directory_CreateIfNotExists(string _path)
    {
        if (!Directory.Exists(_path)) //��������� ������� �����
        {
            Directory.CreateDirectory(_path); //������ �����
        }
    }

    #endregion

    #region Progress Data

    private const string PROGRESSDATA_FILE_NAME = "Progress.xml";
    private string progressData_file_path;

    private struct ProgressData
    {
        public const string ORIGINNODE = "Progress";

        public struct Coins
        {
            public const string NODE = "Coins";
            public const string PATH = ORIGINNODE + "/" + NODE;
            public const int DEFAULTVALUE = 0;
            public static int value;
        }

        public struct Upgrades
        {
            public const string NODE = "Upgrades";
            public enum State
            {
                buy,
                improve,
                received
            }

            public struct MoreCoins
            {
                public const string NODE = "MoreCoins";
                public const string PATH = ORIGINNODE + "/" + Upgrades.NODE + "/" + NODE;
                public const ProgressData.Upgrades.State DEFAULTVALUE = ProgressData.Upgrades.State.buy;
                public static State value;
            }

            public struct MoreBonuses
            {
                public const string NODE = "MoreBonuses";
                public const string PATH = ORIGINNODE + "/" + Upgrades.NODE + "/" + NODE;
                public const ProgressData.Upgrades.State DEFAULTVALUE = ProgressData.Upgrades.State.buy;
                public static State value;
            }

            public struct CoinMagnet
            {
                public const string NODE = "CoinMagnet";
                public const string PATH = ORIGINNODE + "/" + Upgrades.NODE + "/" + NODE;
                public const ProgressData.Upgrades.State DEFAULTVALUE = ProgressData.Upgrades.State.buy;
                public static State value;
            }

            public struct Revive
            {
                public const string NODE = "Revive";
                public const string PATH = ORIGINNODE + "/" + Upgrades.NODE + "/" + NODE;
                public const ProgressData.Upgrades.State DEFAULTVALUE = ProgressData.Upgrades.State.buy;
                public static State value;
            }
        }
    }

    private XmlDocument ProgressData_Load()
    {
        var _xmlDoc_progressData = new XmlDocument();

        if (!File.Exists(progressData_file_path))
        {
            var _originNode = _xmlDoc_progressData.CreateElement(ProgressData.ORIGINNODE); //������� �������� ������ ��� XML ���������
            _xmlDoc_progressData.AppendChild(_originNode); //���������� �������� ������ � XML ��������

            var _node_coins = _xmlDoc_progressData.CreateElement(ProgressData.Coins.NODE); //������� ������ Coins ��� XML ���������
            _node_coins.InnerText = ProgressData.Coins.value.ToString(); //���������� �������� � ������ Coins
            _originNode.AppendChild(_node_coins); //���������� ������ Coins � �������� ������

            var _node_upgrades = _xmlDoc_progressData.CreateElement(ProgressData.Upgrades.NODE); //������� ������ Upgrades ��� XML ���������
            _originNode.AppendChild(_node_upgrades); //���������� ������ Upgrades � �������� ������

            var _node_moreCoins = _xmlDoc_progressData.CreateElement(ProgressData.Upgrades.MoreCoins.NODE); //������� ������ MoreCoins ��� XML ���������
            _node_moreCoins.InnerText = ProgressData.Upgrades.MoreCoins.DEFAULTVALUE.ToString(); //���������� �������� � ������ MoreCoins
            _node_upgrades.AppendChild(_node_moreCoins); //���������� ������ MoreCoins � ������ Upgrades

            var _node_moreBonuses = _xmlDoc_progressData.CreateElement(ProgressData.Upgrades.MoreBonuses.NODE); //������� ������ MoreBonuses ��� XML ���������
            _node_moreBonuses.InnerText = ProgressData.Upgrades.MoreBonuses.DEFAULTVALUE.ToString(); //���������� �������� � ������ MoreBonuses
            _node_upgrades.AppendChild(_node_moreBonuses); //���������� ������ MoreBonuses � ������ Upgrades

            var _node_coinMagnet = _xmlDoc_progressData.CreateElement(ProgressData.Upgrades.CoinMagnet.NODE); //������� ������ CoinMagnet ��� XML ���������
            _node_coinMagnet.InnerText = ProgressData.Upgrades.CoinMagnet.DEFAULTVALUE.ToString(); //���������� �������� � ������ CoinMagnet
            _node_upgrades.AppendChild(_node_coinMagnet); //���������� ������ CoinMagnet � ������ Upgrades

            var _node_revive = _xmlDoc_progressData.CreateElement(ProgressData.Upgrades.Revive.NODE); //������� ������ Revive ��� XML ���������
            _node_revive.InnerText = ProgressData.Upgrades.Revive.DEFAULTVALUE.ToString(); //���������� �������� � ������ Revive
            _node_upgrades.AppendChild(_node_revive); //���������� ������ Revive � ������ Upgrades

            _xmlDoc_progressData.Save(progressData_file_path); //������� ��� �������������� XML ��������
        }
        else
        {
            _xmlDoc_progressData.Load(progressData_file_path);
        }

        return (_xmlDoc_progressData);
    }

    public void ProgressData_Save()
    {
        Directory_CreateIfNotExists(directory_path);

        var _xmlDoc_progressData = ProgressData_Load();
        _xmlDoc_progressData.SelectSingleNode(ProgressData.Coins.PATH).InnerText = ProgressData.Coins.value.ToString(); //�������������� �������� � ������� Coins
        _xmlDoc_progressData.SelectSingleNode(ProgressData.Upgrades.MoreCoins.PATH).InnerText = ProgressData.Upgrades.MoreCoins.value.ToString(); //�������������� �������� � ������� Upgrades/MoreCoins
        _xmlDoc_progressData.SelectSingleNode(ProgressData.Upgrades.MoreBonuses.PATH).InnerText = ProgressData.Upgrades.MoreBonuses.value.ToString(); //�������������� �������� � ������� Upgrades/MoreBonuses
        _xmlDoc_progressData.SelectSingleNode(ProgressData.Upgrades.CoinMagnet.PATH).InnerText = ProgressData.Upgrades.CoinMagnet.value.ToString(); //�������������� �������� � ������� Upgrades/CoinMagnet
        _xmlDoc_progressData.SelectSingleNode(ProgressData.Upgrades.Revive.PATH).InnerText = ProgressData.Upgrades.Revive.value.ToString(); //�������������� �������� � ������� Upgrades/Revive
        _xmlDoc_progressData.Save(progressData_file_path); //�������������� XML ��������
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

    private bool ProgressData_Upgade_isBought(ProgressData.Upgrades.State _upgrade)
    {
        switch (_upgrade)
        {
            case ProgressData.Upgrades.State.improve:
            case ProgressData.Upgrades.State.received:
            return (true);

            default: 
            return (false);
        }
    }

    private bool ProgressData_Upgade_isImproved(ProgressData.Upgrades.State _upgrade)
    {
        switch (_upgrade)
        {
            case ProgressData.Upgrades.State.received:
            return (true);

            default:
            return (false);
        }
    }

    public void ProgressData_Upgrade_MoreCoins_Buy()
    {
        ProgressData.Upgrades.MoreCoins.value = ProgressData.Upgrades.State.improve;
    }
    public bool ProgressData_Upgrade_MoreCoins_IsBought()
    {
        return (ProgressData_Upgade_isBought(ProgressData.Upgrades.MoreCoins.value));
    }
    public void ProgressData_Upgrade_MoreCoins_Improve()
    {
        ProgressData.Upgrades.MoreCoins.value = ProgressData.Upgrades.State.received;
    }
    public bool ProgressData_Upgrade_MoreCoins_IsImproved()
    {
        return (ProgressData_Upgade_isImproved(ProgressData.Upgrades.MoreCoins.value));
    }

    public void ProgressData_Upgrade_MoreBonuses_Buy()
    {
        ProgressData.Upgrades.MoreBonuses.value = ProgressData.Upgrades.State.improve;
    }
    public bool ProgressData_Upgrade_MoreBonuses_IsBought()
    {
        return (ProgressData_Upgade_isBought(ProgressData.Upgrades.MoreBonuses.value));
    }
    public void ProgressData_Upgrade_MoreBonuses_Improve()
    {
        ProgressData.Upgrades.MoreBonuses.value = ProgressData.Upgrades.State.received;
    }
    public bool ProgressData_Upgrade_MoreBonuses_IsImproved()
    {
        return (ProgressData_Upgade_isImproved(ProgressData.Upgrades.MoreBonuses.value));
    }

    public void ProgressData_Upgrade_CoinMagnet_Buy()
    {
        ProgressData.Upgrades.CoinMagnet.value = ProgressData.Upgrades.State.improve;
    }
    public bool ProgressData_Upgrade_CoinMagnet_IsBought()
    {
        return (ProgressData_Upgade_isBought(ProgressData.Upgrades.CoinMagnet.value));
    }
    public void ProgressData_Upgrade_CoinMagnet_Improve()
    {
        ProgressData.Upgrades.CoinMagnet.value = ProgressData.Upgrades.State.received;
    }
    public bool ProgressData_Upgrade_CoinMagnet_IsImproved()
    {
        return (ProgressData_Upgade_isImproved(ProgressData.Upgrades.CoinMagnet.value));
    }

    public void ProgressData_Upgrade_Revive_Buy()
    {
        ProgressData.Upgrades.Revive.value = ProgressData.Upgrades.State.improve;
    }
    public bool ProgressData_Upgrade_Revive_IsBought()
    {
        return (ProgressData_Upgade_isBought(ProgressData.Upgrades.Revive.value));
    }
    public void ProgressData_Upgrade_Revive_Improve()
    {
        ProgressData.Upgrades.Revive.value = ProgressData.Upgrades.State.received;
    }
    public bool ProgressData_Upgrade_Revive_IsImproved()
    {
        return (ProgressData_Upgade_isImproved(ProgressData.Upgrades.Revive.value));
    }

    #endregion

    #region Settings Data

    private const string SETTINGSDATA_FILE_NAME = "Settings.xml";
    private string settingsData_file_path;

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
                public static float value;
            }

            public struct Music
            {
                public const string NODE = "Music";
                public const string PATH = ORIGINNODE + "/" + Audio.NODE + "/" + NODE;
                public const float DEFAULTVALUE = 0.5f;
                public static float value;
            }
        }
    }
    public const float SETTINGSDATA_AUDIO_SOUND_DEFAULTVALUE = SettingsData.Audio.Sound.DEFAULTVALUE;
    public const float SETTINGSDATA_AUDIO_MUSIC_DEFAULTVALUE = SettingsData.Audio.Music.DEFAULTVALUE;

    private XmlDocument SettingsData_Load()
    {
        var _xmlDoc_settingsData = new XmlDocument();

        if (!File.Exists(settingsData_file_path))
        {
            var _originNode = _xmlDoc_settingsData.CreateElement(SettingsData.ORIGINNODE); //������� ������ �������� ������ ��� XML ���������
            _xmlDoc_settingsData.AppendChild(_originNode); //���������� ������ �������� ������ � XML ��������

            var _node_audio = _xmlDoc_settingsData.CreateElement(SettingsData.Audio.NODE); //������� ������ Audio ��� XML ���������
            _originNode.AppendChild(_node_audio); //���������� ������ Audio � �������� ������

            var _node_sound = _xmlDoc_settingsData.CreateElement(SettingsData.Audio.Sound.NODE); //������� ������ Sound ��� XML ���������
            _node_sound.InnerText = SettingsData.Audio.Sound.DEFAULTVALUE.ToString(); //���������� �������� � ������ Sound
            _node_audio.AppendChild(_node_sound); //���������� ������ Sound � ������ Audio

            var _node_musicValue = _xmlDoc_settingsData.CreateElement(SettingsData.Audio.Music.NODE); //������� ������ MusicValue ��� XML ���������
            _node_musicValue.InnerText = SettingsData.Audio.Music.DEFAULTVALUE.ToString(); //���������� �������� � ������ Music
            _node_audio.AppendChild(_node_musicValue); //���������� ������ Music � ������ Audio
            
            _xmlDoc_settingsData.Save(settingsData_file_path); //������� ��� �������������� XML ��������
        }
        else
        {
            _xmlDoc_settingsData.Load(settingsData_file_path);
        }

        return (_xmlDoc_settingsData);
    }

    public void SettingsData_Save()
    {
        Directory_CreateIfNotExists(directory_path);

        var _xmlDoc_settingsData = SettingsData_Load();
        _xmlDoc_settingsData.SelectSingleNode(SettingsData.Audio.Sound.PATH).InnerText = SettingsData.Audio.Sound.value.ToString(); //�������������� �������� � ������� Audio/Sound
        _xmlDoc_settingsData.SelectSingleNode(SettingsData.Audio.Music.PATH).InnerText = SettingsData.Audio.Music.value.ToString(); //�������������� �������� � ������� Audio/Music
        _xmlDoc_settingsData.Save(settingsData_file_path); //�������������� XML ��������
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
        #region General

        SingleOnScene = this;

        directory_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + DIRECTORY_NAME + @"\";

        Directory_CreateIfNotExists(directory_path);

        #endregion

        #region Progress Data

        progressData_file_path = directory_path + PROGRESSDATA_FILE_NAME;

        var _xmlDoc_progressData = ProgressData_Load();

        var _coins_text = _xmlDoc_progressData.SelectSingleNode(ProgressData.Coins.PATH).InnerText;
        ProgressData.Coins.value = int.Parse(_coins_text);

        var _upgrades_moreCoins_text = _xmlDoc_progressData.SelectSingleNode(ProgressData.Upgrades.MoreCoins.PATH).InnerText;
        Enum.TryParse(_upgrades_moreCoins_text, out ProgressData.Upgrades.MoreCoins.value);

        var _upgrades_moreBonuses_text = _xmlDoc_progressData.SelectSingleNode(ProgressData.Upgrades.MoreBonuses.PATH).InnerText;
        Enum.TryParse(_upgrades_moreBonuses_text, out ProgressData.Upgrades.MoreBonuses.value);

        var _upgrades_coinMagnet_text = _xmlDoc_progressData.SelectSingleNode(ProgressData.Upgrades.CoinMagnet.PATH).InnerText;
        Enum.TryParse(_upgrades_coinMagnet_text, out ProgressData.Upgrades.CoinMagnet.value);

        var _upgrades_revive_text = _xmlDoc_progressData.SelectSingleNode(ProgressData.Upgrades.Revive.PATH).InnerText;
        Enum.TryParse(_upgrades_revive_text, out ProgressData.Upgrades.Revive.value);

        #endregion

        #region Settings Data

        settingsData_file_path = directory_path + SETTINGSDATA_FILE_NAME;

        var _xmlDoc_settingsData = SettingsData_Load();

        var _audio_soundValue_text = _xmlDoc_settingsData.SelectSingleNode(SettingsData.Audio.Sound.PATH).InnerText;
        SettingsData.Audio.Sound.value = float.Parse(_audio_soundValue_text);

        var _audio_musicValue_text = _xmlDoc_settingsData.SelectSingleNode(SettingsData.Audio.Music.PATH).InnerText;
        SettingsData.Audio.Music.value = float.Parse(_audio_musicValue_text);

        #endregion
    }
}
