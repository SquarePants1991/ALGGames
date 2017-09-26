using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public ShootController shootController;
	public BrickGenerateController brickGenController;

	public Camera mainCamera;

	// Use this for initialization
	void Start () {
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
}
