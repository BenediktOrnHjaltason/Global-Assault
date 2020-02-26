using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Cannon : MonoBehaviour
{
    
    public ParticleSystem ExplosionFX;
    public ParticleSystem SmokeFX;
    public GameObject Platform;
    public GameObject Barrel;
    private AudioSource ExplosionSound;
    public GameObject AvatarRigBase;
    public GameObject Projectile;

    private Vector3 AvatarWorldSpace;
    private float AvatarDotProduct;
    private Quaternion LookTowardsAvatar;

    private float SpawnTime;


    private int health = 5;


    //Eksplosjonen nekter å slutte å loope selv om den ikke skal!

    private bool bExplosionIsPlaying;
    private float timeAtStartOfExplosion;

    // Start is called before the first frame update
    void Start()
    {
        ExplosionSound = GetComponent<AudioSource>();

        SpawnTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        //Sjekka om jeg hadde riktig forward vektor fra avatar til midten av planeten. Skal bruke normalisert dot produktet av denne vektor og
        //forward vektor til kanonen til å sjekke om de skal skyte mot oss og rotere mot oss.
        //AvatarWorldSpace = AvatarRigBase.transform.TransformPoint(AvatarRigBase.transform.position);


        //Debug.DrawLine(AvatarWorldSpace, AvatarWorldSpace + AvatarRigBase.transform.forward * 50, new Color(1,0,0), 2.0f);



        AvatarDotProduct = -Vector3.Dot(AvatarRigBase.transform.forward, transform.forward);



        if (AvatarDotProduct > 0.2f)
        { 
            LookTowardsAvatar = Quaternion.LookRotation(-AvatarRigBase.transform.forward, AvatarRigBase.transform.up);

            Barrel.transform.SetPositionAndRotation(Barrel.transform.position, LookTowardsAvatar);
        }

        /*
        if (bExplosionIsPlaying && Time.time > timeAtStartOfExplosion + 4)
        {
            ExplosionFX.Stop();
            bExplosionIsPlaying = false;
        }
        */
    }

    void OnTriggerEnter(Collider other)
    {

        Debug.Log("Cannon was hit");
        other.enabled = false;
        Destroy(other);


        //Instantiate(ExplosionFX, transform.position + transform.forward * 10, transform.rotation);
        

        ExplosionSound.Play();
        ExplosionFX.Play();
        //bExplosionIsPlaying = true;
        //float timeAtStartOfExplosion = Time.time;

        health--;

        if (health == 2) SmokeFX.Play();

        else if (health < 1) { 
            Destroy(Barrel);
            Destroy(Platform);
            Destroy(SmokeFX);
            Destroy(ExplosionFX);
            Destroy(this); }
    }
}
