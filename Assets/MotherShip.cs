using System.Collections;
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

    private float missileTime;

    private int health = 9;


    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        missileTime = spawnTime + 6;
    }

    // Update is called once per frame
    void Update()
    {

        timeAlive = Time.time - spawnTime;

        //Komme opp fra nordpol, så hover
        if (!bStoppedRising && transform.position.y < 265.0f)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(0, 267, Mathf.Pow((Time.time), 2)), transform.position.z);
            if (transform.position.y > 264) bStoppedRising = true;
        }

        else transform.position = new Vector3(transform.position.x, ((Mathf.Sin((Time.time * 2)) * 6) + 265.0f), transform.position.z);

        
        //Skyte missil
        if (timeAlive > missileTime)
        {

            Instantiate(Missile, transform.position + new Vector3(0, 23, 0), Quaternion.LookRotation(transform.up)).GetComponent<Missile>().AvatarRigBase = AvatarRigBase;

            missileTime += 10;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.LogError("MOTHERSHIP COLLIDING WITH: " + other.name);

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
