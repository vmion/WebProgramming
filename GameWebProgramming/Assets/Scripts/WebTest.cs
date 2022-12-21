using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class WebTest : MonoBehaviour
{    
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
}
