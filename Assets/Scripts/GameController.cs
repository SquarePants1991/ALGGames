using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class GameController : MonoBehaviour {
	public GameControlController gameControlController;
	public BrickGenerateController brickGenController;

	public Camera mainCamera;
	public GameObject game3DEnv;

	public bool enableARMode = false;
	public bool isGameRunning = false;



	// Use this for initialization
	void Start () {
		if (enableARMode) {
			EnableARMode ();
		} else {
			DisableARMode ();
		}
		gameControlController.enabled = false;
		game3DEnv.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isGameRunning) {
			
		}
	}

	public void GameStart() {
		isGameRunning = true;
		brickGenController.GenBricks ();
		gameControlController.Reset ();
		gameControlController.enabled = true;
		brickGenController.enabled = true;
		game3DEnv.SetActive (true);
		LevelService.sharedService.Reset ();
		Debug.LogWarning (LevelService.sharedService.healthPoint);
	}

	public void GamePause() {
		isGameRunning = false;
		gameControlController.enabled = false;
		brickGenController.enabled = false;
	}

	public void GameResume() {
		isGameRunning = true;
		gameControlController.enabled = true;
		brickGenController.enabled = true;
	}

	public void GameOver() {
		isGameRunning = false;
		brickGenController.DestroyAllBricks ();
		gameControlController.enabled = false;
		game3DEnv.SetActive (false);
		HighestScoreService.sharedService.SaveScore(LevelService.sharedService.totalScore);
	}

	void BulletHitBrick(GameObject brick) {
		Brick brickScript = brick.GetComponent<Brick> ();
		float score = brickScript.GetScore ();
		Debug.Log ("Got Score: " + score);
	}

	void EnableARMode() {
		mainCamera.GetComponent<UnityARVideo> ().enabled = true;
		mainCamera.clearFlags = CameraClearFlags.Depth;
		GetComponent<UnityARCameraManager> ().enabled = true;
	}

	void DisableARMode() {
		mainCamera.GetComponent<UnityARVideo> ().enabled = false;
		mainCamera.clearFlags = CameraClearFlags.Color;
		GetComponent<UnityARCameraManager> ().enabled = false;
	}
}
