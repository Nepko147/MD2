using UnityEngine;
using System.IO;
using System.Xml;
using System;

public class SaveLoader : MonoBehaviour
{
    public static SaveLoader Instance { get; private set; }
    private string filePath;
    private string directoryPath;

    private void Awake()
    {
        Instance = this;
        directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Midnight Drive\";
    }

    public void Save(int _parameter , string _parameterName)
    {       
        if (!Directory.Exists(directoryPath)) //�������� ������� ����� � �������
        {
            Directory.CreateDirectory(directoryPath); //������, ���� ����� ���
        }

        filePath = directoryPath + "Save.xml";
        if (!File.Exists(filePath)) //�������� ������������� ����� ����������
        {
            CreateSaveFile(filePath); //������, ���� ����� ���           
        }
        
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);
        xmlDoc.SelectSingleNode("gamesave/" + _parameterName).InnerText = "" + _parameter;
        xmlDoc.Save(filePath);
    }

    public int Load(string _parameterName)
    {
        filePath = directoryPath + "Save.xml";

        if (!File.Exists(filePath)) //�������� ������������� ����� ����������
        {
            CreateSaveFile(filePath); //������, ���� ����� ��� 
        }
        
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);
        return int.Parse(xmlDoc.SelectSingleNode("gamesave/" + _parameterName).InnerText);
    }

    private void CreateSaveFile(string _path)  //������� �������� ������ �����
    {
        if (!Directory.Exists(directoryPath)) //�������� ������� ����� � �������
        {
            Directory.CreateDirectory(directoryPath); //������, ���� ����� ���
        }

        XmlDocument xmlDoc = new XmlDocument();                 //������ ����� ��������
        XmlNode rootNode = xmlDoc.CreateElement("gamesave");    //������ ����� �������
        xmlDoc.AppendChild(rootNode);                           //��������� ���, ��� �������� ����

        XmlNode userNode; //��������� ��������� �������

        userNode = xmlDoc.CreateElement("complete"); //������ ����� �������
        userNode.InnerText = "1000000";                   //����� ��� ��������
        rootNode.AppendChild(userNode);             //��������� ���, ��� �������
                                                    //...
        userNode = xmlDoc.CreateElement("coins");
        userNode.InnerText = "0";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("volume");
        userNode.InnerText = "10";
        rootNode.AppendChild(userNode);

        xmlDoc.Save(_path); //��������� ��������
    }
}
