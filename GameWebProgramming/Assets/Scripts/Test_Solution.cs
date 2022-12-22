using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class Test_Solution : MonoBehaviour
{
    List<string> downloadList;
    void Start()
    {
        downloadList = new List<string>();
        PathFile("file:///" + "D:\\Game\\PatchInfo\\DownLoadList.csv");
        StartCoroutine(DownLoadAsset());
    }
        
    public void PathFile(string name)
    {
        //name은 경로와 확장자를 포함한 이름
        using(StreamReader sr = new StreamReader(name))
        {
            string line = sr.ReadLine();
            while((line = sr.ReadLine())!=null)
            {
                string[] datas = line.Split(',');
                //datas[0] : index
                //datas[1] : category
                //datas[2] : name
                string extent = string.Empty;
                if(datas[1].Equals("캐릭터"))
                {
                    extent = ".txt";
                }
                if (datas[1].Equals("배경"))
                {
                    extent = ".txt";
                }
                if (datas[1].Equals("테이블"))
                {
                    extent = ".csv";
                }
                string filename = datas[2] + extent;
                downloadList.Add(filename);
            }
            sr.Close();
        }
    }
    IEnumerator DownLoadAsset()
    {
        string url = string.Empty;
        string dstFolder = "D:\\Game\\DownLoad\\";
        foreach (string one in downloadList)
        {
            url = "file:///" + "D:\\Game\\PatchInfo\\" + one;
            UnityWebRequest www = new UnityWebRequest(url);
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            if(www.isDone)
            {
                byte[] results = www.downloadHandler.data;
                File.WriteAllBytes(dstFolder + "/" + one, results);
            }            
        }        
    }
    void Update()
    {
        
    }
}
