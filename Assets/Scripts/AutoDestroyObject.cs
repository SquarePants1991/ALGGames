using UnityEngine;
using System.Collections;

public class AutoDestroyObject : MonoBehaviour
{
	public float existInterval;
	// Use this for initialization
	void Start ()
	{

		StartCoroutine ("AutoDestruct");
	}

	IEnumerator AutoDestruct()
	{
		yield return new WaitForSeconds(existInterval);
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

