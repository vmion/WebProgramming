using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.IO;

public class AssetBundleGenerator
{
    [MenuItem("AssetBundle/SaveBundle")]
    static public void SaveBundle()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles_001_001",
            BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
    [MenuItem("AssetBundle/LoadBundle")]
    static public void LoadBundle()
    {        
        string path = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath,
            "*.*");
        if (string.IsNullOrEmpty(path))
            return;
        string[] fileNames = Directory.GetFiles(path);
        foreach (string fileName in fileNames)
        {
            //파일이름을 포함한 경로
            string url = path + "/" + fileName;
            //폴더에 있는 파일 중에서 확장자가 assetbundle인 파일만 로드
            string extension = Path.GetExtension(fileName);
            if(extension.Equals(".assetbundle"))
            {
                var bundleFile = AssetBundle.LoadFromFile(fileName);
                GameObject[] objs = bundleFile.LoadAllAssets<GameObject>();
                for (int i = 0; i < objs.Length; i++)
                {
                    GameObject obj = GameObject.Instantiate(objs[i]);
                }
                bundleFile.Unload(false);
                Resources.UnloadUnusedAssets();
            }            
        }
        
    }
}
