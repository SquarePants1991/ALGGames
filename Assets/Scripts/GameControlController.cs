using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControlController : MonoBehaviour
{
	public ShootController shootController;
	public Camera playerCamera;	
	public float xSensitive = 1.0f;
	public float ySensitive = 1.0f;
	public JoyStick joyStick;
	public Image shootButton;

	private Vector2 mouseLastPosition;
	private float xAngle; 
	private float yAngle; 
	private float maxDeltaDistance = 100;

	private int joyStickFingerId;
	// Use this for initialization
	void Start ()
	{
		maxDeltaDistance = joyStick.GetStickBaseRadius();
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			mouseLastPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		}
		joyStickFingerId = -1;
		shootButton.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (SystemInfo.deviceType == DeviceType.Handheld) {
			if (Input.touchCount > 0) {
				foreach (Touch touch in Input.touches) {
					if (touch.position.x < Screen.width / 2) {
						if (joyStickFingerId < 0 || touch.fingerId == joyStickFingerId) {
							joyStickFingerId = touch.fingerId;
							if (touch.phase == TouchPhase.Began) {
								mouseLastPosition = touch.position;
								joyStick.StickControlBegin (mouseLastPosition);
							} else if (touch.phase == TouchPhase.Moved) {
								Vector3 posDelta = touch.position - mouseLastPosition;
								float xAnglePercent = Mathf.Clamp (posDelta.x, -maxDeltaDistance, maxDeltaDistance) / maxDeltaDistance;
								float yAnglePercent = Mathf.Clamp (posDelta.y, -maxDeltaDistance, maxDeltaDistance) / maxDeltaDistance;
								xAngle -= 60 * yAnglePercent * Time.deltaTime;
								yAngle += 60 * xAnglePercent * Time.deltaTime;
								joyStick.StickControlMoved (new Vector2 (xAnglePercent * maxDeltaDistance, yAnglePercent * maxDeltaDistance));
							} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
								joyStick.StickControlEnd ();
								joyStickFingerId = -1;
							}
						}
					} else {
						if (touch.fingerId != joyStickFingerId) {
							if (touch.phase == TouchPhase.Began) {
								// fire
								Vector3 direction = playerCamera.transform.forward;
								shootController.Shoot (direction, playerCamera.transform.position);
								shootButton.transform.position = new Vector3 (touch.position.x, touch.position.y, 1.0f);
								shootButton.enabled = true;
							} else if (touch.phase == TouchPhase.Moved) {
								shootButton.transform.position = new Vector3 (touch.position.x, touch.position.y, 1.0f);
							} else if (touch.phase == TouchPhase.Ended) {
								shootButton.enabled = false;
							}

						}
					}

				}

			}
		} else {
//			Vector3 newMousePosition = Input.mousePosition;
//			yAngle += (newMousePosition.x - mouseLastPosition.x) * ySensitive;
//			xAngle += -(newMousePosition.y - mouseLastPosition.y) * xSensitive;
//			mouseLastPosition = newMousePosition;
			if (Input.GetMouseButtonDown(0)) {
				mouseLastPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				joyStick.StickControlBegin (mouseLastPosition);

				Vector3 direction = playerCamera.transform.forward;
				shootController.Shoot (direction, playerCamera.transform.position);
				shootButton.transform.position = new Vector3 (mouseLastPosition.x, mouseLastPosition.y, 1.0f);
				shootButton.enabled = true;
			} 

			if (Input.GetMouseButton (0)) {
				Vector3 posDelta = new Vector2 (Input.mousePosition.x, Input.mousePosition.y) - mouseLastPosition;
				float xAnglePercent = Mathf.Clamp (posDelta.x, -maxDeltaDistance, maxDeltaDistance) / maxDeltaDistance;
				float yAnglePercent = Mathf.Clamp (posDelta.y, -maxDeltaDistance, maxDeltaDistance) / maxDeltaDistance;
				xAngle -= 60 * yAnglePercent * Time.deltaTime;
				yAngle += 60 * xAnglePercent * Time.deltaTime;
				joyStick.StickControlMoved (new Vector2 (xAnglePercent * maxDeltaDistance, yAnglePercent * maxDeltaDistance));
			} else {
				joyStick.StickControlEnd ();
				shootButton.enabled = false;
			}
		}

		Quaternion xQuaternion = Quaternion.AngleAxis (xAngle, new Vector3(1, 0, 0));
		Quaternion yQuaternion = Quaternion.AngleAxis (yAngle, new Vector3 (0, 1, 0));
		playerCamera.transform.rotation = yQuaternion * xQuaternion;
	}
}

