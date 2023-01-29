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
    public List<AssetBundle> assetBundlesList;
    private AssetBundle abURL;
    public string assetBundleURL;
    public uint abVersion;
    public string abVersionURL;
    public string SceneName;

    private IEnumerator Start()
    {
        yield return StartCoroutine(GetABVersion());
        //foreach (var assetBundle in assetBundlesNames)
        //{
        //    yield return StartCoroutine(LoadAssets(assetBundle, result => assetBundlesList.Add(result)));
        //}
        for (int i = 0; i < assetBundlesNames.Count; i++)
        {
            yield return StartCoroutine(LoadAssets(assetBundlesNames[i], result => assetBundlesList.Add(result)));
        }
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
        AssetBundle ab = assetBundlesList.Find(bundle => bundle.LoadAsset<Sprite>(assetName));
        return ab.LoadAsset<Sprite>(assetName);
    }

    private IEnumerator LoadAssetsFromURL()
    {
        UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleURL);
        yield return uwr.SendWebRequest();
        if (uwr.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            abURL = DownloadHandlerAssetBundle.GetContent(uwr);
        }
        Debug.Log("Downloaded bytes: " + uwr.downloadedBytes);
        Debug.Log(abURL == null ? "Failed to download Asset Bundle" : "Asset Bundle downloaded");
    }

    private IEnumerator GetABVersion()
    {
        UnityWebRequest uwr = UnityWebRequest.Get(abVersionURL);
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("User-Agent", "DefaultBrowser");
        yield return uwr.SendWebRequest();
        if (uwr.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(uwr.error);
        }
        Debug.Log(uwr.downloadHandler.text);
        abVersion = uint.Parse(uwr.downloadHandler.text);
    }

    public void GetAndLoadNewScene()
    {
        string[] assetArray = abURL.GetAllScenePaths();
        //foreach (var asset in assetArray)
        //{
        //    if (asset.Contains(SceneName))
        //    {
        //        SceneManager.LoadSceneAsync(asset);
        //    }
        //}
        for (int i = 0; i < assetArray.Length; i++)
        {
            if (assetArray[i].Contains(SceneName))
            {
                SceneManager.LoadSceneAsync(assetArray[i]);
            }
        }
    }
}
