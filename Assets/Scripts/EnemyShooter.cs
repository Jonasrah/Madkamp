using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

	public float shootingInterval;
	private float timer;
	public GameObject[] enemyProjectile;
	private GameObject clone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 dir = Camera.main.gameObject.transform.position - transform.position;
		Vector3 focus = Vector3.Cross(dir.normalized, Vector3.Cross(dir, Vector3.up));
		transform.LookAt(focus);
		
		timer += Time.deltaTime;

		if (timer > shootingInterval) {
			clone = Instantiate(enemyProjectile[Random.Range(0, enemyProjectile.Length)], transform.position + transform.up, transform.rotation);

			Rigidbody rb = clone.GetComponent<Rigidbody>();
			rb.velocity = dir;
			rb.velocity *= 2;

			timer = 0f;
		}
	}
}
