﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float velocity;
    public GameObject AvatarRigBase;
    public ParticleSystem ExplosionFX;
    public AudioSource ExplosionSound;
    public GameObject Mesh;
    public GameObject ThrustLight;
    public ParticleSystem Exhaust;

    private float spawnTime;
    private float lerpTime = 0;
    private float aliveTime;

    private Quaternion StartRotation;
    private Vector3 AvatarTurnLocation;
    private Vector3 MissileTurnLocation;
    private bool turnLocationsTaken = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        StartRotation = transform.rotation;
        ExplosionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * velocity * Time.deltaTime);

        aliveTime = Time.time - spawnTime;

        //Utskytning og følg etter spiller
        if (aliveTime < 4.5f && aliveTime > 2.5f)
        {

            lerpTime += Time.deltaTime * 0.5f;

            if (!turnLocationsTaken) { 
                AvatarTurnLocation = AvatarRigBase.transform.position; 
                MissileTurnLocation = transform.position;
                turnLocationsTaken = true;
            }
            transform.rotation = Quaternion.Lerp(StartRotation, Quaternion.LookRotation((
                AvatarRigBase.transform.position - transform.position)), lerpTime);
        }
        else if (aliveTime > 4.5f) 
            transform.rotation = (Quaternion.Lerp(Quaternion.LookRotation(transform.forward), 
            Quaternion.LookRotation(AvatarRigBase.transform.position - transform.position).normalized, 0.4f));
        
    }
    void OnTriggerEnter(Collider other)
    {
        //Får lære mer kollisjonsfiltrering senere :P
        if (!other.name.Contains("Mot")) {

            ExplosionFX.Play();
            ExplosionSound.Play();

            Destroy(Mesh);
            Destroy(ThrustLight);
            Destroy(Exhaust);
            GameObject.Destroy(this);
        }
    }

}