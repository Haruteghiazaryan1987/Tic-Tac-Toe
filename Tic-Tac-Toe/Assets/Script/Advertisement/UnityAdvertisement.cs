using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Assets.Script.Advertisement
{
    public sealed class UnityAdvertisement:IAdvertisement,IUnityAdsListener
    {
        public event Action<ShowResult> ResultShow;
        private readonly string gameId;
        private bool testMode;

        private string interstitialAd = "rewardedVideo";
        private string rewardedAd = "video";
        
        public UnityAdvertisement(string gameId)
        {
            this.gameId = gameId;
            Init();
        }

        public UnityAdvertisement(string gameId, bool testMode):this(gameId)
        {
            this.testMode = testMode;
        }

        public void Init()
        {
            Debug.Log(("mtav init"));
            UnityEngine.Advertisements.Advertisement.AddListener (this);
            UnityEngine.Advertisements.Advertisement.Initialize(gameId,testMode);   
        }

        public void Load(string adId)
        {
            if (adId.IsNullOrEmpty())
            {
                Debug.LogError("Ad is is null or empty.");
                return;
            }

            UnityEngine.Advertisements.Advertisement.Load(adId);
        }

        public bool IsReady(string adId)
        {
            return UnityEngine.Advertisements.Advertisement.IsReady(adId);
        }

        public void Show(string adId)
        {
            UnityEngine.Advertisements.Advertisement.Show(adId);
        }

        public void OnUnityAdsReady(string placementId)
        {
           
        }

        public void OnUnityAdsDidError(string message)
        {
          
        }

        public void OnUnityAdsDidStart(string placementId)
        {
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            ResultShow?.Invoke(showResult);
        }

        private void showAd(string id)
        {
            if (!IsReady(id))
            {
                Debug.LogError("Ad is not ready.");
                return;
            }
            
            Show(id);
        }

        private void loadAd(string id)
        {
            Load(id);
        }
        public void Show(AdType adType)
        {
            switch (adType)
            {
                case AdType.Interstitial:
                    showAd(interstitialAd);
                    break;
                case AdType.Rewarded:
                    showAd(rewardedAd);
                    break;
            }
        }

        public void Load(AdType adType)
        {
            switch (adType)
            {
                case AdType.Interstitial:
                    loadAd(interstitialAd);
                    break;
                case AdType.Rewarded:
                    loadAd(rewardedAd);
                    break;
            }
        }
    }
}