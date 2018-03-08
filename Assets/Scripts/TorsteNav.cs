using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsteNav : MonoBehaviour
{

	public Vector3[] positions;
	private int i = 0;
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Vector3.Lerp(transform.position, positions[i], Time.deltaTime);

		if (Vector3.Distance(transform.position, positions[i]) < 0.1f)
		{
			i = i >= positions.Length-1 ? 0 : i + 1;
		}
	}
}
