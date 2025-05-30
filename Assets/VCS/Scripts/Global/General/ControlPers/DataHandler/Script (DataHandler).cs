using UnityEngine;
using System.IO;
using System.Xml;
using System;

public class ControlPers_DataHandler : MonoBehaviour
{
    public static ControlPers_DataHandler SingleOnScene { get; private set; }

    private string directory_path;
    private const string DIRECTORY_NAME = "Midnight Drive";
    private void Directory_CreateIfNotExists(string _path)
    {
        if (!Directory.Exists(_path)) //��������� ������� �����
        {
            Directory.CreateDirectory(_path); //������ �����
        }
    }

    private struct ProgressData
    {
        public int coins;
    }
    private ProgressData progressData;
    private string progressData_file_path;
    private const string PROGRESSDATA_FILE_NAME = "Progress.xml";
    private const string PROGRESSDATA_FILE_NODE_COINS = "Coins";
    private const int PROGRESSDATA_FILE_NODE_COINS_DEFAULTVALUE = 0;
    private XmlDocument ProgressData_Load()
    {
        var _xmlDoc_progressData = new XmlDocument();

        if (!File.Exists(progressData_file_path))
        {
            XmlNode _node = _xmlDoc_progressData.CreateElement(PROGRESSDATA_FILE_NODE_COINS); //������� ������ Coins ��� XML ���������
            _node.InnerText = PROGRESSDATA_FILE_NODE_COINS_DEFAULTVALUE.ToString(); //���������� �������� � ������ Coins
            _xmlDoc_progressData.AppendChild(_node); //���������� ������ Coins � XML ��������

            _xmlDoc_progressData.Save(progressData_file_path); //������� ��� �������������� XML ��������
        }
        else
        {
            _xmlDoc_progressData.Load(progressData_file_path);
        }

        return (_xmlDoc_progressData);
    }
    public int ProgressData_Coins 
    {
        get { return (progressData.coins); }
        set { progressData.coins = value; }
    }
    
    private struct SettingsData
    {
        public float soundValue;
        public float musicValue;
    }
    private SettingsData settingsData;
    private string settingsData_file_path;
    private const string SETTINGSDATA_FILE_NAME = "Settings.xml";
    private const string SETTINGSDATA_FILE_NODE_AUDIO_NAME = "Audio";
    private const string SETTINGSDATA_FILE_NODE_AUDIO_NODE_SOUNDVALUE_NAME = "SoundValue";
    private const float SETTINGSDATA_FILE_NODE_AUDIO_NODE_SOUNDVALUE_DEFAULTVALUE = 0.5f;
    private const string SETTINGSDATA_FILE_NODE_AUDIO_NODE_MUSICVALUE_NAME = "MusicValue";
    private const float SETTINGSDATA_FILE_NODE_AUDIO_NODE_MUSICVALUE_DEFAULTVALUE = 0.5f;
    private XmlDocument SettingsData_Load()
    {
        var _xmlDoc_settingsData = new XmlDocument();

        if (!File.Exists(settingsData_file_path))
        {
            XmlNode _node_audio = _xmlDoc_settingsData.CreateElement(SETTINGSDATA_FILE_NODE_AUDIO_NAME); //������� ������ Audio ��� XML ���������
            _xmlDoc_settingsData.AppendChild(_node_audio); //���������� ������ Audio � XML ��������

            XmlNode _node_soundValue = _xmlDoc_settingsData.CreateElement(SETTINGSDATA_FILE_NODE_AUDIO_NODE_SOUNDVALUE_NAME); //������� ������ SoundValue ��� XML ���������
            _node_soundValue.InnerText = SETTINGSDATA_FILE_NODE_AUDIO_NODE_SOUNDVALUE_DEFAULTVALUE.ToString(); //���������� �������� � ������ SoundValue
            _node_audio.AppendChild(_node_soundValue); //���������� ������ SoundValue � ������ Audio

            XmlNode _node_musicValue = _xmlDoc_settingsData.CreateElement(SETTINGSDATA_FILE_NODE_AUDIO_NODE_MUSICVALUE_NAME); //������� ������ MusicValue ��� XML ���������
            _node_musicValue.InnerText = SETTINGSDATA_FILE_NODE_AUDIO_NODE_MUSICVALUE_DEFAULTVALUE.ToString(); //���������� �������� � ������ MusicValue
            _node_audio.AppendChild(_node_musicValue); //���������� ������ MusicValue � ������ Audio
            
            _xmlDoc_settingsData.Save(settingsData_file_path); //������� ��� �������������� XML ��������
        }
        else
        {
            _xmlDoc_settingsData.Load(settingsData_file_path);
        }

        return (_xmlDoc_settingsData);
    }
    public float Settings_SoundValue
    {
        get { return (settingsData.soundValue); }
        set { settingsData.soundValue = value; }
    }
    public float Settings_MusicValue
    {
        get { return (settingsData.musicValue); }
        set { settingsData.musicValue = value; }
    }

    private void Awake()
    {
        SingleOnScene = this;
        directory_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + DIRECTORY_NAME + @"\";

        Directory_CreateIfNotExists(directory_path);

        progressData_file_path = directory_path + PROGRESSDATA_FILE_NAME;
        var _xmlDoc_progressData = ProgressData_Load();
        var _coins_text = _xmlDoc_progressData.SelectSingleNode(PROGRESSDATA_FILE_NODE_COINS).InnerText;
        progressData.coins = int.Parse(_coins_text);

        settingsData_file_path = directory_path + SETTINGSDATA_FILE_NAME;
        var _xmlDoc_settingsData = SettingsData_Load();
        var _audio_soundValue_text = _xmlDoc_settingsData.SelectSingleNode(SETTINGSDATA_FILE_NODE_AUDIO_NAME + "/" + SETTINGSDATA_FILE_NODE_AUDIO_NODE_SOUNDVALUE_NAME).InnerText;
        settingsData.soundValue = float.Parse(_audio_soundValue_text);
        var _audio_musicValue_text = _xmlDoc_settingsData.SelectSingleNode(SETTINGSDATA_FILE_NODE_AUDIO_NAME + "/" + SETTINGSDATA_FILE_NODE_AUDIO_NODE_MUSICVALUE_NAME).InnerText;
        settingsData.musicValue = float.Parse(_audio_musicValue_text);
    }

    public void SaveProgress()
    {
        Directory_CreateIfNotExists(directory_path);

        var _xmlDoc_progressData = ProgressData_Load();
        _xmlDoc_progressData.SelectSingleNode(PROGRESSDATA_FILE_NODE_COINS).InnerText = progressData.coins.ToString(); //�������������� �������� � ������� Coins
        _xmlDoc_progressData.Save(progressData_file_path); //�������������� XML ��������
    }

    public void SaveSettings()
    {
        Directory_CreateIfNotExists(directory_path);

        var _xmlDoc_settingsData = SettingsData_Load();
        _xmlDoc_settingsData.SelectSingleNode(SETTINGSDATA_FILE_NODE_AUDIO_NAME + "/" + SETTINGSDATA_FILE_NODE_AUDIO_NODE_SOUNDVALUE_NAME).InnerText = settingsData.soundValue.ToString(); //�������������� �������� � ������� SoundValue
        _xmlDoc_settingsData.SelectSingleNode(SETTINGSDATA_FILE_NODE_AUDIO_NAME + "/" + SETTINGSDATA_FILE_NODE_AUDIO_NODE_MUSICVALUE_NAME).InnerText = settingsData.musicValue.ToString(); //�������������� �������� � ������� MusicValue
        _xmlDoc_settingsData.Save(settingsData_file_path); //�������������� XML ��������
    }
}
