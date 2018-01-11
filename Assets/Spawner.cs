using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float spawnInterval;
	private bool occupied;
	private float timer;
	public GameObject enemyPrefab;
	private GameObject enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;


		if (timer > spawnInterval) {
			if (!occupied) {
				enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
				enemy.transform.parent = transform;
				occupied = true;
			}

			timer = 0f;
		}

		if (enemy == null) {
			occupied = false;
		}
	}
}
