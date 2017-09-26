using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class GameController : MonoBehaviour {
	public ShootController shootController;
	public BrickGenerateController brickGenController;

	public Camera mainCamera;

	public bool enableARMode = false;

	// Use this for initialization
	void Start () {
		if (enableARMode) {
			EnableARMode ();
		} else {
			DisableARMode ();
		}
		brickGenController.GenBricks ();
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.touches.Length > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)) {
			Debug.Log ("Shoot!!!" + mainCamera);	
			Vector3 direction = mainCamera.transform.forward;
			shootController.Shoot (direction,  mainCamera.transform.position);
		}
	}

	void BulletHitBrick(GameObject brick) {
		Brick brickScript = brick.GetComponent<Brick> ();
		float score = brickScript.GetScore ();
		Debug.Log ("Got Score: " + score);
	}

	void EnableARMode() {
		mainCamera.GetComponent<UnityARVideo> ().enabled = true;
		mainCamera.clearFlags = CameraClearFlags.Depth;
	}

	void DisableARMode() {
		mainCamera.GetComponent<UnityARVideo> ().enabled = false;
		mainCamera.clearFlags = CameraClearFlags.Color;
	}
}
