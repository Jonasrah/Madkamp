using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatOnCollision : MonoBehaviour {

    public Vector4 projectileColor;
    public ParticleDecalPool decalPool;
	private int numberOfRays = 4;
	private float raySteps = 0.025f;
	private Vector3[] rayDirections;

	Vector4 channelMask = new Vector4(1,0,0,0);

	int splatsX = 1;
	int splatsY = 1;

	public float splatScale = 1.0f;

    void Start () 
    {
		/*if (decalPool == null) {
			decalPool = GameObject.FindGameObjectWithTag("Splatter").GetComponent<ParticleDecalPool>();
		}
		rayDirections = new Vector3[numberOfRays];
		rayDirections[0] = Vector3.forward;
		rayDirections[1] = Vector3.back;
		rayDirections[2] = Vector3.left;
		rayDirections[3] = Vector3.right;*/
    }

	void OnCollisionEnter(Collision other){
		
		/*ContactPoint contact = other.contacts[0];


		float plausibleSizeOfDecal = decalPool.decalSizeMax;

		Vector3 perpendicular = Vector3.zero;
		Ray ray = new Ray(contact.point + contact.normal, -contact.normal);
		RaycastHit hit;
		if (other.collider.Raycast(ray, out hit, Vector3.Distance(contact.point, contact.point + contact.normal) +1f)) {
			perpendicular = (Vector3.Dot(Vector3.up, contact.normal) < 0.5f) ? hit.transform.up : hit.transform.forward;
		}

		Vector3 cross = Vector3.Cross(perpendicular, contact.normal);
		//Debug.DrawLine(contact.point + perpendicular, contact.point - perpendicular, Color.red, 4f);
		//Debug.DrawLine(contact.point + cross, contact.point - cross, Color.red, 4f);

		rayDirections[0] = perpendicular;
		rayDirections[1] = -perpendicular;
		rayDirections[2] = cross;
		rayDirections[3] = -cross;

		for (int i = 0; i < numberOfRays; i++) {
			bool isHit = true;
			float step = raySteps;
			float distanceFromContact = 0f;
			for (int j = 0; j < 40 && isHit; j++) {
				Vector3 rayOrigin = contact.point + contact.normal / 10 + rayDirections[i] * raySteps;
				Vector3 rayTarget = contact.point + -contact.normal / 10 + rayDirections[i] * raySteps;
				Ray rayray = new Ray(rayOrigin, -contact.normal / 10);
				Debug.DrawRay(rayOrigin, -contact.normal / 10, Color.blue, 2f);
				RaycastHit hithit;
				if (other.collider.Raycast(rayray, out hithit, Vector3.Distance(rayOrigin, rayTarget))
					&& Vector3.Distance(contact.point, hithit.point) * 2 < plausibleSizeOfDecal) {
					distanceFromContact = Vector3.Distance(contact.point, hithit.point) * 2;

				} else { isHit = false; }
				raySteps += step;
			}
			raySteps = step;
			if (distanceFromContact < plausibleSizeOfDecal) {
				plausibleSizeOfDecal = distanceFromContact;
			}
		}
		//print("size: " + plausibleSizeOfDecal);
        decalPool.ParticleHit(other, particleColorGradient, plausibleSizeOfDecal);*/
		SplatLocation(other.contacts[0]);
		Destroy(this.gameObject);
    }

	void SplatLocation (ContactPoint contact) {
		splatsX = SplatManagerSystem.instance.splatsX;
		splatsY = SplatManagerSystem.instance.splatsY;

		channelMask = projectileColor;

		Ray ray = new Ray(contact.point + contact.normal, -contact.normal);
			RaycastHit hit;
			if( contact.otherCollider.Raycast( ray, out hit, 100 ) ){
				
				Vector3 leftVec = Vector3.Cross ( hit.normal, Vector3.up );
				float randScale = Random.Range(0.5f,1.5f);
				
				GameObject newSplatObject = new GameObject();
				newSplatObject.transform.position = hit.point;
				if( leftVec.magnitude > 0.001f ){
					newSplatObject.transform.rotation = Quaternion.LookRotation( leftVec, hit.normal );
				}
				newSplatObject.transform.RotateAround( hit.point, hit.normal, Random.Range(-180, 180 ) );
				newSplatObject.transform.localScale = new Vector3( randScale, randScale * 0.5f, randScale ) * splatScale;

				Splat newSplat;
				newSplat.splatMatrix = newSplatObject.transform.worldToLocalMatrix;
				newSplat.channelMask = channelMask;

				float splatscaleX = 1.0f / splatsX;
				float splatscaleY = 1.0f / splatsY;
				float splatsBiasX = Mathf.Floor( Random.Range(0,splatsX * 0.99f) ) / splatsX;
				float splatsBiasY = Mathf.Floor( Random.Range(0,splatsY * 0.99f) ) / splatsY;

				newSplat.scaleBias = new Vector4(splatscaleX, splatscaleY, splatsBiasX, splatsBiasY );

				SplatManagerSystem.instance.AddSplat (newSplat);

				GameObject.Destroy( newSplatObject );
			}
	}

}