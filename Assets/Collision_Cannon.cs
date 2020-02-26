using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Cannon : MonoBehaviour
{

    public GameObject ExplosionFX;
    public GameObject Platform;
    public GameObject Barrel;
    private AudioSource ExplosionSound;
    public GameObject AvatarRigBase;

    private Vector3 AvatarWorldSpace;
    private float AvatarDotProduct;
    private Quaternion LookTowardsAvatar;


    private int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        ExplosionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sjekka om jeg hadde riktig forward vektor fra avatar til midten av planeten. Skal bruke normalisert dot produktet av denne vektor og
        //forward vektor til kanonen til å sjekke om de skal skyte mot oss og rotere mot oss.
        //AvatarWorldSpace = AvatarRigBase.transform.TransformPoint(AvatarRigBase.transform.position);


        //Debug.DrawLine(AvatarWorldSpace, AvatarWorldSpace + AvatarRigBase.transform.forward * 50, new Color(1,0,0), 2.0f);

        AvatarDotProduct = -Vector3.Dot(AvatarRigBase.transform.forward, transform.forward);



        //if (AvatarDotProduct > 0.2f)
        //{


        LookTowardsAvatar = Quaternion.LookRotation(-AvatarRigBase.transform.forward, AvatarRigBase.transform.up);

        Debug.Log("Avatar forward: " + AvatarRigBase.transform.forward);

            Barrel.transform.SetPositionAndRotation(Barrel.transform.position, LookTowardsAvatar);
        //}
        
    }

    void OnTriggerEnter(Collider other)
    {
        other.enabled = false;

        
        Instantiate(ExplosionFX,transform.position, transform.rotation);
        ExplosionSound.Play();

        health--;
        Debug.LogWarning("Health left: " + health);

        if (health < 1) { 
            Destroy(Barrel);
            Destroy(Platform);
            Destroy(this); }
    }
}
