using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	public GameObject[] projectile;
	private GameObject clone;
	private Vector3 projectileStartPos;
	public float projectileSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 touchPos = Vector3.zero;
		if (Input.touchCount > 0) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
				touchPos = Input.GetTouch(0).position;
				touchPos.z = 0.5f;
				projectileStartPos = Camera.main.ScreenToWorldPoint(touchPos);
                clone = Instantiate(projectile[Random.Range(0, projectile.Length)], transform.position + transform.forward,
					transform.rotation) as GameObject;
				
				//clone.transform.LookAt(transform.position);

				Rigidbody rb = clone.GetComponent<Rigidbody>();
				rb.AddForce(rb.transform.forward * projectileSpeed);
        	}
		}
	}
}
