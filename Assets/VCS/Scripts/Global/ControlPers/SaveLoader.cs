using UnityEngine;
using System.IO;
using System.Xml;
using System;

public class ControlPers_SaveLoader : MonoBehaviour
{
    public static ControlPers_SaveLoader Singletone { get; private set; }
    private string filePath;
    private string directoryPath;

    private void Awake()
    {
        Singletone = this;
        directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Midnight Drive\";
    }

    public void Save(int _value , string _key)
    {       
        if (!Directory.Exists(directoryPath)) //Проверка наличия папки с сейвами
        {
            Directory.CreateDirectory(directoryPath); //Создаём, если папки нет
        }

        filePath = directoryPath + "Save.xml";
        if (!File.Exists(filePath)) //Проверка существования файла сохранения
        {
            CreateSaveFile(filePath); //Создаём, если файла нет           
        }
        
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);
        xmlDoc.SelectSingleNode("gamesave/" + _key).InnerText = "" + _value;
        xmlDoc.Save(filePath);
    }

    public int Load(string _key)
    {
        filePath = directoryPath + "Save.xml";

        if (!File.Exists(filePath)) //Проверка существования файла сохранения
        {
            CreateSaveFile(filePath); //Создаём, если файла нет 
        }
        
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);
        return int.Parse(xmlDoc.SelectSingleNode("gamesave/" + _key).InnerText);
    }

    private void CreateSaveFile(string _path)  //Процесс создания нового файла
    {
        if (!Directory.Exists(directoryPath)) //Проверка наличия папки с сейвами
        {
            Directory.CreateDirectory(directoryPath); //Создаём, если папки нет
        }

        XmlDocument xmlDoc = new XmlDocument();                 //Создаём новый документ
        XmlNode rootNode = xmlDoc.CreateElement("gamesave");    //Создаём новый элемент
        xmlDoc.AppendChild(rootNode);                           //Добавляем его, как корневой узел

        XmlNode userNode; //Объявляем экземпляр подузла

        userNode = xmlDoc.CreateElement("complete"); //Создаём новый элемент
        userNode.InnerText = "1000000";                   //Задаём ему значение
        rootNode.AppendChild(userNode);             //Добавляем его, как подузел
                                                    //...
        userNode = xmlDoc.CreateElement("coins");
        userNode.InnerText = "0";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("volume");
        userNode.InnerText = "10";
        rootNode.AppendChild(userNode);

        xmlDoc.Save(_path); //Сохраняем документ
    }
}
