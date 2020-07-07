using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour
{
    string video = "video";
    string iosGameID = "3467033";
    public static InterstitialAds instance;
    public bool afterDeadAd;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        afterDeadAd = false;
        Advertisement.Initialize(iosGameID);
    }
    public void ShowInterstitial() {
        afterDeadAd = true;
            Advertisement.Show(video);
    }
}
