using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;
using TMPro;
public class AdMobAdsManager : MonoBehaviour
{
    private InterstitialAd interstitial;
    GameMan gameMan;

    bool shown;
    int adscnt = 1;
    float time = 0;

    public GameObject enterButton;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        adscnt = PlayerPrefs.GetInt("AdsCnt");
        PlayerPrefs.SetInt("AdsCnt", ++adscnt);
        RequestInterstitial();
        gameMan = FindObjectOfType<GameMan>();

        GameOver();
    }
    void Update()
    {
        if (shown == false)
        {
            GameOver();
        }
    }
    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-2080541969257119/3432161434";
        #elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
                string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
    }

    private void GameOver()
    {
        time += Time.deltaTime;
        if (this.interstitial.IsLoaded() || time > 4)
		{
            shown = true;
            enterButton.SetActive(true);
            this.interstitial.Show();
            time = 0;
        }
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
    }

}
