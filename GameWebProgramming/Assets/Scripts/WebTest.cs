using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class WebTest : MonoBehaviour
{
    string path = "file:///" + "D:\\Game\\DownLoad\\";
    string fileName = "DownLoadList.csv";
    void Start()
    {
        StartCoroutine(LoadPatch());
    }
    IEnumerator LoadPatch()
    {
        string path = "file:///" + "D:\\Game\\PatchInfo\\";
        string url = path + "DownLoadList.csv";        
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();  
        string patchDirectory = "D:\\Game\\DownLoad";
        if(!Directory.Exists(patchDirectory))
        {
            Directory.CreateDirectory(patchDirectory);            
        }
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            File.WriteAllText(patchDirectory, www.downloadHandler.text);
        }

        StreamReader sr = new StreamReader(patchDirectory + "/" + "DownLoadList.csv");
        while(sr != null)
        {
            string line = sr.ReadLine();
            string[] array = line.Split(',');
            string[] fileEntries = Directory.GetFiles(path + "/" + "Source");
            for (int i = 0; i < array.Length; i++)
            {
                foreach (string fileName in fileEntries)
                {
                    if (array[i].Equals(fileName))
                    {
                        File.WriteAllText(patchDirectory + "/" + "Patch", fileName);
                    }
                }
            }            
            
        }        
    }
    IEnumerator PatchFile(string fileName)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string url = "file:///" + "D:\\Game\\PatchInfo\\" + fileName;
        UnityWebRequest www = new UnityWebRequest(url);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();
        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            byte[] results = www.downloadHandler.data;
            File.WriteAllBytes(path +"/" +fileName, results);
        }
    }
    IEnumerator FileDownLoad()
    {
        using (StreamReader sr = new StreamReader(path + fileName))
        {
            string downloadFilename = string.Empty;
            while((downloadFilename = sr.ReadLine())!=null)
            {
                string url = "file:///" + path + downloadFilename;
                UnityWebRequest www = new UnityWebRequest(url);
                www.downloadHandler = new DownloadHandlerBuffer();
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    byte[] results = www.downloadHandler.data;
                    File.WriteAllBytes(path + "/" + fileName, results);
                }
            }
        }
    }
}
