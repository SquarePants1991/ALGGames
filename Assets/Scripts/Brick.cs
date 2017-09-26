using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
	public enum BrickType {
		Normal,
		DoubleScore,
		TribbleScore,
		GatlinGun,
		PowerGun
	}
	public float score;
	public BrickType brickType;
	// Use this for initialization
	void Start ()
	{
		setBrickType (BrickType.Normal);
	}

	public void setBrickType(BrickType brickType) {
		this.brickType = brickType;
		Renderer renderer = gameObject.GetComponent<Renderer> ();
		switch (brickType) {
		case BrickType.DoubleScore:
			renderer.material.SetColor ("_MainColor", new Color(0x2b / 255.0f, 0x9e / 255.0f, 0xb3 / 255.0f));
			break;
		case BrickType.TribbleScore:
			renderer.material.SetColor ("_MainColor", new Color(0x44 / 255.0f, 0xaf / 255.0f, 0x69 / 255.0f));
			break;
		case BrickType.GatlinGun:
			renderer.material.SetColor ("_MainColor", new Color(0xfc / 255.0f, 0xab / 255.0f, 0x10 / 255.0f));
			break;
		case BrickType.PowerGun:
			renderer.material.SetColor ("_MainColor", new Color(0xf8 / 255.0f, 0x33 / 255.0f, 0x3c / 255.0f));
			break;
	
		}
	}

	public BrickType RandomBrickType() {
		float res = Random.Range (0, 100) / 100.0f;
		if (res > 0.98) {
			return BrickType.PowerGun;
		} else if (res > 0.95) {
			return BrickType.GatlinGun;
		} else if (res > 0.92) {
			return BrickType.TribbleScore;
		} else if (res > 0.9) {
			return BrickType.DoubleScore;
		}
		return BrickType.Normal;
	}

	public float GetScore() {
		switch (this.brickType) {
		case BrickType.DoubleScore:
			return 2 * score;
		case BrickType.TribbleScore:
			return 3 * score;
		case BrickType.GatlinGun:
			return 1000;
		case BrickType.PowerGun:
			return 1000;
		}
		return score;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter(Collision collisionOther)
	{
		if (collisionOther.gameObject.tag == "bullet") {
			Debug.Log ("Hit..." + collisionOther.gameObject.tag);
			ScoreService.sharedService.totalScore += GetScore ();
			Destroy (gameObject);
			Destroy (collisionOther.gameObject);
		}
	}
}

