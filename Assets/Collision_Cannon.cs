using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collision_Cannon : MonoBehaviour
{
    public ParticleSystem SmokeFX;
    public GameObject Platform;
    public GameObject Barrel;
    public GameObject AvatarRigBase;
    public GameObject Projectile;
    public GameObject Light;

    //Referanse til planeten så kanonene kan oppdatere status over antall levende mtp spawning
    public PlanetLogic PlanetRef;

    private Canvas ScreenOverlay;

    private Vector3 AvatarWorldSpace;
    private float AvatarDotProduct;
    private Quaternion LookTowardsAvatar;
    private Vector3 CannonAvatarDirection;

    private float SpawnTime;


    private int health = 5;

    private Vector3 TwoDCoords;
    private Text OverlayX;

    public static int AliveCount = 0;



    //Skyting
    private float shootSignal = 2.0f;

    

    // Start is called before the first frame update
    void Start()
    {
        SpawnTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        //Sjekka om jeg hadde riktig forward vektor fra avatar til midten av planeten. Skal bruke normalisert dot produktet av denne vektor og
        //forward vektor til kanonen til å sjekke om de skal skyte mot oss og rotere mot oss.
        //AvatarWorldSpace = AvatarRigBase.transform.TransformPoint(AvatarRigBase.transform.position);

        //Debug.DrawLine(AvatarWorldSpace, AvatarWorldSpace + AvatarRigBase.transform.forward * 50, new Color(1,0,0), 2.0f);

        //TwoDCoords = Camera.main.WorldToScreenPoint(this.transform.position);
        //OverlayX.transform.localPosition.Set(TwoDCoords.x, TwoDCoords.y, 0.0f);

        
        AvatarDotProduct = -Vector3.Dot(AvatarRigBase.transform.forward, transform.forward);

        //Slik at kanonene kun skyter mot spiller når man er i fornuftig vinkel
        if (AvatarDotProduct > 0.45f)
        {


            CannonAvatarDirection = AvatarRigBase.transform.position - Barrel.transform.position;
            LookTowardsAvatar = Quaternion.LookRotation(CannonAvatarDirection/*, Vector3.Cross(CannonAvatarDirection, -CannonAvatarDirection)*/);

            Barrel.transform.SetPositionAndRotation(Barrel.transform.position, LookTowardsAvatar);

            if (Time.time > shootSignal)
            {
                Instantiate(Projectile, Barrel.transform.position + (Barrel.transform.forward * 10), Barrel.transform.rotation);
                shootSignal = Time.time + 1.0f;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {

        Debug.Log("Cannon was hit");
        other.enabled = false;
        Destroy(other);      



        health--;

        if (health == 2) SmokeFX.Play();

        else if (health < 1) {

            --PlanetRef.CannonsAlive;

            Destroy(Barrel);
            Destroy(Platform);
            Destroy(SmokeFX);
            Destroy(Light);
            GetComponent<Light>().enabled = false;
            Destroy(this); }
    }
}
