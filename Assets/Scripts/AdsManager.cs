using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : Singleton<AdsManager>, IUnityAdsInitializationListener
{
    [SerializeField] string androidGameId;
    [SerializeField] string iOSGameId;
    [SerializeField] bool testMode = true;
    private string gameId;

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    [SerializeField] string androidAdUnitId = "banner";
    [SerializeField] string iOSAdUnitId = "banner";
    string adUnitId = null; // This will remain null for unsupported platforms.

    void Awake()
    {
        InitializeAds();
    }

    void Start()
    {
//        // Get the Ad Unit ID for the current platform:
//#if UNITY_IOS
//        _adUnitId = _iOSAdUnitId;
//#elif UNITY_ANDROID
//        _adUnitId = _androidAdUnitId;
//#endif
        Advertisement.Banner.SetPosition(_bannerPosition);
        LoadBanner();
    }

    public void InitializeAds()
    {
        gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOSGameId
            : androidGameId;
        Advertisement.Initialize(gameId, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(adUnitId, options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        //BannerOptions options = new BannerOptions
        //{
        //    clickCallback = OnBannerClicked,
        //    hideCallback = OnBannerHidden,
        //    showCallback = OnBannerShown
        //};

        // Show the loaded Banner Ad Unit:
        //Advertisement.Banner.Show(_adUnitId, options);
        adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOSAdUnitId
            : androidAdUnitId;
        Advertisement.Banner.Show(adUnitId);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }
}