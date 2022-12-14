using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XmlParser : MonoBehaviour
{
    string fileName = "Itemxml";
    void Start()
    {
        TextAsset txt = (TextAsset)Resources.Load<TextAsset>(fileName);
        //Debug.Log(txt.text);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txt.text);
        //태그로 구분된 데이터
        XmlNodeList _dataList = xmlDoc.SelectNodes("dataroot/TestItem");
        foreach(XmlNode node in _dataList)
        {
            Debug.Log("id = " + node.SelectSingleNode("id").InnerText);
            Debug.Log("name = " + node.SelectSingleNode("name").InnerText);
            Debug.Log("cost = " + node.SelectSingleNode("cost").InnerText);
        }
        //name태그 데이터만 출력
        XmlNodeList _nameList = xmlDoc.GetElementsByTagName("name");
        foreach(XmlNode node in _nameList)
        {
            Debug.Log("name태그 = " + node.InnerText);
        }
    }
    void Update()
    {
        
    }
}
