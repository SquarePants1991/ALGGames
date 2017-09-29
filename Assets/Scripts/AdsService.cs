using System;
using UnityEngine.Advertisements;
using UnityEngine;

public class AdsService
{
	public static AdsService sharedService = new AdsService ();
	public static string AdsTypeVideo = "video";
	public static string AdsTypeRewardedVideo = "rewardedVideo";
	public AdsService ()
	{
	}

	public bool isRewardedVideoAdsReady() {
		return Advertisement.IsReady (AdsTypeRewardedVideo);
	}

	public void ShowRewardedAds(Action<ShowResult> resultCallback) {
		var options = new ShowOptions();
		options.resultCallback = resultCallback;
		if (Advertisement.IsReady(AdsTypeRewardedVideo)) {
			Advertisement.Show (AdsTypeRewardedVideo, options);
		}
	}

	public void CheckState ()
	{
		Debug.Log ("Unity Ads initialized: " + Advertisement.isInitialized);
		Debug.Log ("Unity Ads is supported: " + Advertisement.isSupported);
		Debug.Log ("Unity Ads test mode enabled: " + Advertisement.testMode);
	}
}