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
        if (!Directory.Exists(directoryPath)) //Проверка наличия папки с сейвами
        {
            Directory.CreateDirectory(directoryPath); //Создаём, если папки нет
        }
        filePath = directoryPath + _fileName;
        if (!File.Exists(filePath)) //Проверка существования файла сохранения
        {
            CreateSaveFile(filePath, _hiScore); //Создаём, если файла нет           
        }  else
        {   
            dataWriter = new StreamWriter(filePath);
            dataWriter.WriteLine(_hiScore); //Сохраняем файл
            dataWriter.Close();             //Отключаемся от него
        }      
    }

    public int Load(string _fileName)
    {
        filePath = directoryPath + _fileName;
        if (!File.Exists(filePath)) //Проверка существования файла сохранения
        {
            return _fileName == "Settings.db" ? 10 : 0;
        }
        dataReader = new StreamReader(filePath);
        record = int.Parse(dataReader.ReadLine()); //Считываем файл сохранения. ('Parse' нужен для перевода строки в число)
        dataReader.Close();                        //Отключаемся от него
        return record;
    }

    private void CreateSaveFile(string _path, int _hiScore)  //Процесс создания нового файла
    {
        using (FileStream fs = File.Create(filePath))
        {
            byte[] info = new UTF8Encoding(true).GetBytes("" + _hiScore);
            fs.Write(info, 0, info.Length);
            fs.Close();
        }            
    }
}
