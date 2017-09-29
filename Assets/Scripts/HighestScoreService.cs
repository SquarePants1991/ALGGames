using UnityEngine;
using System.Collections;

public class HighestScoreService
{
	public static HighestScoreService sharedService = new HighestScoreService();
	public float highestScore;

	public HighestScoreService() {
		highestScore = PlayerPrefs.GetFloat ("HighestScore", 0);
	}

	public void SaveScore(float newScore) {
		if (newScore > highestScore) {
			highestScore = newScore;
			SaveHighestScore ();
		}
	}

	private void SaveHighestScore() {
		PlayerPrefs.SetFloat ("HighestScore", highestScore);
		PlayerPrefs.Save ();
	}
}

