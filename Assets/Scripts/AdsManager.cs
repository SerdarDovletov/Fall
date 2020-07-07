using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdsManager : MonoBehaviour,IUnityAdsListener
{
    //string rewardedVideo = "rewardedVideo";  
    string rewardedVideo = "video";  
    string iosGameID = "3467033";
    public bool earnCoins, reviveVideo;

    public static AdsManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        reviveVideo = false;
        earnCoins = false;
        Advertisement.AddListener(this);
        Advertisement.Initialize(iosGameID);
    }

    public void ShowRewardedVideo() {
        if (GameController.instance.audioOn) GameController.instance.Select.Play();
        if (Advertisement.IsReady()){
            reviveVideo = true;
            Advertisement.Show(rewardedVideo);
        }
    }
    
    public void Earn50Coins (){
        if (GameController.instance.audioOn) GameController.instance.Select.Play();
        if (Advertisement.IsReady()){
            earnCoins = true;
            Advertisement.Show(rewardedVideo);
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardedVideo)
        {
            if (reviveVideo && !InterstitialAds.instance.afterDeadAd && (showResult == ShowResult.Finished || showResult == ShowResult.Skipped))
                GameController.instance.WatchVideo();
            if (earnCoins && !InterstitialAds.instance.afterDeadAd && (showResult == ShowResult.Finished || showResult == ShowResult.Skipped))
                GameController.instance.Earn50Coins();
        }
    }
    
    //Unneeded
    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }
}
