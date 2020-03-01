using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    public ParticleSystem SmokeFX;
    public ParticleSystem ExplosionFX;
    public GameObject Mesh;
    public GameObject Light;
    public AudioSource ExplosionSound;


    public GameObject AvatarRigBase;
    public GameObject Missile;

    //Satt fra planeten når den spawnes
    public PlanetLogic PlanetRef;

    private float timeAlive = 0;
    private bool bStoppedRising = false;
    private float deltaRise = 0.01f;
    


    private float missileTime;

    private int health = 9;


    // Start is called before the first frame update
    void Start()
    {
        ExplosionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        timeAlive += Time.deltaTime;

        //Komme opp fra nordpol, så hover
        if (!bStoppedRising && transform.position.y < 265.0f)
        {
            deltaRise += Time.deltaTime * 0.2f;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(0, 267, deltaRise), transform.position.z);
            if (transform.position.y > 264) { bStoppedRising = true; missileTime = timeAlive + 2; }
        }

        else transform.position = new Vector3(transform.position.x, ((Mathf.Sin((Time.time * 2)) * 6) + 265.0f), transform.position.z);

        
        //Skyte missil
        if (bStoppedRising && timeAlive > missileTime)
        {
            //Missiler må ha referanse til Avatar så de kan følge etter
            Instantiate(Missile, transform.position + new Vector3(0, 23, 0), Quaternion.LookRotation(transform.up)).GetComponent<Missile>().AvatarRigBase = AvatarRigBase;
            Instantiate(Missile, transform.position + AvatarRigBase.transform.right * 23, Quaternion.LookRotation(AvatarRigBase.transform.right)).GetComponent<Missile>().AvatarRigBase = AvatarRigBase;
            Instantiate(Missile, transform.position -AvatarRigBase.transform.right * 23, Quaternion.LookRotation(-AvatarRigBase.transform.right)).GetComponent<Missile>().AvatarRigBase = AvatarRigBase;

            missileTime = timeAlive + 15;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Mothership collided with: " + other.name);
        if (other.name.StartsWith("Proj")) { 
        other.enabled = false;
        Destroy(other);

        ExplosionFX.Play();
            ExplosionSound.Play();

        health--;

        if (health == 4) SmokeFX.Play();

        else if (health < 1)
        {
            PlanetRef.bMothershipIsAlive = false;
                Destroy(SmokeFX);
                Destroy(Mesh);
                Destroy(Light);
                GameObject.Destroy(this);
        }

        }
    }
}
