using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplatting : MonoBehaviour {

	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
