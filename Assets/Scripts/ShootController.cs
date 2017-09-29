using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{
	public GameObject bulletType;
	public float bulletSpeed = 5;
	public Image shootCenterImage;

	private double shootInterval;
	private double timeFromLastShoot;
	private Vector3 shootDirection;
	private Vector3 shootPosition;
	// Use this for initialization
	void Start ()
	{
		shootInterval = -1;
		shootDirection = new Vector3 (0, 1, 0);
	}

	
	// Update is called once per frame
	void Update ()
	{
		if (shootInterval > 0) {
			timeFromLastShoot += Time.deltaTime;
			if (timeFromLastShoot >= shootInterval) {
				ShootOnce (shootDirection, shootPosition);
				timeFromLastShoot = 0;
			}
		}
	}

	public void BeginShoot() {
		BufEffect contiShoot = LevelService.sharedService.GetBuf (BufEffectType.ContinuousShoot);
		if (contiShoot != null) {
			BeginIntervalShoot (1.0f / contiShoot.effectFactor);
		} else {
			Debug.Log ("Shoot Once" + shootDirection);
			ShootOnce (shootDirection, shootPosition);
		}
	}

	public void EndShoot() {
		EndIntervalShoot ();
	}

	public void ShootOnce(Vector3 direction, Vector3 fromPosition) {
		Vector3 genPosition = fromPosition;
		genPosition.x += direction.x * 1f;
		genPosition.y += direction.y * 1f;
		genPosition.z += direction.z * 1f;
		Quaternion rotation = Quaternion.LookRotation (direction);
		GameObject bullet = Instantiate (bulletType, genPosition, rotation);
		Bullet bulletScript = bullet.GetComponent<Bullet> ();
		if (bulletScript != null) {
			bulletScript.speed = new Vector3(direction.x * bulletSpeed, direction.y * bulletSpeed, direction.z * bulletSpeed);
		}
	}

	public void BeginIntervalShoot(double interval) {
		timeFromLastShoot = 0;
		shootInterval = interval;
		ShootOnce (shootDirection, shootPosition);
	}

	public void EndIntervalShoot() {
		timeFromLastShoot = 0;
		shootInterval = -1;
	}

	public void setShootPositonAndDirection(Vector3 direction, Vector3 position) {
		this.shootDirection = direction;
		this.shootPosition = position;
	}
}

