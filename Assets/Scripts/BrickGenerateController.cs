using UnityEngine;
using System.Collections;

public class BrickGenerateController : MonoBehaviour
{
	public GameObject brickType;
	public GameObject bricksParent;

	public int brickRow = 5;
	public int brickCol = 5;
	public int brickSliceCount = 5;
	public Vector3 brickSize = new Vector3 (0.44f, 0.24f, 0.24f);
	public float brickSpeed = 0.2f;

	private GameObject[][] bricks;
	private bool brickIsGen = false;
	// Use this for initialization
	void Start ()
	{
	}

	public void GenBricks() {
		bricks = new GameObject[brickSliceCount][];
		for (int sliceIndex = 0; sliceIndex < brickSliceCount; ++sliceIndex) {
			bricks [sliceIndex] = new GameObject[brickRow * brickCol];
		}

		Vector3 centerPosition = new Vector3 (0, 0, 0);
		// Example Cube Brick Generator
		int row = brickRow;
		int col = brickCol;
		int slice = brickSliceCount;
		Vector3 basePosition = new Vector3 (centerPosition.x - col * brickSize.x * 0.5f, centerPosition.y, centerPosition.z - slice * brickSize.z * 0.5f);
		for (int sliceIndex = 0; sliceIndex < slice; ++sliceIndex) {
			GameObject[] brickSlice = bricks[sliceIndex];
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
					brickSlice [rowIndex * brickCol + colIndex] = brick;
				}
			}
		}
			
		brickIsGen = true;
	}

	public void DestroyAllBricks() {
		for (int sliceIndex = 0; sliceIndex < brickSliceCount; ++sliceIndex) {
			GameObject[] brickSlice = bricks[sliceIndex];
			for (int rowIndex = 0; rowIndex < brickRow; ++rowIndex) {
				for (int colIndex = 0; colIndex < brickCol; ++colIndex) {
					Destroy (brickSlice [rowIndex * brickCol + colIndex]);
				}
			}
		}
		bricks = null;
		brickIsGen = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (!brickIsGen) {
			return;
		}
		for (int sliceIndex = 0; sliceIndex < brickSliceCount; ++sliceIndex) {
			GameObject[] brickSlice = bricks[sliceIndex];
			int activeCount = 0;
			for (int rowIndex = 0; rowIndex < brickRow; ++rowIndex) {
				for (int colIndex = 0; colIndex < brickCol; ++colIndex) {
					GameObject brick = brickSlice [rowIndex * brickCol + colIndex];
					brick.transform.position += new Vector3 (0, 0, -Time.deltaTime * brickSpeed);
					if (brick.activeInHierarchy) {
						activeCount++;
					}
				}
			}
			if (activeCount <= 0) {
				MoveBrickSliceToTail (brickSlice);
			}
		}
	}

	void MoveBrickSliceToTail(GameObject[] moveBrickSlice) {
		float tailZ = moveBrickSlice [0].transform.position.z;
		for (int sliceIndex = 0; sliceIndex < brickSliceCount; ++sliceIndex) {
			GameObject[] brickSlice = bricks [sliceIndex];
			if (brickSlice [0].transform.position.z > tailZ) {
				tailZ = brickSlice [0].transform.position.z;
			}
		}
		float newPositionZ = tailZ + brickSize.z;
		for (int rowIndex = 0; rowIndex < brickRow; ++rowIndex) {
			for (int colIndex = 0; colIndex < brickCol; ++colIndex) {
				Vector3 pos = moveBrickSlice [rowIndex * brickCol + colIndex].transform.position;
				pos.z = newPositionZ;
				GameObject brick = moveBrickSlice [rowIndex * brickCol + colIndex];
				brick.transform.position = pos;
				brick.SetActive (true);
				Brick brickScript = brick.GetComponent<Brick> ();
				brickScript.setBrickType (brickScript.RandomBrickType());
			}	
		}
	}
}

