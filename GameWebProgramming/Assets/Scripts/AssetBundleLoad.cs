using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoad : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadBundle_New());
    }    
    IEnumerator LoadBundle_New()
    {
        string url = "file:///" + "Assets/AssetBundles_001_001/gamecharacter.assetbundle";
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return www.SendWebRequest();
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
        var prefabs = bundle.LoadAllAssets<GameObject>();
        foreach (GameObject one in prefabs)
        {
            GameObject obj = GameObject.Instantiate(one);
        }
        bundle.Unload(false);
        Resources.UnloadUnusedAssets();
    }
}
