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

	public ParticleSystem valuableParticleSystem;
	public GameObject explosionEffectType;
	// Use this for initialization
	void Start ()
	{
		
	}

	public void setBrickType(BrickType brickType) {
		this.brickType = brickType;
		Renderer renderer = gameObject.GetComponent<Renderer> ();

		{
			valuableParticleSystem.gameObject.SetActive (false);
		}

		Color mainColor = Color.cyan;
		switch (brickType) {
		case BrickType.DoubleScore:
			mainColor = new Color (0x2b / 255.0f, 0x9e / 255.0f, 0xb3 / 255.0f);
			break;
		case BrickType.TribbleScore:
			mainColor = new Color(0x44 / 255.0f, 0xaf / 255.0f, 0x69 / 255.0f);
			break;
		case BrickType.GatlinGun:
			mainColor = new Color(0xfc / 255.0f, 0xab / 255.0f, 0x10 / 255.0f);
			break;
		case BrickType.PowerGun:
			mainColor = new Color(0xf8 / 255.0f, 0x33 / 255.0f, 0x3c / 255.0f);
			break;
		}
		ParticleSystem.MainModule main = valuableParticleSystem.main;
		main.startColor = mainColor;
		renderer.material.SetColor ("_MainColor", mainColor);
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
			CreateExplosionEffect ();
			Debug.Log ("Hit..." + collisionOther.gameObject.tag);
			LevelService.sharedService.totalScore += GetScore ();
			gameObject.SetActive (false);
			Destroy (collisionOther.gameObject);
		} else if (collisionOther.gameObject.tag == "finishLine") {
			CreateExplosionEffect ();
			gameObject.SetActive (false);
			LevelService.sharedService.healthPoint--;
		}
	}

	void CreateExplosionEffect() {
		Instantiate (explosionEffectType, gameObject.transform.position, Quaternion.identity);

	}
}

