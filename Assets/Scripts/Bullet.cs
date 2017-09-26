using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public Vector3 speed;
	private Rigidbody rigidbody;
	// Use this for initialization
	void Start ()
	{
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (rigidbody != null) {
			rigidbody.velocity = speed;
		}
	}
}

