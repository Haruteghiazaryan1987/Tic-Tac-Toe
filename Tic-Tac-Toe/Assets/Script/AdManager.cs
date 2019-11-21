using System;
using System.Collections;
using Assets.Script.Advertisement;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdManager : MonoBehaviour //,IUnityAdsListener
{
    public enum AdvertisementType
    {
        Unity,
        Admob
    }

    [SerializeField] private AdvertisementType advertisementType;

    private IAdvertisement advertisement;
    public void Init()
    {
        switch (advertisementType)
        {
            case AdvertisementType.Unity:
                advertisement=new UnityAdvertisement("",true);
                break;
            case AdvertisementType.Admob:
                advertisement = null;
                break;
        }
    }

    public void LoadAd(AdType adType)
    {
        advertisement.Load(adType);
    }
    
    public void ShowAd(AdType adType)
    {
        advertisement.Show(adType);
    }































    /*
    public event Action<ShowResult> ResultShow;
#if UNITY_ANDROID
    private string gameID = "3264824";
#endif
    public void Awake()
    {
//        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID,false);
    }

//    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult)
//    {
//        if (showResult == ShowResult.Finished)
//        {
//            print("ShowResult.Finished");
//        } 
//        else if (showResult == ShowResult.Skipped) 
//        {
//            print("ShowResult.Skipped");
//        } 
//        else if (showResult == ShowResult.Failed) 
//        {
//            print("ShowResult.Failed");
//        }
//    }
//    public void OnUnityAdsReady (string placementId) 
//    {
//        if (Advertisement.IsReady(placementId))
//        {
//            print("fff");
//            Advertisement.Show(placementId);
//        }
//    }

//    public void OnUnityAdsDidError(string message)
//    {
//         print("OnUnityAdsDidError");
//    }
//    public void OnUnityAdsDidStart(string placementId)
//    {
//        print("OnUnityAdsDidStart");
//    }
    
    public void ShowAd(string zone = "")
    {
        if (string.Equals (zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions ();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady (zone))
            Advertisement.Show (zone, options);
    }

    void AdCallbackhandler (ShowResult result)
    {
        ResultShow?.Invoke(result);

    }*/
}


