using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinishUI : MonoBehaviour
{
	public Text highestScoreText;
	public Text currentScoreText;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		highestScoreText.text = "" + HighestScoreService.sharedService.highestScore;
		currentScoreText.text = "" + LevelService.sharedService.totalScore;
	}
}

