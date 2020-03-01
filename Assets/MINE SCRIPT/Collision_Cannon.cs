using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collision_Cannon : MonoBehaviour
{
    public ParticleSystem SmokeFX;
    public ParticleSystem ExplosionFX;
    private AudioSource ExplosionSound;
    public GameObject Platform;
    public GameObject Barrel;
    public GameObject AvatarRigBase;
    public GameObject Projectile;
    public GameObject Light;

    //Referanse til planeten så kanonene kan oppdatere status over antall levende mtp respawning. Satt fra planeten ved spawning.
    public PlanetLogic PlanetRef;

    private float AvatarDotProduct;

    private float SpawnTime;


    private int health = 5;


    //Skyting
    private float shootSignal = 2.0f;

    

    // Start is called before the first frame update
    void Start()
    {
        SpawnTime = Time.realtimeSinceStartup;
        ExplosionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bruker normaliserte dot-produktet av kanonenes og avatars
        //forward vektor til å sjekke om de skal skyte mot oss og rotere mot oss.
       
        AvatarDotProduct = Vector3.Dot(AvatarRigBase.transform.forward, transform.forward);

        //Slik at kanonene kun skyter mot spiller når man er over kanonens horisont
        if (AvatarDotProduct < -0.45f && Barrel)
        {
            Barrel.transform.rotation = Quaternion.LookRotation(AvatarRigBase.transform.position - Barrel.transform.position);

            if (Time.time > shootSignal)
            {
                Instantiate(Projectile, Barrel.transform.position + (Barrel.transform.forward * 10), Barrel.transform.rotation);
                shootSignal = Time.time + 1.0f;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        ExplosionFX.Play();
        ExplosionSound.Play();

        health--;

        if (health == 2) SmokeFX.Play();

        else if (health < 1) {

            --PlanetRef.CannonsAlive;

            Destroy(Barrel);
            Destroy(Platform);
            Destroy(SmokeFX);
            Destroy(Light);
            Destroy(this); }
    }
}
