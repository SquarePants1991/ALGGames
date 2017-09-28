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
	}

	public void GameStart() {
		brickGenController.GenBricks ();
		gameControlController.enabled = true;
		game3DEnv.SetActive (true);
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
