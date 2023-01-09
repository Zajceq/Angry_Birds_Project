using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

public class AssetBundlesManager : Singleton<AssetBundlesManager>
{
    public List<string> assetBundlesNames;
    private AssetBundle ab;
    public string assetBundleURL;
    public uint abVersion;
    //public string abVersionURL;

    private IEnumerator Start()
    {
        //yield return StartCoroutine(GetABVersion());
        foreach (var assetBundle in assetBundlesNames)
        {
            yield return StartCoroutine(LoadAssets(assetBundle, result => ab = result));
        }
        yield return StartCoroutine(LoadAssetsFromURL());
    }

    private IEnumerator LoadAssets(string name, Action<AssetBundle> bundle)
    {
        AssetBundleCreateRequest abcr;
        string path = Path.Combine(Application.streamingAssetsPath, name);
        abcr = AssetBundle.LoadFromFileAsync(path);
        yield return abcr;
        bundle.Invoke(abcr.assetBundle);
        Debug.LogFormat(abcr.assetBundle == null ? "Failed to load Asset Bundle : {0}" : "Asset Bundle {0} loaded", name);
    }

    public Sprite GetSprite(string assetName)
    {
        return ab.LoadAsset<Sprite>(assetName);
    }

    private IEnumerator LoadAssetsFromURL()
    {
        //UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleURL, abVersion, 0);
        UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleURL);
        //uwr.SetRequestHeader("Content-Type", "application/json");
        //uwr.SetRequestHeader("User-Agent", "DefaultBrowser");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            ab = DownloadHandlerAssetBundle.GetContent(uwr);
        }
        Debug.Log("Downloaded bytes: " + uwr.downloadedBytes);
        Debug.Log(ab == null ? "Failed to download Asset Bundle" : "Asset Bundle downloaded");
    }   
    
    //private IEnumerator GetABVersion()
    //{
    //    UnityWebRequest uwr = UnityWebRequest.Get(abVersionURL);
    //    uwr.SetRequestHeader("Content-Type", "application/json");
    //    uwr.SetRequestHeader("User-Agent", "DefaultBrowser");
    //    yield return uwr.SendWebRequest();
    //    if (uwr.isNetworkError || uwr.isHttpError)
    //    {
    //        Debug.Log(uwr.error);
    //    }
    //    Debug.Log(uwr.downloadHandler.text);
    //    abVersion = uint.Parse(uwr.downloadHandler.text);
    //}

    public void GetAndLoadNewScene(string assetName)
    {
        ab.LoadAsset(assetName);
        String[] assetList = ab.GetAllScenePaths();
        SceneManager.LoadSceneAsync(assetList[0]);
    }
}
