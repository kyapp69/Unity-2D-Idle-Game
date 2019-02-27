using System.Collections;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;
using ShowResult = UnityEngine.Monetization.ShowResult;

public class AdsStart : MonoBehaviour {

    public CoinManager coinManager;

    private string bannerAdPlacementId = "BannerAd";
    private string basicAdPlacementId = "video";
    private string rewardAdPlacementId = "rewardedVideo";

    private const string AndroidGameId = "3020594";
    private const string IosGameId = "3020595";


    // Setup Ads Info
    public const string GameId = AndroidGameId;
    private const bool TestMode = true;


    void Start () {
        Monetization.Initialize (GameId, TestMode);
        ShowBannerAd();

    }

    public void InitRewardAd() {
        StartCoroutine(ShowRewardedAd());
    }

    IEnumerator ShowRewardedAd () {
        while (!Monetization.IsReady (rewardAdPlacementId)) {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent (rewardAdPlacementId) as ShowAdPlacementContent;

        if (ad != null) {
            ad.Show (RewardedAdFinished);
        }
    }

    void RewardedAdFinished (ShowResult result) {
        if (result == ShowResult.Finished) {
            Debug.LogWarning("Finished rewarded video");
            coinManager.rewardMultiplier();
        } else if (result == ShowResult.Skipped) {
            Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
        } else if (result == ShowResult.Failed) {
            Debug.LogWarning("Video failed to show");
        }
    }

    private IEnumerator ShowBasicAd () {
        while (!Monetization.IsReady (basicAdPlacementId)) {
            yield return new WaitForSeconds(0.25f);
        }
        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent (basicAdPlacementId) as ShowAdPlacementContent;
        if(ad != null) {
            ad.Show ();
        }
    }

    void ShowBannerAd () {
        Advertisement.Banner.Load(bannerAdPlacementId); // todo needed?
        // while (!Advertisement.IsReady (bannerPlacement)) {
        // yield return new WaitForSeconds (0.5f);
        // }
        Advertisement.Banner.Show (bannerAdPlacementId);
    }


}