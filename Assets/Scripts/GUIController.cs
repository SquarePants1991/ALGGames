using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
	public GameController gameController;
	public Canvas gamingUICanvas;
	public Canvas indexUICanvas;
	public Canvas finishUICanvas;
	public Canvas rewardedAdsUICanvas;
	public Text scoreText;

	// Use this for initialization
	void Start ()
	{
		gamingUICanvas.enabled = false;
		finishUICanvas.enabled = false;
		indexUICanvas.enabled = true;
		rewardedAdsUICanvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreText.text = "" + LevelService.sharedService.totalScore + ", Life: " + LevelService.sharedService.healthPoint;
		if (gameController.isGameRunning) {
			if (LevelService.sharedService.healthPoint <= 0) {
				if (AdsService.sharedService.isRewardedVideoAdsReady ()) {
					gameController.GamePause ();
					rewardedAdsUICanvas.enabled = true;
				} else {
					gamingUICanvas.enabled = false;
					indexUICanvas.enabled = false;
					gameController.GameOver ();
					finishUICanvas.enabled = true;
				}
			}
		}
	}

	public void StartGameClicked() {
		gamingUICanvas.enabled = true;
		indexUICanvas.enabled = false;
		finishUICanvas.enabled = false;
		gameController.GameStart ();
	}

	public void WatchRewardedAdsClicked() {
		
		AdsService.sharedService.ShowRewardedAds ((UnityEngine.Advertisements.ShowResult obj) => {
			if (obj == UnityEngine.Advertisements.ShowResult.Finished) {
				LevelService.sharedService.healthPoint = 100;
				gameController.GameResume ();
				rewardedAdsUICanvas.enabled = false;
			}
		});
	}

	public void RefuseWatchRewardedAdsClicked() {
		rewardedAdsUICanvas.enabled = false;
		gamingUICanvas.enabled = false;
		indexUICanvas.enabled = false;
		gameController.GameOver ();
		finishUICanvas.enabled = true;
	}
		
}

