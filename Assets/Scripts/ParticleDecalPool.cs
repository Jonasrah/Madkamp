using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecalPool : MonoBehaviour {

    public int maxDecals = 100;
    public float decalSizeMin = .5f;
    public float decalSizeMax = 1.5f;

    private ParticleSystem decalParticleSystem;
    private int particleDecalDataIndex;
    private ParticleDecalData[] particleData;
    private ParticleSystem.Particle[] particles;
    private float amountOfProjectilePos = 0.6f;

    void Start () 
    {
        decalParticleSystem = GetComponent<ParticleSystem> ();
        particles = new ParticleSystem.Particle[maxDecals];
        particleData = new ParticleDecalData[maxDecals];
        for (int i = 0; i < maxDecals; i++) 
        {
            particleData [i] = new ParticleDecalData ();    
        }
    }

    public void ParticleHit(Collision hitCollider, Gradient colorGradient, float plausibleSizeOfDecal)
    {
        SetParticleData (hitCollider, colorGradient, plausibleSizeOfDecal);
        DisplayParticles ();
    }

    void SetParticleData(Collision hitCollider, Gradient colorGradient, float plausibleSizeOfDecal){

        ContactPoint contact = hitCollider.contacts[0];

        if (particleDecalDataIndex >= maxDecals) 
        {
            particleDecalDataIndex = 0;
        }
            
        Vector3 particlePos = contact.point;
        Vector3 particleRotationEuler = Quaternion.LookRotation (contact.normal).eulerAngles;
        particleRotationEuler.z = Random.Range (0, 360);
        particleData [particleDecalDataIndex].rotation = particleRotationEuler;

		particleData [particleDecalDataIndex].position = Vector3.Lerp(contact.point, contact.thisCollider.transform.position, amountOfProjectilePos);
        particleData [particleDecalDataIndex].size = Random.Range(
            (plausibleSizeOfDecal < decalSizeMin) ? plausibleSizeOfDecal : decalSizeMin,
			    (plausibleSizeOfDecal < decalSizeMax) ? plausibleSizeOfDecal : decalSizeMax);
        particleData [particleDecalDataIndex].color = colorGradient.Evaluate (Random.Range (0f, 1f));

        particleDecalDataIndex++;
    }

    void DisplayParticles()
    {
        for (int i = 0; i < particleData.Length; i++) 
        {
            particles [i].position = particleData [i].position;
            particles [i].rotation3D = particleData [i].rotation;
            particles [i].startSize = particleData [i].size;
            particles [i].startColor = particleData [i].color;
        }

        decalParticleSystem.SetParticles (particles, particles.Length);
    }
}