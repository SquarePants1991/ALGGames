using UnityEngine;
using System.Collections;

public class BrickGenerateController : MonoBehaviour
{
	public GameObject brickType;
	public GameObject bricksParent;
	// Use this for initialization
	void Start ()
	{

	}

	public void GenBricks() {
		Vector3 centerPosition = new Vector3 (0, 0, 0);
		// Example Cube Brick Generator
		Vector3 brickSize = new Vector3(0.44f, 0.24f,  0.24f);
		int row = 10;
		int col = 10;
		int slice = 10;
		Vector3 basePosition = new Vector3 (centerPosition.x - col * brickSize.x * 0.5f, centerPosition.y, centerPosition.z - slice * brickSize.z * 0.5f);
		for (int sliceIndex = 0; sliceIndex < slice; ++sliceIndex) {
			for (int rowIndex = 0; rowIndex < row; ++rowIndex) {
				for (int colIndex = 0; colIndex < col; ++colIndex) {
					Vector3 position = new Vector3 (basePosition.x + colIndex * brickSize.x + brickSize.x / 2, basePosition.y + rowIndex * brickSize.y + brickSize.y / 2, basePosition.z + sliceIndex * brickSize.z + brickSize.z / 2);
					position = position + bricksParent.transform.position;
					GameObject brick = Instantiate (brickType, position, Quaternion.identity);
					Brick brickScript = brick.GetComponent<Brick> ();

					brickScript.setBrickType (brickScript.RandomBrickType());
					if (bricksParent != null) {
						brick.transform.parent = bricksParent.gameObject.transform;
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

