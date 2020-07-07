using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Banners : MonoBehaviour
{
    string banner = "banner";
    string iosGameID = "3467033";

    IEnumerator Start()
    {
        if (!GameController.instance.noAdsAnddoubleCoins)
        {
            Advertisement.Initialize(iosGameID);
            while (!Advertisement.IsReady(banner)) yield return null;

            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show(banner); 
        }
    }
}
