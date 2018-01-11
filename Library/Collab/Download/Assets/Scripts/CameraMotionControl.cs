using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionControl : MonoBehaviour {

	static bool isRemote = false;
	private Transform gyroTransform;
	private Transform camTransform;
	public float startOrientation;

	// Use this for initialization
	void Start () {

		Application.targetFrameRate = 60;

		if (gyroTransform == null) {
			gyroTransform = GetComponent<Transform>();
		}

		#if UNITY_EDITOR
		isRemote = UnityEditor.EditorApplication.isRemoteConnected;
		#endif


		if (Application.isEditor && true) {
            Input.gyro.enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.gyro.enabled) {
			gyroTransform.rotation = Input.gyro.attitude;
			gyroTransform.transform.Rotate (0f, 0f, 180f, Space.Self); // Swap "handedness" of quaternion from gyro.
			gyroTransform.transform.Rotate (90f, startOrientation, 0f, Space.World);
		} else {
			gyroTransform.LookAt (gyroTransform.position+ Vector3.forward,Vector3.up);
		}
	}
}
