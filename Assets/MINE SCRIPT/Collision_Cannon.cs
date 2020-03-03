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
    public GameObject BacksideIndicator;
    private Vector3 AvatarDirection;

    //Referanse til planeten så kanonene kan oppdatere status over antall levende mtp respawning. Satt fra planeten ved spawning.
    public PlanetLogic PlanetRef;

    private float AvatarDotProduct;

    private float AdjustedDotProduct;
    private float scaleLerp;

    private float SpawnTime;


    private int health = 5;


    //Skyting
    private float shootSignal = 2.0f;

    

    // Start is called before the first frame update
    void Start()
    {
        SpawnTime = Time.realtimeSinceStartup;
        ExplosionSound = GetComponent<AudioSource>();
        BacksideIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        

        //Bruker normaliserte dot-produktet av kanonenes og avatars
        //forward vektor til å sjekke om de skal skyte mot oss og rotere mot oss.

        AvatarDotProduct = Vector3.Dot(AvatarRigBase.transform.forward, transform.forward);
        AvatarDirection = AvatarRigBase.transform.position - Barrel.transform.position;

        //Slik at kanonene kun skyter mot spiller når man er over kanonens horisont
        if (AvatarDotProduct < -0.45f && Barrel)
        {
            Barrel.transform.rotation = Quaternion.LookRotation(AvatarDirection);

            if (Time.time > shootSignal)
            {
                Instantiate(Projectile, Barrel.transform.position + (Barrel.transform.forward * 10), Barrel.transform.rotation);
                shootSignal = Time.time + 1.0f;
            }
        }

        if (AvatarDotProduct > -0.3f)
        {
            AdjustedDotProduct = (AvatarDotProduct + 0.3f) / 1.3f;

            //Debug.Log("Direction vector normalized: " + AvatarDirection.normalized);

            BacksideIndicator.SetActive(true);
            BacksideIndicator.transform.position = transform.position + (AvatarDirection) / (2.8f) + AvatarDirection.normalized * Mathf.Lerp(1, 185, AdjustedDotProduct);

            scaleLerp = Mathf.Lerp(1.0f, 0.2f, AdjustedDotProduct) + Mathf.Abs(Mathf.Sin(Time.time * 4))*0.1f;
            BacksideIndicator.transform.localScale = new Vector3(scaleLerp, scaleLerp, scaleLerp);
        }
        else BacksideIndicator.SetActive(false);

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
            Destroy(BacksideIndicator);
            Destroy(this); }
    }
}
