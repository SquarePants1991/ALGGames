using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
	public GameController gameController;
	public Canvas gamingUICanvas;
	public Canvas indexUICanvas;
	public Text scoreText;
	// Use this for initialization
	void Start ()
	{
		gamingUICanvas.enabled = false;
		indexUICanvas.enabled = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreText.text = "" + ScoreService.sharedService.totalScore;
	}

	public void StartGameClicked() {
		gamingUICanvas.enabled = true;
		indexUICanvas.enabled = false;
		gameController.GameStart ();
	}
		
}

