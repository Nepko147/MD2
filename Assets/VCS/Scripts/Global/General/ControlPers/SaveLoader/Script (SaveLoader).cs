using UnityEngine;
using System.IO;
using System.Xml;
using System;

public class ControlPers_DataHandler : MonoBehaviour
{
    public static ControlPers_DataHandler SingleOnScene { get; private set; }

    private string directory_path; 
    
    private string file_progressData_path;
    private const string FILE_PROGRESSDATA_NAME = "ProgressData.xml";
    private const string FILE_PROGRESSDATA_ROOT_GAMESAVE = "gamesave";
    private const string FILE_PROGRESSDATA_ROOT_GAMESAVE_COINS = "coins";

    private string file_settingsData_path;
    private const string FILE_SETTINGSDATA_NAME = "Settings.xml";
    private const string FILE_SETTINGSDATA_ROOT_AUDIO = "audio";
    private const string FILE_SETTINGSDATA_ROOT_SETTINGS_VOLUME = "volume";

    private ProgressData progressData;
    private SettingsData settingsData;

    private struct ProgressData
    {
        public int coins;
    }

    private struct SettingsData
    {
        public int volume;
    }

    public int ProgressData_Coins_Get() 
    {
        return progressData.coins;
    }

    public void ProgressData_Coins_Set(int _value)
    {
        progressData.coins = _value;
    }

    public int Settings_Volume_Get()
    {
        return settingsData.volume;
    }

    public void Settings_Volume_Set(int _value)
    {
        settingsData.volume = _value;
    }

    private void Awake()
    {
        SingleOnScene = this;
        Application.targetFrameRate = 500;
        directory_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Midnight Drive\";
        
        if (!Directory.Exists(directory_path)) //Проверка наличия папки
        {
            Directory.CreateDirectory(directory_path); //Создаём, если папки нет
        }

        //Грузим прогресс
        file_progressData_path = directory_path + FILE_PROGRESSDATA_NAME;

        XmlDocument _xmlDoc_progressData = new XmlDocument();

        if (!File.Exists(file_progressData_path)) //Проверка существования файла прогресса
        {                              
            XmlNode _rootNode = _xmlDoc_progressData.CreateElement(FILE_PROGRESSDATA_ROOT_GAMESAVE);    //Создаём новый элемент
            _xmlDoc_progressData.AppendChild(_rootNode);                                                //Добавляем его, как корневой узел

            XmlNode _childNode; //Объявляем экземпляр подузла

            _childNode = _xmlDoc_progressData.CreateElement(FILE_PROGRESSDATA_ROOT_GAMESAVE_COINS);
            _childNode.InnerText = "0";
            _rootNode.AppendChild(_childNode);

            _xmlDoc_progressData.Save(file_progressData_path); //Сохраняем документ
        }

        _xmlDoc_progressData.Load(file_progressData_path);
        var _coins = _xmlDoc_progressData.SelectSingleNode(FILE_PROGRESSDATA_ROOT_GAMESAVE + "/" + FILE_PROGRESSDATA_ROOT_GAMESAVE_COINS).InnerText;
        progressData.coins = int.Parse(_coins);

        //Грузим настройки
        file_settingsData_path = directory_path + FILE_SETTINGSDATA_NAME;

        XmlDocument _xmlDoc_settings = new XmlDocument();

        if (!File.Exists(file_settingsData_path)) //Проверка существования файла настроек
        {            
            XmlNode _rootNode = _xmlDoc_settings.CreateElement(FILE_SETTINGSDATA_ROOT_AUDIO);   //Создаём новый элемент
            _xmlDoc_settings.AppendChild(_rootNode);                                            //Добавляем его, как корневой узел

            XmlNode _childNode; //Объявляем экземпляр подузла

            _childNode = _xmlDoc_settings.CreateElement(FILE_SETTINGSDATA_ROOT_SETTINGS_VOLUME);
            _childNode.InnerText = "10";
            _rootNode.AppendChild(_childNode);

            _xmlDoc_settings.Save(file_settingsData_path); //Сохраняем документ
        }

        _xmlDoc_settings.Load(file_settingsData_path);
        var _volume = _xmlDoc_settings.SelectSingleNode(FILE_SETTINGSDATA_ROOT_AUDIO + "/" + FILE_SETTINGSDATA_ROOT_SETTINGS_VOLUME).InnerText;
        settingsData.volume = int.Parse(_volume);
    }

    public void SaveProgress()
    {       
        if (!Directory.Exists(directory_path)) //Проверка наличия папки с сейвами
        {
            Directory.CreateDirectory(directory_path); //Создаём, если папки нет
        }

        XmlDocument _xmlDoc_progressData = new XmlDocument();

        if (!File.Exists(file_progressData_path)) //Проверка существования файла сохранения
        {
            XmlNode _rootNode = _xmlDoc_progressData.CreateElement(FILE_PROGRESSDATA_ROOT_GAMESAVE);    //Создаём новый элемент
            _xmlDoc_progressData.AppendChild(_rootNode);                                       //Добавляем его, как корневой узел

            XmlNode _childNode; //Объявляем экземпляр подузла

            _childNode = _xmlDoc_progressData.CreateElement(FILE_PROGRESSDATA_ROOT_GAMESAVE_COINS);
            _childNode.InnerText = "0";
            _rootNode.AppendChild(_childNode);

            _xmlDoc_progressData.Save(file_progressData_path); //Сохраняем документ
        }

        XmlDocument _xmlDoc = new XmlDocument();
        _xmlDoc.Load(file_progressData_path);        
        _xmlDoc.SelectSingleNode(FILE_PROGRESSDATA_ROOT_GAMESAVE + "/" + FILE_PROGRESSDATA_ROOT_GAMESAVE_COINS).InnerText = progressData.coins.ToString();        
        _xmlDoc.Save(file_progressData_path);
    }

    public void SaveSettings()
    {
        if (!Directory.Exists(directory_path)) //Проверка наличия папки с сейвами
        {
            Directory.CreateDirectory(directory_path); //Создаём, если папки нет
        }

        XmlDocument _xmlDoc_settingsData = new XmlDocument();

        if (!File.Exists(file_settingsData_path)) //Проверка существования файла сохранения
        {
            XmlNode _rootNode = _xmlDoc_settingsData.CreateElement(FILE_SETTINGSDATA_ROOT_AUDIO);    //Создаём новый элемент
            _xmlDoc_settingsData.AppendChild(_rootNode);                                       //Добавляем его, как корневой узел

            XmlNode _childNode; //Объявляем экземпляр подузла

            _childNode = _xmlDoc_settingsData.CreateElement(FILE_SETTINGSDATA_ROOT_SETTINGS_VOLUME);
            _childNode.InnerText = "0";
            _rootNode.AppendChild(_childNode);

            _xmlDoc_settingsData.Save(file_settingsData_path); //Сохраняем документ
        }

        XmlDocument _xmlDoc = new XmlDocument();
        _xmlDoc.Load(file_settingsData_path);
        _xmlDoc.SelectSingleNode(FILE_SETTINGSDATA_ROOT_AUDIO + "/" + FILE_SETTINGSDATA_ROOT_SETTINGS_VOLUME).InnerText = settingsData.volume.ToString();
        _xmlDoc.Save(file_settingsData_path);
    }
}
