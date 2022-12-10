using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AB_Script : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(LoadAB());
    }

    // Update is called once per frame
    void Update()
    {

    }

    List<string> ABFiles = new List<string>() { "StandaloneWindows" };

    private IEnumerator DownloadFile(string url, string savePath)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        FileStream fs = new FileStream(savePath, FileMode.Create);
        fs.Write(request.downloadHandler.data, 0, request.downloadHandler.data.Length);
        fs.Close();
    }

    private IEnumerator DownloadAllAB()
    {
        if (!Directory.Exists(Application.dataPath + @"\..\Download"))
        {
            Directory.CreateDirectory(Application.dataPath + @"\..\Download");
        }

        StartCoroutine(DownloadFile("http://81.68.87.60/StandaloneWindows.manifest", Application.dataPath + @"\..\Download\StandaloneWindows.manifest"));
        StartCoroutine(DownloadFile("http://81.68.87.60/StandaloneWindows.assetbundle", Application.dataPath + @"\..\Download\StandaloneWindows.assetbundle"));
        yield return new WaitForSeconds(2);
        AssetBundle ab = AssetBundle.LoadFromFile(Application.dataPath + @"\..\Download\StandaloneWindows.assetbundle");
        AssetBundleManifest abm = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] s = abm.GetAllAssetBundles();
        foreach (string name in s)
        {
            ABFiles.Add(name);
            StartCoroutine(DownloadFile("http://81.68.87.60/" + name + ".manifest", Application.dataPath + @"\..\Download\" + name + ".manifest"));
            StartCoroutine(DownloadFile("http://81.68.87.60/" + name + ".assetbundle", Application.dataPath + @"\..\Download\" + name + ".assetbundle"));
        }
    }

    private IEnumerator LoadAB()
    {
        StartCoroutine(DownloadAllAB());
        yield return new WaitForSeconds(4);
        //if (File.Exists(Application.streamingAssetsPath + @"\StandaloneWindows.assetbundle") && File.Exists(Application.streamingAssetsPath + @"\StandaloneWindows.manifest"))
        //{
        //    //Hash128 h = abm.GetAssetBundleHash(name);
        //    AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + @"\lua.assetbundle");
        //    string[] s = ab.GetAllAssetNames();
        //    TextAsset[] t = ab.LoadAllAssets<TextAsset>();
        //    for (int i = 0; i < t.Length; i++)
        //    {
        //        FileStream f = new FileStream(Application.dataPath + "/Resources/Battle/" + s[i].Split('/')[s[i].Split('/').Length - 1], FileMode.Create);
        //        f.Write(t[i].bytes, 0, t[i].bytes.Length);
        //        f.Close();
        //    }
        //    this.gameObject.AddComponent<LuaScript>().enabled = true;
        //}
        //else
        //{
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        if (!Directory.Exists(Application.streamingAssetsPath + "/Battle"))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Battle");
        }

        foreach (string name in ABFiles)
        {
            File.Copy(Application.dataPath + @"\..\Download\" + name + ".assetbundle", Application.streamingAssetsPath + @"\" + name + ".assetbundle", true);
            File.Copy(Application.dataPath + @"\..\Download\" + name + ".manifest", Application.streamingAssetsPath + @"\" + name + ".manifest", true);
        }
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + @"\lua.assetbundle");
        string[] s = ab.GetAllAssetNames();
        TextAsset[] t = ab.LoadAllAssets<TextAsset>();
        for (int i = 0; i < t.Length; i++)
        {
            FileStream f = new FileStream(Application.streamingAssetsPath + "/Battle/" + s[i].Split('/')[s[i].Split('/').Length - 1], FileMode.Create);
            f.Write(t[i].bytes, 0, t[i].bytes.Length);
            f.Close();
        }
        this.gameObject.AddComponent<LuaScript>();
    }
}

