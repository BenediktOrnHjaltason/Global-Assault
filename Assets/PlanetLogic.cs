﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlanetLogic : MonoBehaviour
{

    public int startAmountCannons;

    public GameObject AvatarRigBase;
    public GameObject PFCannonBase;
    public GameObject Mothership;

    private float timeInterval;
    public int CannonsAlive;

    private float firstTimeLaunchMothership = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //timeInterval = Time.time + 7.0f;
        //SpawnCannons(startAmountCannons)

    }

    // Update is called once per frame
    void Update()
    {

        /*
        //Sjekke hvert 7. sekund
        if(Time.time > timeInterval)
        {
            if (CannonsAlive < 2) SpawnCannons(3 + (int)Random.Range(1, 5));


            timeInterval = Time.time + 7.0f;
        }
        */


        if (Time.time > firstTimeLaunchMothership)
        {

            //Mothership må ha referanse til Avatar for å kunne gi videre til missilene hun skyter ut
            Instantiate(Mothership, transform.position, transform.rotation).GetComponent<MotherShip>().AvatarRigBase = AvatarRigBase;

            firstTimeLaunchMothership = 1000.0f;

        }

    }

    void SpawnCannons(int number, bool firstTimeSpawn = false)
    {
        GameObject NewCannon;

        for (int i = 0; i < number; i++)
        {
            Vector3 Position = new Vector3(Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f)).normalized * 197;

            NewCannon = Instantiate(PFCannonBase, Position, Quaternion.LookRotation(Position));

            //Kanonene trenger referanse til spiller, så de kan kalkulere hvor de skal skyte
            NewCannon.GetComponent<Collision_Cannon>().AvatarRigBase = this.AvatarRigBase;

            //Kanonene trenger referanse til planeten, så de kan oppdatere tall over overlevende mtp spawning
            NewCannon.GetComponent<Collision_Cannon>().PlanetRef = this;

            CannonsAlive++;
        }
    }
}
