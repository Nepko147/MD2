using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Security.Cryptography;
using YG;
using YG.Insides;

public class ControlPers_DataHandler : MonoBehaviour
{
    #region General

    public static ControlPers_DataHandler SingleOnScene { get; private set; }

    public bool IsDataLoaded { get; private set; }

    #endregion

    #region Directory

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

    #region Encryption

    private const string ENCRYPTION_PASSWORD = "SuperSecretKey";
    private const string ENCRYPTION_SALT = "Salt1234"; // "Соль" - это вторая половинка ключа шифрования

    private string Encryption_Encrypt(string _inputData)
    {
        Aes _aesAlgorhythm = Aes.Create(); // Создаём алгоритм блочного шифрования
        
        var _salt = Encoding.UTF8.GetBytes(ENCRYPTION_SALT); 
        var _deriveBytes = new Rfc2898DeriveBytes(ENCRYPTION_PASSWORD, _salt); //Полный ключ шифрования складывается из пароля и "Соли"

        var _keyBytes = _deriveBytes.GetBytes(32); //Байты ключа
        var _saltBytes = _deriveBytes.GetBytes(16);  //Байты "Соли"
        _aesAlgorhythm.Key = _keyBytes;
        _aesAlgorhythm.IV = _saltBytes;

        ICryptoTransform _encryptor = _aesAlgorhythm.CreateEncryptor(_aesAlgorhythm.Key, _aesAlgorhythm.IV); // Создаём шифровальшик

        MemoryStream _memoryStream = new MemoryStream(); //Создаём отдельный поток в памяти
        CryptoStream _cryptoStream = new CryptoStream(_memoryStream, _encryptor, CryptoStreamMode.Write); //Создаём в отдельном потоке памяти, поток шифрования в режиме записи
        StreamWriter _streamWriter = new StreamWriter(_cryptoStream); // Создаём в этом всём "Потокового писальщика"...        
        _streamWriter.Write(_inputData); // Шифруем
        _streamWriter.Close(); //Закрываем
        var _encryptedString = Convert.ToBase64String(_memoryStream.ToArray()); //Производим ковертацию зашифрованных данных в Base64-строку
        
        return _encryptedString;               
    }

    private string Encryption_Decrypt(string _encryptedText)
    {
        Aes _aesAlgorhythm = Aes.Create(); // Создаём алгоритм блочного шифрования
        
        var _salt = Encoding.UTF8.GetBytes(ENCRYPTION_SALT); 
        var _deriveBytes = new Rfc2898DeriveBytes(ENCRYPTION_PASSWORD, _salt); //Полный ключ шифрования складывается из пароля и "Соли"

        var _keyBytes = _deriveBytes.GetBytes(32); //Байты ключа
        var _saltBytes = _deriveBytes.GetBytes(16);  //Байты "Соли"
        _aesAlgorhythm.Key = _keyBytes;
        _aesAlgorhythm.IV = _saltBytes;

        ICryptoTransform _decryptor = _aesAlgorhythm.CreateDecryptor(_aesAlgorhythm.Key, _aesAlgorhythm.IV); // Создаём дешифровальшик

        byte[] _encryptedTextBytes = Convert.FromBase64String(_encryptedText); //Производим ковертацию зашифрованной строки в байты из Base64-строки

        MemoryStream _memoryStream = new MemoryStream(_encryptedTextBytes); //Создаём отдельный поток в памяти
        CryptoStream _cryptoStream = new CryptoStream(_memoryStream, _decryptor, CryptoStreamMode.Read); //Создаём в отдельном потоке памяти, поток дешифрования в режиме чтения
        StreamReader _streamReader = new StreamReader(_cryptoStream); // Создаём в этом всём "Потокового читальщика"...        
        var _decryptedString = _streamReader.ReadToEnd(); //Расшифровываем
        
        return _decryptedString;        
    }

    #endregion

    #region Progress Data

    private struct ProgressData
    {
        public const string ORIGINNODE = "Progress";

        public struct Statistics
        {
            public const string NODE = "Statistics";
            public const string PATH = ORIGINNODE + "/" + NODE;

            public struct ReviveNumber
            {
                public const string NODE = "ReviveNumber";
                public const string PATH = ORIGINNODE + "/" + Statistics.NODE + "/" + NODE;
                public const int DEFAULTVALUE = 0;
                public static int value = DEFAULTVALUE;
            }

            public struct ReviveNumberBest
            {
                public const string NODE = "ReviveNumberBest";
                public const string PATH = ORIGINNODE + "/" + Statistics.NODE + "/" + NODE;
                public const int DEFAULTVALUE = 999999;
                public static int value = DEFAULTVALUE;
            }

            public struct CoinsTotal
            {
                public const string NODE = "CoinsTotal";
                public const string PATH = ORIGINNODE + "/" + Statistics.NODE + "/" + NODE;
                public const int DEFAULTVALUE = 0;
                public static int value = DEFAULTVALUE;
            }

            public struct CoinsSpentOnRevivals
            {
                public const string NODE = "CoinsSpentOnRevivals";
                public const string PATH = ORIGINNODE + "/" + Statistics.NODE + "/" + NODE;
                public const int DEFAULTVALUE = 0;
                public static int value = DEFAULTVALUE;
            }

            public struct Defeats
            {
                public const string NODE = "Defeats";
                public const string PATH = ORIGINNODE + "/" + Statistics.NODE + "/" + NODE;
                public const int DEFAULTVALUE = 0;
                public static int value = DEFAULTVALUE;
            }

            public struct TotalDrivings
            {
                public const string NODE = "TotalDrivings";
                public const string PATH = ORIGINNODE + "/" + Statistics.NODE + "/" + NODE;
                public const int DEFAULTVALUE = 0;
                public static int value = DEFAULTVALUE;
            }
        }

        public struct Upgrades
        {
            public const string NODE = "Upgrades";
            public const string PATH = ORIGINNODE + "/" + NODE;

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

        public struct Coins
        {
            public const string NODE = "Coins";
            public const string PATH = ORIGINNODE + "/" + NODE;
            public const int DEFAULTVALUE = 0;
            public static int value = DEFAULTVALUE;
        }

        public struct Tutorial
        {
            public const string NODE = "Coins";
            public const string PATH = ORIGINNODE + "/" + NODE;
            public const bool DEFAULTVALUE = true;
            public static bool value = DEFAULTVALUE;
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
    private const string PROGRESSDATA_FILE_ENCRYPTED_NAME = "Progress.enc";
    private string progressData_file_path;

    private void ProgressData_File_Check()
    {
        var _originNode = progressData_file.SelectSingleNode(ProgressData.ORIGINNODE); //Объявляем исходный раздел

        if (progressData_file.SelectSingleNode(ProgressData.ORIGINNODE) == null) //Проверяем наличие исходного раздела
        {
            _originNode = progressData_file.CreateElement(ProgressData.ORIGINNODE); //Создаем исходный раздел для XML документа
            progressData_file.AppendChild(_originNode); //Записываем исходный раздел в XML документ
        }

        var _node_statistics = progressData_file.SelectSingleNode(ProgressData.Statistics.PATH); //Берём раздел "Statistics"

        if (_node_statistics == null) //Проверяем наличие раздела Statistics в документе
        {
            _node_statistics = progressData_file.CreateElement(ProgressData.Statistics.NODE); //Создаем раздел Statistics для XML документа
            _originNode.AppendChild(_node_statistics); //Записываем раздел Statistics в исходный раздел
        }

        if (progressData_file.SelectSingleNode(ProgressData.Statistics.ReviveNumber.PATH) == null) //Проверяем наличие узла ReviveNumber в документе
        {
            var _node_reviveNumber = progressData_file.CreateElement(ProgressData.Statistics.ReviveNumber.NODE); //Создаем раздел ReviveNumber для XML документа
            _node_reviveNumber.InnerText = ProgressData.Statistics.ReviveNumber.DEFAULTVALUE.ToString(); //Записываем значение в раздел ReviveNumber
            _node_statistics.AppendChild(_node_reviveNumber); //Записываем раздел ReviveNumber в раздел Statistics                    
        }

        if (progressData_file.SelectSingleNode(ProgressData.Statistics.ReviveNumberBest.PATH) == null) //Проверяем наличие узла ReviveNumberBest в документе
        {
            var _node_reviveNumberBest = progressData_file.CreateElement(ProgressData.Statistics.ReviveNumberBest.NODE); //Создаем раздел ReviveNumberBest для XML документа
            _node_reviveNumberBest.InnerText = ProgressData.Statistics.ReviveNumberBest.DEFAULTVALUE.ToString(); //Записываем значение в раздел ReviveNumberBest
            _node_statistics.AppendChild(_node_reviveNumberBest); //Записываем раздел ReviveNumberBest в раздел Statistics                    
        }

        if (progressData_file.SelectSingleNode(ProgressData.Statistics.CoinsTotal.PATH) == null) //Проверяем наличие узла CoinsTotal в документе
        {
            var _node_coinsTotal = progressData_file.CreateElement(ProgressData.Statistics.CoinsTotal.NODE); //Создаем раздел CoinsTotal для XML документа
            _node_coinsTotal.InnerText = ProgressData.Statistics.CoinsTotal.DEFAULTVALUE.ToString(); //Записываем значение в раздел CoinsTotal
            _node_statistics.AppendChild(_node_coinsTotal); //Записываем раздел CoinsTotal в раздел Statistics
        }

        if (progressData_file.SelectSingleNode(ProgressData.Statistics.CoinsSpentOnRevivals.PATH) == null) //Проверяем наличие узла CoinsSpentOnRevivals в документе
        {
            var _node_coinsSpentOnRevivals = progressData_file.CreateElement(ProgressData.Statistics.CoinsSpentOnRevivals.NODE); //Создаем раздел CoinsSpentOnRevivals для XML документа
            _node_coinsSpentOnRevivals.InnerText = ProgressData.Statistics.CoinsSpentOnRevivals.DEFAULTVALUE.ToString(); //Записываем значение в раздел CoinsSpentOnRevivals
            _node_statistics.AppendChild(_node_coinsSpentOnRevivals); //Записываем раздел CoinsSpentOnRevivals в раздел Statistics
        }

        if (progressData_file.SelectSingleNode(ProgressData.Statistics.Defeats.PATH) == null) //Проверяем наличие узла Defeats в документе
        {
            var _node_defeats = progressData_file.CreateElement(ProgressData.Statistics.Defeats.NODE); //Создаем раздел Defeats для XML документа
            _node_defeats.InnerText = ProgressData.Statistics.Defeats.DEFAULTVALUE.ToString(); //Записываем значение в раздел Defeats
            _node_statistics.AppendChild(_node_defeats); //Записываем раздел Defeats в раздел Statistics
        }

        if (progressData_file.SelectSingleNode(ProgressData.Statistics.TotalDrivings.PATH) == null) //Проверяем наличие узла TotalDrivings в документе
        {
            var _node_totalDrivings = progressData_file.CreateElement(ProgressData.Statistics.TotalDrivings.NODE); //Создаем раздел TotalDrivings для XML документа
            _node_totalDrivings.InnerText = ProgressData.Statistics.TotalDrivings.DEFAULTVALUE.ToString(); //Записываем значение в раздел TotalDrivings
            _node_statistics.AppendChild(_node_totalDrivings); //Записываем раздел TotalDrivings в раздел Statistics
        }

        var _node_upgrades = progressData_file.SelectSingleNode(ProgressData.Upgrades.PATH); //Берём раздел "Upgrades"

        if (_node_upgrades == null) //Проверяем наличие раздела Upgrades в документе
        {
            _node_upgrades = progressData_file.CreateElement(ProgressData.Upgrades.NODE); //Создаем раздел Upgrades для XML документа
            _originNode.AppendChild(_node_upgrades); //Записываем раздел Upgrades в исходный раздел
        }

        if (progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreCoins.PATH) == null) //Проверяем наличие узла MoreCoins в документе
        {
            var _node_moreCoins = progressData_file.CreateElement(ProgressData.Upgrades.MoreCoins.NODE); //Создаем раздел MoreCoins для XML документа
            _node_moreCoins.InnerText = ProgressData.Upgrades.MoreCoins.DEFAULTVALUE.ToString(); //Записываем значение в раздел MoreCoins
            _node_upgrades.AppendChild(_node_moreCoins); //Записываем раздел MoreCoins в раздел Upgrades                    
        }

        if (progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreBonuses.PATH) == null) //Проверяем наличие узла MoreBonuses в документе
        {
            var _node_moreBonuses = progressData_file.CreateElement(ProgressData.Upgrades.MoreBonuses.NODE); //Создаем раздел MoreBonuses для XML документа
            _node_moreBonuses.InnerText = ProgressData.Upgrades.MoreCoins.DEFAULTVALUE.ToString(); //Записываем значение в раздел MoreBonuses
            _node_upgrades.AppendChild(_node_moreBonuses); //Записываем раздел MoreBonuses в раздел Upgrades                    
        }

        if (progressData_file.SelectSingleNode(ProgressData.Upgrades.CoinMagnet.PATH) == null) //Проверяем наличие узла CoinMagnet в документе
        {
            var _node_coinMagnet = progressData_file.CreateElement(ProgressData.Upgrades.CoinMagnet.NODE); //Создаем раздел CoinMagnet для XML документа
            _node_coinMagnet.InnerText = ProgressData.Upgrades.MoreCoins.DEFAULTVALUE.ToString(); //Записываем значение в раздел CoinMagnet
            _node_upgrades.AppendChild(_node_coinMagnet); //Записываем раздел CoinMagnet в раздел Upgrades
        }

        if (progressData_file.SelectSingleNode(ProgressData.Upgrades.Revive.PATH) == null) //Проверяем наличие узла Revive в документе
        {
            var _node_revive = progressData_file.CreateElement(ProgressData.Upgrades.Revive.NODE); //Создаем раздел Revive для XML документа
            _node_revive.InnerText = ProgressData.Upgrades.MoreCoins.DEFAULTVALUE.ToString(); //Записываем значение в раздел Revive
            _node_upgrades.AppendChild(_node_revive); //Записываем раздел Revive в раздел Upgrades
        }

        if (progressData_file.SelectSingleNode(ProgressData.Coins.PATH) == null) //Проверяем наличие узла Coins в документе
        {
            var _node_coins = progressData_file.CreateElement(ProgressData.Coins.NODE); //Создаем раздел Coins для XML документа
            _node_coins.InnerText = ProgressData.Coins.value.ToString(); //Записываем значение в раздел Coins
            _originNode.AppendChild(_node_coins); //Записываем раздел Coins в исходный раздел
        }
    }

    public void ProgressData_Load()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.PlatformType_Current)
        {
            case ControlPers_BuildSettings.PlatformType.windows:

                progressData_file = new XmlDocument();

                try //Прробуем загрузить файл
                {
                    if (ControlPers_BuildSettings.SingleOnScene.EncryptProgressFile)
                    {
                        var _encryptedData = File.ReadAllText(progressData_file_path);
                        progressData_file.InnerXml = Encryption_Decrypt(_encryptedData);
                    }
                    else
                    {
                        progressData_file.Load(progressData_file_path); //Если файл в порядке, то всё загрузится
                    }
                }
                catch { } //Если нет, то будет нулевой прогресс. Код ниже отработает. После сохранения, файл починится (даже если он убит в хлам)

                ProgressData_File_Check();

                var _reviveNumber_text = progressData_file.SelectSingleNode(ProgressData.Statistics.ReviveNumber.PATH).InnerText; //Считываем данные из файла
                ProgressData.Statistics.ReviveNumber.value = int.Parse(_reviveNumber_text); //Грузиим "Количество возрождений"
                var _reviveNumberBest_text = progressData_file.SelectSingleNode(ProgressData.Statistics.ReviveNumberBest.PATH).InnerText; //Считываем данные из файла
                ProgressData.Statistics.ReviveNumberBest.value = int.Parse(_reviveNumberBest_text); //Грузиим "Количество возрождений" (Лучший)
                var _coinsTotal_text = progressData_file.SelectSingleNode(ProgressData.Statistics.CoinsTotal.PATH).InnerText; //Считываем данные из файла
                ProgressData.Statistics.CoinsTotal.value = int.Parse(_coinsTotal_text); //Грузиим "Всего монет"
                var _coinsSpentOnRevivals_text = progressData_file.SelectSingleNode(ProgressData.Statistics.CoinsSpentOnRevivals.PATH).InnerText; //Считываем данные из файла
                ProgressData.Statistics.CoinsSpentOnRevivals.value = int.Parse(_coinsSpentOnRevivals_text); //Грузиим "Потрачено монет на возрождения"
                var _defeats_text = progressData_file.SelectSingleNode(ProgressData.Statistics.Defeats.PATH).InnerText; //Считываем данные из файла
                ProgressData.Statistics.Defeats.value = int.Parse(_defeats_text); //Грузиим "Поражения"
                var _totalDrivings_text = progressData_file.SelectSingleNode(ProgressData.Statistics.TotalDrivings.PATH).InnerText; //Считываем данные из файла
                ProgressData.Statistics.TotalDrivings.value = int.Parse(_totalDrivings_text); //Грузиим "Всего Поездок"

                var _upgrades_moreCoins_text = progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreCoins.PATH).InnerText; //Считываем данные из файла
                Enum.TryParse(_upgrades_moreCoins_text, out ProgressData.Upgrades.MoreCoins.value); //Грузиим состояние апгрейда "Больше монет"
                var _upgrades_moreBonuses_text = progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreBonuses.PATH).InnerText; //Считываем данные из файла
                Enum.TryParse(_upgrades_moreBonuses_text, out ProgressData.Upgrades.MoreBonuses.value); //Грузиим состояние апгрейда "Больше бонусов"
                var _upgrades_coinMagnet_text = progressData_file.SelectSingleNode(ProgressData.Upgrades.CoinMagnet.PATH).InnerText; //Считываем данные из файла
                Enum.TryParse(_upgrades_coinMagnet_text, out ProgressData.Upgrades.CoinMagnet.value); //Грузиим состояние апгрейда "Магнит для монет"
                var _upgrades_revive_text = progressData_file.SelectSingleNode(ProgressData.Upgrades.Revive.PATH).InnerText; //Считываем данные из файла
                Enum.TryParse(_upgrades_revive_text, out ProgressData.Upgrades.Revive.value); //Грузиим состояние апгрейда "Да не умер он"

                var _coins_text = progressData_file.SelectSingleNode(ProgressData.Coins.PATH).InnerText; //Считываем данные из файла
                ProgressData.Coins.value = int.Parse(_coins_text); //Грузиим монетки
            break;

            case ControlPers_BuildSettings.PlatformType.web_yandexGames_desktop:
            case ControlPers_BuildSettings.PlatformType.web_yandexGames_mobile_android:

                ProgressData.Statistics.ReviveNumber.value = YG2.saves.ProgressData_Statistics_ReviveNumber;
                ProgressData.Statistics.ReviveNumberBest.value = YG2.saves.ProgressData_Statistics_ReviveNumberBest;
                ProgressData.Statistics.CoinsTotal.value = YG2.saves.ProgressData_Statistics_CoinsTotal;
                ProgressData.Statistics.CoinsSpentOnRevivals.value = YG2.saves.ProgressData_Statistics_CoinsSpentOnRevivals;
                ProgressData.Statistics.Defeats.value = YG2.saves.ProgressData_Statistics_Defeats;
                ProgressData.Statistics.TotalDrivings.value = YG2.saves.ProgressData_Statistics_TotalDrivings;

                ProgressData.Upgrades.MoreCoins.value = YG2.saves.ProgressData_Upgrades_MoreCoins;
                ProgressData.Upgrades.MoreBonuses.value = YG2.saves.ProgressData_Upgrades_MoreBonuses;
                ProgressData.Upgrades.CoinMagnet.value = YG2.saves.ProgressData_Upgrades_CoinMagnet;
                ProgressData.Upgrades.Revive.value = YG2.saves.ProgressData_Upgrades_Revive;

                ProgressData.Coins.value = YG2.saves.ProgressData_Coins;
            break;
        }
    }

    public void ProgressData_Save()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.PlatformType_Current)
        {
            case ControlPers_BuildSettings.PlatformType.windows:
                Directory_CreateIfNotExists(directory_path);

                progressData_file.RemoveAll(); // Очищаем файл с сохранением. Нужно, чтобы в него не попало ничего лишнего (Хвосты из прошлых версий. Например что-то переименовали/переместили/удалили)
                ProgressData_File_Check(); //Перезаполняем структуру файла

                progressData_file.SelectSingleNode(ProgressData.Statistics.ReviveNumber.PATH).InnerText = ProgressData.Statistics.ReviveNumber.value.ToString(); //Перезаписываем значение в разделе Statistics/ReviveNumber
                progressData_file.SelectSingleNode(ProgressData.Statistics.ReviveNumberBest.PATH).InnerText = ProgressData.Statistics.ReviveNumberBest.value.ToString(); //Перезаписываем значение в разделе Statistics/ReviveNumberBest
                progressData_file.SelectSingleNode(ProgressData.Statistics.CoinsTotal.PATH).InnerText = ProgressData.Statistics.CoinsTotal.value.ToString(); //Перезаписываем значение в разделе Statistics/CoinsTotal
                progressData_file.SelectSingleNode(ProgressData.Statistics.CoinsSpentOnRevivals.PATH).InnerText = ProgressData.Statistics.CoinsSpentOnRevivals.value.ToString(); //Перезаписываем значение в разделе Statistics/CoinsSpentOnRevivals
                progressData_file.SelectSingleNode(ProgressData.Statistics.Defeats.PATH).InnerText = ProgressData.Statistics.Defeats.value.ToString(); //Перезаписываем значение в разделе Statistics/Defeats
                progressData_file.SelectSingleNode(ProgressData.Statistics.TotalDrivings.PATH).InnerText = ProgressData.Statistics.TotalDrivings.value.ToString(); //Перезаписываем значение в разделе Statistics/TotalDrivings

                progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreCoins.PATH).InnerText = ProgressData.Upgrades.MoreCoins.value.ToString(); //Перезаписываем значение в разделе Upgrades/MoreCoins
                progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreBonuses.PATH).InnerText = ProgressData.Upgrades.MoreBonuses.value.ToString(); //Перезаписываем значение в разделе Upgrades/MoreBonuses
                progressData_file.SelectSingleNode(ProgressData.Upgrades.CoinMagnet.PATH).InnerText = ProgressData.Upgrades.CoinMagnet.value.ToString(); //Перезаписываем значение в разделе Upgrades/CoinMagnet
                progressData_file.SelectSingleNode(ProgressData.Upgrades.Revive.PATH).InnerText = ProgressData.Upgrades.Revive.value.ToString(); //Перезаписываем значение в разделе Upgrades/Revive

                progressData_file.SelectSingleNode(ProgressData.Coins.PATH).InnerText = ProgressData.Coins.value.ToString(); //Перезаписываем значение в разделе Coins

                if (ControlPers_BuildSettings.SingleOnScene.EncryptProgressFile)
                {
                    var _encryptedProgressData = Encryption_Encrypt(progressData_file.InnerXml);
                    File.WriteAllText(progressData_file_path, _encryptedProgressData);
                }
                else
                {
                    progressData_file.Save(progressData_file_path); //Создаем или перезаписываем файл XML документа
                }                
            break;

            case ControlPers_BuildSettings.PlatformType.web_yandexGames_desktop:
            case ControlPers_BuildSettings.PlatformType.web_yandexGames_mobile_android:

                YG2.saves.ProgressData_Statistics_ReviveNumber = ProgressData.Statistics.ReviveNumber.value;
                YG2.saves.ProgressData_Statistics_ReviveNumberBest = ProgressData.Statistics.ReviveNumberBest.value;
                YG2.saves.ProgressData_Statistics_CoinsTotal = ProgressData.Statistics.CoinsTotal.value;
                YG2.saves.ProgressData_Statistics_CoinsSpentOnRevivals = ProgressData.Statistics.CoinsSpentOnRevivals.value;
                YG2.saves.ProgressData_Statistics_Defeats = ProgressData.Statistics.Defeats.value;
                YG2.saves.ProgressData_Statistics_TotalDrivings = ProgressData.Statistics.TotalDrivings.value;

                YG2.saves.ProgressData_Upgrades_MoreCoins = ProgressData.Upgrades.MoreCoins.value;
                YG2.saves.ProgressData_Upgrades_MoreBonuses = ProgressData.Upgrades.MoreBonuses.value;
                YG2.saves.ProgressData_Upgrades_CoinMagnet = ProgressData.Upgrades.CoinMagnet.value;
                YG2.saves.ProgressData_Upgrades_Revive = ProgressData.Upgrades.Revive.value;

                YG2.saves.ProgressData_Coins = ProgressData.Coins.value;

                YG2.SaveProgress();
            break;
        }
    }

    public int ProgressData_Statistics_ReviveNumber
    {
        get
        {
            return ProgressData.Statistics.ReviveNumber.value;
        }
        set
        {
            ProgressData.Statistics.ReviveNumber.value = value;
        }
    }

    public int ProgressData_Statistics_ReviveNumberBest
    {
        get
        {
            return ProgressData.Statistics.ReviveNumberBest.value;
        }
        set
        {
            ProgressData.Statistics.ReviveNumberBest.value = value;
        }
    }

    public int ProgressData_Statistics_CoinsTotal
    {
        get
        {
            return ProgressData.Statistics.CoinsTotal.value;
        }
        set
        {
            ProgressData.Statistics.CoinsTotal.value = value;
        }
    }

    public int ProgressData_Statistics_CoinsSpentOnRevivals
    {
        get
        {
            return ProgressData.Statistics.CoinsSpentOnRevivals.value;
        }
        set
        {
            ProgressData.Statistics.CoinsSpentOnRevivals.value = value;
        }
    }

    public int ProgressData_Statistics_Defeats
    {
        get
        {
            return ProgressData.Statistics.Defeats.value;
        }
        set
        {
            ProgressData.Statistics.Defeats.value = value;
        }
    }
    public int ProgressData_Statistics_TotalDrivings
    {
        get
        {
            return ProgressData.Statistics.TotalDrivings.value;
        }
        set
        {
            ProgressData.Statistics.TotalDrivings.value = value;
        }
    }

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

    public bool ProgressData_Tutorial
    {
        get
        {
            return (ProgressData.Tutorial.value);
        }
        set
        {
            ProgressData.Tutorial.value = value;
        }
    }

    #endregion

    #region Settings Data

    private struct SettingsData
    {
        public const string ORIGINNODE = "Settings";

        public struct Audio
        {
            public const string NODE = "Audio";
            public const string PATH = ORIGINNODE + "/" + NODE;

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

        public struct Language
        {
            public const string NODE = "Language";
            public const string PATH = ORIGINNODE + "/" + NODE;
            public const ControlPers_LanguageHandler.GameLanguage DEFAULTVALUE = ControlPers_LanguageHandler.GameLanguage.english;
            public static ControlPers_LanguageHandler.GameLanguage value = DEFAULTVALUE;
        }
    }

    private XmlDocument settingsData_file;
    private const string SETTINGSDATA_FILE_NAME = "Settings.xml";
    private string settingsData_file_path;

    private void SettingsData_File_Check()
    {
        var _originNode = settingsData_file.SelectSingleNode(SettingsData.ORIGINNODE); // Считываем исходный радел; //Объявляем исходный раздел

        if (_originNode == null) //Проверяем наличие исходного раздела
        {
            _originNode = settingsData_file.CreateElement(SettingsData.ORIGINNODE); //Создаем исходный раздел для XML документа
            settingsData_file.AppendChild(_originNode); //Записываем исходный раздел в XML документ
        }

        var _node_audio = settingsData_file.SelectSingleNode(SettingsData.Audio.PATH); //Берём раздел "Audio"

        if (_node_audio == null) //Проверяем наличие раздела Audio
        {
            _node_audio = settingsData_file.CreateElement(SettingsData.Audio.NODE); //Создаем раздел Audio для XML документа
            _originNode.AppendChild(_node_audio); //Записываем раздел Audio в исходный раздел
        }

        if (settingsData_file.SelectSingleNode(SettingsData.Audio.Sound.PATH) == null) //Проверяем наличие узла Sound в документе                 
        {
            var _node_sound = settingsData_file.CreateElement(SettingsData.Audio.Sound.NODE); //Создаем раздел Sound для XML документа
            _node_sound.InnerText = SettingsData.Audio.Sound.DEFAULTVALUE.ToString(); //Записываем значение в раздел Sound
            _node_audio.AppendChild(_node_sound); //Записываем в него раздел Sound                    
        }

        if (settingsData_file.SelectSingleNode(SettingsData.Audio.Music.PATH) == null) //Проверяем наличие узла Music в документе                 
        {
            var _node_musicValue = settingsData_file.CreateElement(SettingsData.Audio.Music.NODE); //Создаем раздел Music для XML документа
            _node_musicValue.InnerText = SettingsData.Audio.Music.DEFAULTVALUE.ToString(); //Записываем значение в раздел Music
            _node_audio.AppendChild(_node_musicValue); //Записываем в него раздел Music                    
        }

        if (settingsData_file.SelectSingleNode(SettingsData.Language.PATH) == null) //Проверяем наличие узла Language в документе 
        {
            var _node_language = settingsData_file.CreateElement(SettingsData.Language.NODE); //Создаем раздел Language для XML документа
            _node_language.InnerText = SettingsData.Language.DEFAULTVALUE.ToString(); //Записываем значение в раздел Language
            _originNode.AppendChild(_node_language); //Записываем в него раздел Language                    
        }
    }

    public const float SETTINGSDATA_AUDIO_SOUND_DEFAULTVALUE = SettingsData.Audio.Sound.DEFAULTVALUE;
    public const float SETTINGSDATA_AUDIO_MUSIC_DEFAULTVALUE = SettingsData.Audio.Music.DEFAULTVALUE;

    public const ControlPers_LanguageHandler.GameLanguage SETTINGSDATA_LANGUAGE_DEFAULTVALUE = SettingsData.Language.DEFAULTVALUE;

    public void SettingsData_Load()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.PlatformType_Current)
        {
            case ControlPers_BuildSettings.PlatformType.windows:
                settingsData_file = new XmlDocument();
                settingsData_file_path = directory_path + SETTINGSDATA_FILE_NAME;

                try //Прробуем загрузить файл
                {
                    settingsData_file.Load(settingsData_file_path); //Если файл в порядке, то всё загрузится
                }
                catch { } //Если нет, то будут настройки по умолчанию. Код ниже отработает. После сохранения, файл починится (даже если он убит в хлам)

                SettingsData_File_Check(); //Проверяем структуру файла                

                var _audio_soundValue_text = settingsData_file.SelectSingleNode(SettingsData.Audio.Sound.PATH).InnerText; //Считываем данные из файла
                SettingsData.Audio.Sound.value = float.Parse(_audio_soundValue_text); //Грузим громкость звуков

                var _audio_musicValue_text = settingsData_file.SelectSingleNode(SettingsData.Audio.Music.PATH).InnerText; //Считываем данные из файла
                SettingsData.Audio.Music.value = float.Parse(_audio_musicValue_text); //Грузим громкость музыки

                var _languageValue_text = settingsData_file.SelectSingleNode(SettingsData.Language.PATH).InnerText; //Считываем данные из файла
                Enum.TryParse(_languageValue_text, out SettingsData.Language.value); //Грузим язык
            break;

            case ControlPers_BuildSettings.PlatformType.web_yandexGames_desktop:
            case ControlPers_BuildSettings.PlatformType.web_yandexGames_mobile_android:
                SettingsData.Audio.Sound.value = YG2.saves.SettingsData_Audio_Sound;
                SettingsData.Audio.Music.value = YG2.saves.SettingsData_Audio_Music;

                switch (YG2.lang)
                {
                    case "ru":
                        SettingsData.Language.value = ControlPers_LanguageHandler.GameLanguage.russian;
                    break;

                    default:
                        SettingsData.Language.value = SettingsData.Language.DEFAULTVALUE;
                    break;
                }
            break;
        }
    }

    public void SettingsData_Save()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.PlatformType_Current)
        {
            case ControlPers_BuildSettings.PlatformType.windows:
                Directory_CreateIfNotExists(directory_path);
                                
                settingsData_file.RemoveAll(); // Очищаем файл с сохранением. Нужно, чтобы в него не попало ничего лишнего (Хвосты из прошлых версий. Например что-то переименовали/переместили/удалили)
                SettingsData_File_Check(); //Перезаполняем структуру файла

                settingsData_file.SelectSingleNode(SettingsData.Audio.Sound.PATH).InnerText = SettingsData.Audio.Sound.value.ToString(); //Перезаписываем значение в разделе Audio/Sound
                settingsData_file.SelectSingleNode(SettingsData.Audio.Music.PATH).InnerText = SettingsData.Audio.Music.value.ToString(); //Перезаписываем значение в разделе Audio/Music
                settingsData_file.SelectSingleNode(SettingsData.Language.PATH).InnerText = SettingsData.Language.value.ToString(); //Перезаписываем значение в разделе Audio/Music

                settingsData_file.Save(settingsData_file_path); //Создаем или перезаписываем файл XML документа
            break;

            case ControlPers_BuildSettings.PlatformType.web_yandexGames_desktop:
            case ControlPers_BuildSettings.PlatformType.web_yandexGames_mobile_android:
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

    public ControlPers_LanguageHandler.GameLanguage SettingsData_LanguageValue
    {
        get { return (SettingsData.Language.value); }
        set { SettingsData.Language.value = value; }
    }

    #endregion

    private void Awake()
    {
        SingleOnScene = this;

        IsDataLoaded = false;
    }

    private void Start()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.PlatformType_Current)
        {
            case ControlPers_BuildSettings.PlatformType.windows:
                directory_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + DIRECTORY_NAME + @"\";

                var _progressData_FileName = ControlPers_BuildSettings.SingleOnScene.EncryptProgressFile ? PROGRESSDATA_FILE_ENCRYPTED_NAME : PROGRESSDATA_FILE_NAME;
                progressData_file_path = directory_path + _progressData_FileName;

                Directory_CreateIfNotExists(directory_path);

                ProgressData_Load();
                SettingsData_Load();

                IsDataLoaded = true;
            break;

            case ControlPers_BuildSettings.PlatformType.web_yandexGames_desktop:
            case ControlPers_BuildSettings.PlatformType.web_yandexGames_mobile_android:
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
