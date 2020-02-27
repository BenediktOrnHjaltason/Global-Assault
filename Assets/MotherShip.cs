﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    public ParticleSystem SmokeFX;
    public GameObject Mesh;
    public GameObject Light;


    public GameObject AvatarRigBase;
    public GameObject Missile;


    private float yPos = 0;
    private Vector3 NewPos;
    private float spawnTime;
    private float timeAlive;
    private bool bStoppedRising = false;
    private float deltaRise = 0.01f;


    private float missileTime;

    private int health = 9;


    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        timeAlive = Time.time - spawnTime;

        //Komme opp fra nordpol, så hover
        if (!bStoppedRising && transform.position.y < 265.0f)
        {
            deltaRise += Time.deltaTime * 0.2f;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(0, 267, deltaRise), transform.position.z);
            if (transform.position.y > 264) { bStoppedRising = true; missileTime = Time.time + 2; }
        }

        else transform.position = new Vector3(transform.position.x, ((Mathf.Sin((Time.time * 2)) * 6) + 265.0f), transform.position.z);

        
        //Skyte missil
        if (bStoppedRising && timeAlive > missileTime)
        {

            Instantiate(Missile, transform.position + new Vector3(0, 23, 0), Quaternion.LookRotation(transform.up)).GetComponent<Missile>().AvatarRigBase = AvatarRigBase;
            Instantiate(Missile, transform.position + AvatarRigBase.transform.right * 23, Quaternion.LookRotation(AvatarRigBase.transform.right)).GetComponent<Missile>().AvatarRigBase = AvatarRigBase;
            Instantiate(Missile, transform.position -AvatarRigBase.transform.right * 23, Quaternion.LookRotation(-AvatarRigBase.transform.right)).GetComponent<Missile>().AvatarRigBase = AvatarRigBase;

            missileTime += 10;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.LogError("MOTHERSHIP COLLIDING WITH: " + other.name);

        if (!other.name.Contains("Mis") && !other.name.Contains("Cyl")) { 
        other.enabled = false;
        Destroy(other);


        health--;

        if (health == 4) SmokeFX.Play();

        else if (health < 1)
        {
            foreach (Transform child in transform) GameObject.Destroy(child.gameObject);
            GameObject.Destroy(this);
        }

        }
    }
}