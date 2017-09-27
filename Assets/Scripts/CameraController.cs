using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
	public Camera playerCamera;	
	public float xSensitive = 1.0f;
	public float ySensitive = 1.0f;
	public Text sensorInfoText;

	private Vector3 mouseLastPosition;
	private float xAngle; 
	private float yAngle; 

	private float smoothTime = 0.1F;
	private float yVelocity = 0.0F;
	// Use this for initialization
	void Start ()
	{
		if (Application.isEditor) {
			mouseLastPosition = Input.mousePosition;
		} else {
			mouseLastPosition = Input.acceleration;
		} 
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!Application.isEditor) {
			Vector3 dir = Input.acceleration;
			Vector3 actualDir = dir - mouseLastPosition;
			// playerCamera.transform.forward = new Vector3(0, actualDir.z, 0);

			xAngle = Mathf.SmoothDamp(xAngle, -actualDir.z * 60, ref yVelocity, smoothTime);

			Quaternion xQuaternion = Quaternion.AngleAxis (xAngle, new Vector3(1, 0, 0));
			playerCamera.transform.rotation = xQuaternion;

			sensorInfoText.text = "xAngle: " + xAngle;
		} else {
			Vector3 newMousePosition = Input.mousePosition;
			yAngle += (newMousePosition.x - mouseLastPosition.x) * ySensitive;
			xAngle += -(newMousePosition.y - mouseLastPosition.y) * xSensitive;
			mouseLastPosition = newMousePosition;
			Quaternion xQuaternion = Quaternion.AngleAxis (xAngle, new Vector3(1, 0, 0));
			Quaternion yQuaternion = Quaternion.AngleAxis (yAngle, new Vector3 (0, 1, 0));
			playerCamera.transform.rotation = yQuaternion * xQuaternion;
		}
	}
}

