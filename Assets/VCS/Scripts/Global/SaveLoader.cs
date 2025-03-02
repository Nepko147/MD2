using UnityEngine;
using System.IO;
using System.Text;

public class SaveLoader : MonoBehaviour
{
    public static SaveLoader Instance { get; private set; }
    private string filePath;
    private string directoryPath;
    private int record;
    private StreamReader dataReader;
    private StreamWriter dataWriter;

    private void Awake()
    {
        Instance = this;
        directoryPath = "C:/Midnight Drive/";
    }

    public void Save(int _hiScore , string _fileName)
    {       
        if (!Directory.Exists(directoryPath)) //�������� ������� ����� � �������
        {
            Directory.CreateDirectory(directoryPath); //������, ���� ����� ���
        }
        filePath = directoryPath + _fileName;
        if (!File.Exists(filePath)) //�������� ������������� ����� ����������
        {
            CreateSaveFile(filePath, _hiScore); //������, ���� ����� ���           
        }  else
        {   
            dataWriter = new StreamWriter(filePath);
            dataWriter.WriteLine(_hiScore); //��������� ����
            dataWriter.Close();             //����������� �� ����
        }      
    }

    public int Load(string _fileName)
    {
        filePath = directoryPath + _fileName;
        if (!File.Exists(filePath)) //�������� ������������� ����� ����������
        {
            return _fileName == "Settings.db" ? 10 : 0;
        }
        dataReader = new StreamReader(filePath);
        record = int.Parse(dataReader.ReadLine()); //��������� ���� ����������. ('Parse' ����� ��� �������� ������ � �����)
        dataReader.Close();                        //����������� �� ����
        return record;
    }

    private void CreateSaveFile(string _path, int _hiScore)  //������� �������� ������ �����
    {
        using (FileStream fs = File.Create(filePath))
        {
            byte[] info = new UTF8Encoding(true).GetBytes("" + _hiScore);
            fs.Write(info, 0, info.Length);
            fs.Close();
        }            
    }
}
