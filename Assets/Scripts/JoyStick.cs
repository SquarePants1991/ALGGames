using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour
{
	public Image stickBaseImage;
	public Image stickImage;
	public Canvas attachedCanvas;
	// Use this for initialization
	void Start ()
	{
		stickBaseImage.enabled = false;
		stickImage.enabled = false;
	}

	public float GetStickBaseRadius() {
		return stickBaseImage.rectTransform.rect.width / 2.0f * attachedCanvas.scaleFactor;
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	public void StickControlBegin(Vector2 position) {
		stickBaseImage.transform.position = new Vector3(position.x, position.y, 0.0f);
		stickImage.transform.position = new Vector3(position.x, position.y, 0.0f);

		stickBaseImage.enabled = true;
		stickImage.enabled = true;
	}

	public void StickControlMoved(Vector2 offset) {
		stickImage.transform.position = new Vector3(stickBaseImage.transform.position.x + offset.x, stickBaseImage.transform.position.y + offset.y, 0.0f);
	}

	public void StickControlEnd() {
		stickBaseImage.enabled = false;
		stickImage.enabled = false;
	}
}

