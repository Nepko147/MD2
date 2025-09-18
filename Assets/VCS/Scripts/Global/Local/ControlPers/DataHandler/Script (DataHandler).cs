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
        if (!Directory.Exists(_path)) //��������� ������� �����
        {
            Directory.CreateDirectory(_path); //������ �����
        }
    }

    #endregion

    #region Encryption

    private const string ENCRYPTION_PASSWORD = "SuperSecretKey";
    private const string ENCRYPTION_SALT = "Salt1234"; // "����" - ��� ������ ��������� ����� ����������

    private string Encryption_Encrypt(string _inputData)
    {
        Aes _aesAlgorhythm = Aes.Create(); // ������ �������� �������� ����������
        
        var _salt = Encoding.UTF8.GetBytes(ENCRYPTION_SALT); 
        var _deriveBytes = new Rfc2898DeriveBytes(ENCRYPTION_PASSWORD, _salt); //������ ���� ���������� ������������ �� ������ � "����"

        var _keyBytes = _deriveBytes.GetBytes(32); //����� �����
        var _saltBytes = _deriveBytes.GetBytes(16);  //����� "����"
        _aesAlgorhythm.Key = _keyBytes;
        _aesAlgorhythm.IV = _saltBytes;

        ICryptoTransform _encryptor = _aesAlgorhythm.CreateEncryptor(_aesAlgorhythm.Key, _aesAlgorhythm.IV); // ������ ������������

        MemoryStream _memoryStream = new MemoryStream(); //������ ��������� ����� � ������
        CryptoStream _cryptoStream = new CryptoStream(_memoryStream, _encryptor, CryptoStreamMode.Write); //������ � ��������� ������ ������, ����� ���������� � ������ ������
        StreamWriter _streamWriter = new StreamWriter(_cryptoStream); // ������ � ���� ��� "���������� ����������"...        
        _streamWriter.Write(_inputData); // �������
        _streamWriter.Close(); //���������
        var _encryptedString = Convert.ToBase64String(_memoryStream.ToArray()); //���������� ���������� ������������� ������ � Base64-������
        
        return _encryptedString;               
    }

    private string Encryption_Decrypt(string _encryptedText)
    {
        Aes _aesAlgorhythm = Aes.Create(); // ������ �������� �������� ����������
        
        var _salt = Encoding.UTF8.GetBytes(ENCRYPTION_SALT); 
        var _deriveBytes = new Rfc2898DeriveBytes(ENCRYPTION_PASSWORD, _salt); //������ ���� ���������� ������������ �� ������ � "����"

        var _keyBytes = _deriveBytes.GetBytes(32); //����� �����
        var _saltBytes = _deriveBytes.GetBytes(16);  //����� "����"
        _aesAlgorhythm.Key = _keyBytes;
        _aesAlgorhythm.IV = _saltBytes;

        ICryptoTransform _decryptor = _aesAlgorhythm.CreateDecryptor(_aesAlgorhythm.Key, _aesAlgorhythm.IV); // ������ ��������������

        byte[] _encryptedTextBytes = Convert.FromBase64String(_encryptedText); //���������� ���������� ������������� ������ � ����� �� Base64-������

        MemoryStream _memoryStream = new MemoryStream(_encryptedTextBytes); //������ ��������� ����� � ������
        CryptoStream _cryptoStream = new CryptoStream(_memoryStream, _decryptor, CryptoStreamMode.Read); //������ � ��������� ������ ������, ����� ������������ � ������ ������
        StreamReader _streamReader = new StreamReader(_cryptoStream); // ������ � ���� ��� "���������� ����������"...        
        var _decryptedString = _streamReader.ReadToEnd(); //��������������
        
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
        var _originNode = progressData_file.SelectSingleNode(ProgressData.ORIGINNODE); //��������� �������� ������

        if (progressData_file.SelectSingleNode(ProgressData.ORIGINNODE) == null) //��������� ������� ��������� �������
        {
            _originNode = progressData_file.CreateElement(ProgressData.ORIGINNODE); //������� �������� ������ ��� XML ���������
            progressData_file.AppendChild(_originNode); //���������� �������� ������ � XML ��������
        }

        var _node_statistics = progressData_file.SelectSingleNode(ProgressData.Statistics.PATH); //���� ������ "Statistics"

        if (_node_statistics == null) //��������� ������� ������� Statistics � ���������
        {
            _node_statistics = progressData_file.CreateElement(ProgressData.Statistics.NODE); //������� ������ Statistics ��� XML ���������
            _originNode.AppendChild(_node_statistics); //���������� ������ Statistics � �������� ������
        }

        if (progressData_file.SelectSingleNode(ProgressData.Statistics.ReviveNumber.PATH) == null) //��������� ������� ���� ReviveNumber � ���������
        {
            var _node_reviveNumber = progressData_file.CreateElement(ProgressData.Statistics.ReviveNumber.NODE); //������� ������ ReviveNumber ��� XML ���������
            _node_reviveNumber.InnerText = ProgressData.Statistics.ReviveNumber.DEFAULTVALUE.ToString(); //���������� �������� � ������ ReviveNumber
            _node_statistics.AppendChild(_node_reviveNumber); //���������� ������ ReviveNumber � ������ Statistics                    
        }

        if (progressData_file.SelectSingleNode(ProgressData.Statistics.ReviveNumberBest.PATH) == null) //��������� ������� ���� ReviveNumberBest � ���������
        {
            var _node_reviveNumberBest = progressData_file.CreateElement(ProgressData.Statistics.ReviveNumberBest.NODE); //������� ������ ReviveNumberBest ��� XML ���������
            _node_reviveNumberBest.InnerText = ProgressData.Statistics.ReviveNumberBest.DEFAULTVALUE.ToString(); //���������� �������� � ������ ReviveNumberBest
            _node_statistics.AppendChild(_node_reviveNumberBest); //���������� ������ ReviveNumberBest � ������ Statistics                    
        }

        if (progressData_file.SelectSingleNode(ProgressData.Statistics.CoinsTotal.PATH) == null) //��������� ������� ���� CoinsTotal � ���������
        {
            var _node_coinsTotal = progressData_file.CreateElement(ProgressData.Statistics.CoinsTotal.NODE); //������� ������ CoinsTotal ��� XML ���������
            _node_coinsTotal.InnerText = ProgressData.Statistics.CoinsTotal.DEFAULTVALUE.ToString(); //���������� �������� � ������ CoinsTotal
            _node_statistics.AppendChild(_node_coinsTotal); //���������� ������ CoinsTotal � ������ Statistics
        }

        if (progressData_file.SelectSingleNode(ProgressData.Statistics.CoinsSpentOnRevivals.PATH) == null) //��������� ������� ���� CoinsSpentOnRevivals � ���������
        {
            var _node_coinsSpentOnRevivals = progressData_file.CreateElement(ProgressData.Statistics.CoinsSpentOnRevivals.NODE); //������� ������ CoinsSpentOnRevivals ��� XML ���������
            _node_coinsSpentOnRevivals.InnerText = ProgressData.Statistics.CoinsSpentOnRevivals.DEFAULTVALUE.ToString(); //���������� �������� � ������ CoinsSpentOnRevivals
            _node_statistics.AppendChild(_node_coinsSpentOnRevivals); //���������� ������ CoinsSpentOnRevivals � ������ Statistics
        }

        if (progressData_file.SelectSingleNode(ProgressData.Statistics.Defeats.PATH) == null) //��������� ������� ���� Defeats � ���������
        {
            var _node_defeats = progressData_file.CreateElement(ProgressData.Statistics.Defeats.NODE); //������� ������ Defeats ��� XML ���������
            _node_defeats.InnerText = ProgressData.Statistics.Defeats.DEFAULTVALUE.ToString(); //���������� �������� � ������ Defeats
            _node_statistics.AppendChild(_node_defeats); //���������� ������ Defeats � ������ Statistics
        }

        if (progressData_file.SelectSingleNode(ProgressData.Statistics.TotalDrivings.PATH) == null) //��������� ������� ���� TotalDrivings � ���������
        {
            var _node_totalDrivings = progressData_file.CreateElement(ProgressData.Statistics.TotalDrivings.NODE); //������� ������ TotalDrivings ��� XML ���������
            _node_totalDrivings.InnerText = ProgressData.Statistics.TotalDrivings.DEFAULTVALUE.ToString(); //���������� �������� � ������ TotalDrivings
            _node_statistics.AppendChild(_node_totalDrivings); //���������� ������ TotalDrivings � ������ Statistics
        }

        var _node_upgrades = progressData_file.SelectSingleNode(ProgressData.Upgrades.PATH); //���� ������ "Upgrades"

        if (_node_upgrades == null) //��������� ������� ������� Upgrades � ���������
        {
            _node_upgrades = progressData_file.CreateElement(ProgressData.Upgrades.NODE); //������� ������ Upgrades ��� XML ���������
            _originNode.AppendChild(_node_upgrades); //���������� ������ Upgrades � �������� ������
        }

        if (progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreCoins.PATH) == null) //��������� ������� ���� MoreCoins � ���������
        {
            var _node_moreCoins = progressData_file.CreateElement(ProgressData.Upgrades.MoreCoins.NODE); //������� ������ MoreCoins ��� XML ���������
            _node_moreCoins.InnerText = ProgressData.Upgrades.MoreCoins.DEFAULTVALUE.ToString(); //���������� �������� � ������ MoreCoins
            _node_upgrades.AppendChild(_node_moreCoins); //���������� ������ MoreCoins � ������ Upgrades                    
        }

        if (progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreBonuses.PATH) == null) //��������� ������� ���� MoreBonuses � ���������
        {
            var _node_moreBonuses = progressData_file.CreateElement(ProgressData.Upgrades.MoreBonuses.NODE); //������� ������ MoreBonuses ��� XML ���������
            _node_moreBonuses.InnerText = ProgressData.Upgrades.MoreCoins.DEFAULTVALUE.ToString(); //���������� �������� � ������ MoreBonuses
            _node_upgrades.AppendChild(_node_moreBonuses); //���������� ������ MoreBonuses � ������ Upgrades                    
        }

        if (progressData_file.SelectSingleNode(ProgressData.Upgrades.CoinMagnet.PATH) == null) //��������� ������� ���� CoinMagnet � ���������
        {
            var _node_coinMagnet = progressData_file.CreateElement(ProgressData.Upgrades.CoinMagnet.NODE); //������� ������ CoinMagnet ��� XML ���������
            _node_coinMagnet.InnerText = ProgressData.Upgrades.MoreCoins.DEFAULTVALUE.ToString(); //���������� �������� � ������ CoinMagnet
            _node_upgrades.AppendChild(_node_coinMagnet); //���������� ������ CoinMagnet � ������ Upgrades
        }

        if (progressData_file.SelectSingleNode(ProgressData.Upgrades.Revive.PATH) == null) //��������� ������� ���� Revive � ���������
        {
            var _node_revive = progressData_file.CreateElement(ProgressData.Upgrades.Revive.NODE); //������� ������ Revive ��� XML ���������
            _node_revive.InnerText = ProgressData.Upgrades.MoreCoins.DEFAULTVALUE.ToString(); //���������� �������� � ������ Revive
            _node_upgrades.AppendChild(_node_revive); //���������� ������ Revive � ������ Upgrades
        }

        if (progressData_file.SelectSingleNode(ProgressData.Coins.PATH) == null) //��������� ������� ���� Coins � ���������
        {
            var _node_coins = progressData_file.CreateElement(ProgressData.Coins.NODE); //������� ������ Coins ��� XML ���������
            _node_coins.InnerText = ProgressData.Coins.value.ToString(); //���������� �������� � ������ Coins
            _originNode.AppendChild(_node_coins); //���������� ������ Coins � �������� ������
        }
    }

    public void ProgressData_Load()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.PlatformType_Current)
        {
            case ControlPers_BuildSettings.PlatformType.windows:

                progressData_file = new XmlDocument();

                try //�������� ��������� ����
                {
                    if (ControlPers_BuildSettings.SingleOnScene.EncryptProgressFile)
                    {
                        var _encryptedData = File.ReadAllText(progressData_file_path);
                        progressData_file.InnerXml = Encryption_Decrypt(_encryptedData);
                    }
                    else
                    {
                        progressData_file.Load(progressData_file_path); //���� ���� � �������, �� �� ����������
                    }
                }
                catch { } //���� ���, �� ����� ������� ��������. ��� ���� ����������. ����� ����������, ���� ��������� (���� ���� �� ���� � ����)

                ProgressData_File_Check();

                var _reviveNumber_text = progressData_file.SelectSingleNode(ProgressData.Statistics.ReviveNumber.PATH).InnerText; //��������� ������ �� �����
                ProgressData.Statistics.ReviveNumber.value = int.Parse(_reviveNumber_text); //������� "���������� �����������"
                var _reviveNumberBest_text = progressData_file.SelectSingleNode(ProgressData.Statistics.ReviveNumberBest.PATH).InnerText; //��������� ������ �� �����
                ProgressData.Statistics.ReviveNumberBest.value = int.Parse(_reviveNumberBest_text); //������� "���������� �����������" (������)
                var _coinsTotal_text = progressData_file.SelectSingleNode(ProgressData.Statistics.CoinsTotal.PATH).InnerText; //��������� ������ �� �����
                ProgressData.Statistics.CoinsTotal.value = int.Parse(_coinsTotal_text); //������� "����� �����"
                var _coinsSpentOnRevivals_text = progressData_file.SelectSingleNode(ProgressData.Statistics.CoinsSpentOnRevivals.PATH).InnerText; //��������� ������ �� �����
                ProgressData.Statistics.CoinsSpentOnRevivals.value = int.Parse(_coinsSpentOnRevivals_text); //������� "��������� ����� �� �����������"
                var _defeats_text = progressData_file.SelectSingleNode(ProgressData.Statistics.Defeats.PATH).InnerText; //��������� ������ �� �����
                ProgressData.Statistics.Defeats.value = int.Parse(_defeats_text); //������� "���������"
                var _totalDrivings_text = progressData_file.SelectSingleNode(ProgressData.Statistics.TotalDrivings.PATH).InnerText; //��������� ������ �� �����
                ProgressData.Statistics.TotalDrivings.value = int.Parse(_totalDrivings_text); //������� "����� �������"

                var _upgrades_moreCoins_text = progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreCoins.PATH).InnerText; //��������� ������ �� �����
                Enum.TryParse(_upgrades_moreCoins_text, out ProgressData.Upgrades.MoreCoins.value); //������� ��������� �������� "������ �����"
                var _upgrades_moreBonuses_text = progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreBonuses.PATH).InnerText; //��������� ������ �� �����
                Enum.TryParse(_upgrades_moreBonuses_text, out ProgressData.Upgrades.MoreBonuses.value); //������� ��������� �������� "������ �������"
                var _upgrades_coinMagnet_text = progressData_file.SelectSingleNode(ProgressData.Upgrades.CoinMagnet.PATH).InnerText; //��������� ������ �� �����
                Enum.TryParse(_upgrades_coinMagnet_text, out ProgressData.Upgrades.CoinMagnet.value); //������� ��������� �������� "������ ��� �����"
                var _upgrades_revive_text = progressData_file.SelectSingleNode(ProgressData.Upgrades.Revive.PATH).InnerText; //��������� ������ �� �����
                Enum.TryParse(_upgrades_revive_text, out ProgressData.Upgrades.Revive.value); //������� ��������� �������� "�� �� ���� ��"

                var _coins_text = progressData_file.SelectSingleNode(ProgressData.Coins.PATH).InnerText; //��������� ������ �� �����
                ProgressData.Coins.value = int.Parse(_coins_text); //������� �������
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

                progressData_file.RemoveAll(); // ������� ���� � �����������. �����, ����� � ���� �� ������ ������ ������� (������ �� ������� ������. �������� ���-�� �������������/�����������/�������)
                ProgressData_File_Check(); //������������� ��������� �����

                progressData_file.SelectSingleNode(ProgressData.Statistics.ReviveNumber.PATH).InnerText = ProgressData.Statistics.ReviveNumber.value.ToString(); //�������������� �������� � ������� Statistics/ReviveNumber
                progressData_file.SelectSingleNode(ProgressData.Statistics.ReviveNumberBest.PATH).InnerText = ProgressData.Statistics.ReviveNumberBest.value.ToString(); //�������������� �������� � ������� Statistics/ReviveNumberBest
                progressData_file.SelectSingleNode(ProgressData.Statistics.CoinsTotal.PATH).InnerText = ProgressData.Statistics.CoinsTotal.value.ToString(); //�������������� �������� � ������� Statistics/CoinsTotal
                progressData_file.SelectSingleNode(ProgressData.Statistics.CoinsSpentOnRevivals.PATH).InnerText = ProgressData.Statistics.CoinsSpentOnRevivals.value.ToString(); //�������������� �������� � ������� Statistics/CoinsSpentOnRevivals
                progressData_file.SelectSingleNode(ProgressData.Statistics.Defeats.PATH).InnerText = ProgressData.Statistics.Defeats.value.ToString(); //�������������� �������� � ������� Statistics/Defeats
                progressData_file.SelectSingleNode(ProgressData.Statistics.TotalDrivings.PATH).InnerText = ProgressData.Statistics.TotalDrivings.value.ToString(); //�������������� �������� � ������� Statistics/TotalDrivings

                progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreCoins.PATH).InnerText = ProgressData.Upgrades.MoreCoins.value.ToString(); //�������������� �������� � ������� Upgrades/MoreCoins
                progressData_file.SelectSingleNode(ProgressData.Upgrades.MoreBonuses.PATH).InnerText = ProgressData.Upgrades.MoreBonuses.value.ToString(); //�������������� �������� � ������� Upgrades/MoreBonuses
                progressData_file.SelectSingleNode(ProgressData.Upgrades.CoinMagnet.PATH).InnerText = ProgressData.Upgrades.CoinMagnet.value.ToString(); //�������������� �������� � ������� Upgrades/CoinMagnet
                progressData_file.SelectSingleNode(ProgressData.Upgrades.Revive.PATH).InnerText = ProgressData.Upgrades.Revive.value.ToString(); //�������������� �������� � ������� Upgrades/Revive

                progressData_file.SelectSingleNode(ProgressData.Coins.PATH).InnerText = ProgressData.Coins.value.ToString(); //�������������� �������� � ������� Coins

                if (ControlPers_BuildSettings.SingleOnScene.EncryptProgressFile)
                {
                    var _encryptedProgressData = Encryption_Encrypt(progressData_file.InnerXml);
                    File.WriteAllText(progressData_file_path, _encryptedProgressData);
                }
                else
                {
                    progressData_file.Save(progressData_file_path); //������� ��� �������������� ���� XML ���������
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
        var _originNode = settingsData_file.SelectSingleNode(SettingsData.ORIGINNODE); // ��������� �������� �����; //��������� �������� ������

        if (_originNode == null) //��������� ������� ��������� �������
        {
            _originNode = settingsData_file.CreateElement(SettingsData.ORIGINNODE); //������� �������� ������ ��� XML ���������
            settingsData_file.AppendChild(_originNode); //���������� �������� ������ � XML ��������
        }

        var _node_audio = settingsData_file.SelectSingleNode(SettingsData.Audio.PATH); //���� ������ "Audio"

        if (_node_audio == null) //��������� ������� ������� Audio
        {
            _node_audio = settingsData_file.CreateElement(SettingsData.Audio.NODE); //������� ������ Audio ��� XML ���������
            _originNode.AppendChild(_node_audio); //���������� ������ Audio � �������� ������
        }

        if (settingsData_file.SelectSingleNode(SettingsData.Audio.Sound.PATH) == null) //��������� ������� ���� Sound � ���������                 
        {
            var _node_sound = settingsData_file.CreateElement(SettingsData.Audio.Sound.NODE); //������� ������ Sound ��� XML ���������
            _node_sound.InnerText = SettingsData.Audio.Sound.DEFAULTVALUE.ToString(); //���������� �������� � ������ Sound
            _node_audio.AppendChild(_node_sound); //���������� � ���� ������ Sound                    
        }

        if (settingsData_file.SelectSingleNode(SettingsData.Audio.Music.PATH) == null) //��������� ������� ���� Music � ���������                 
        {
            var _node_musicValue = settingsData_file.CreateElement(SettingsData.Audio.Music.NODE); //������� ������ Music ��� XML ���������
            _node_musicValue.InnerText = SettingsData.Audio.Music.DEFAULTVALUE.ToString(); //���������� �������� � ������ Music
            _node_audio.AppendChild(_node_musicValue); //���������� � ���� ������ Music                    
        }

        if (settingsData_file.SelectSingleNode(SettingsData.Language.PATH) == null) //��������� ������� ���� Language � ��������� 
        {
            var _node_language = settingsData_file.CreateElement(SettingsData.Language.NODE); //������� ������ Language ��� XML ���������
            _node_language.InnerText = SettingsData.Language.DEFAULTVALUE.ToString(); //���������� �������� � ������ Language
            _originNode.AppendChild(_node_language); //���������� � ���� ������ Language                    
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

                try //�������� ��������� ����
                {
                    settingsData_file.Load(settingsData_file_path); //���� ���� � �������, �� �� ����������
                }
                catch { } //���� ���, �� ����� ��������� �� ���������. ��� ���� ����������. ����� ����������, ���� ��������� (���� ���� �� ���� � ����)

                SettingsData_File_Check(); //��������� ��������� �����                

                var _audio_soundValue_text = settingsData_file.SelectSingleNode(SettingsData.Audio.Sound.PATH).InnerText; //��������� ������ �� �����
                SettingsData.Audio.Sound.value = float.Parse(_audio_soundValue_text); //������ ��������� ������

                var _audio_musicValue_text = settingsData_file.SelectSingleNode(SettingsData.Audio.Music.PATH).InnerText; //��������� ������ �� �����
                SettingsData.Audio.Music.value = float.Parse(_audio_musicValue_text); //������ ��������� ������

                var _languageValue_text = settingsData_file.SelectSingleNode(SettingsData.Language.PATH).InnerText; //��������� ������ �� �����
                Enum.TryParse(_languageValue_text, out SettingsData.Language.value); //������ ����
            break;

            case ControlPers_BuildSettings.PlatformType.web_yandexGames_desktop:
            case ControlPers_BuildSettings.PlatformType.web_yandexGames_mobile_android:
                SettingsData.Audio.Sound.value = YG2.saves.SettingsData_Audio_Sound;
                SettingsData.Audio.Music.value = YG2.saves.SettingsData_Audio_Music;
                SettingsData.Language.value = YG2.saves.SettingsData_Language;
            break;
        }
    }

    public void SettingsData_Save()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.PlatformType_Current)
        {
            case ControlPers_BuildSettings.PlatformType.windows:
                Directory_CreateIfNotExists(directory_path);
                                
                settingsData_file.RemoveAll(); // ������� ���� � �����������. �����, ����� � ���� �� ������ ������ ������� (������ �� ������� ������. �������� ���-�� �������������/�����������/�������)
                SettingsData_File_Check(); //������������� ��������� �����

                settingsData_file.SelectSingleNode(SettingsData.Audio.Sound.PATH).InnerText = SettingsData.Audio.Sound.value.ToString(); //�������������� �������� � ������� Audio/Sound
                settingsData_file.SelectSingleNode(SettingsData.Audio.Music.PATH).InnerText = SettingsData.Audio.Music.value.ToString(); //�������������� �������� � ������� Audio/Music
                settingsData_file.SelectSingleNode(SettingsData.Language.PATH).InnerText = SettingsData.Language.value.ToString(); //�������������� �������� � ������� Audio/Music

                settingsData_file.Save(settingsData_file_path); //������� ��� �������������� ���� XML ���������
            break;

            case ControlPers_BuildSettings.PlatformType.web_yandexGames_desktop:
            case ControlPers_BuildSettings.PlatformType.web_yandexGames_mobile_android:
                YG2.saves.SettingsData_Audio_Sound = SettingsData.Audio.Sound.value;
                YG2.saves.SettingsData_Audio_Music = SettingsData.Audio.Music.value;
                YG2.saves.SettingsData_Language = SettingsData.Language.value;

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
