using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

	public float shootingInterval;
	private float timer;
	public GameObject enemyProjectile;
	private GameObject clone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Camera.main.gameObject.transform.position);
		
		timer += Time.deltaTime;

		if (timer > shootingInterval) {
			clone = Instantiate(enemyProjectile, transform.position + transform.forward, transform.rotation);

			Rigidbody rb = clone.GetComponent<Rigidbody>();
			rb.velocity = Camera.main.gameObject.transform.position - transform.position;

			timer = 0f;
		}
	}
}
