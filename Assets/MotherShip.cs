using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{

    private AudioSource ExplosionSound;
    public ParticleSystem ExplosionFX;
    public ParticleSystem SmokeFX;
    public GameObject Mesh;
    public GameObject Light;


    public GameObject AvatarRigBase;



    private float yPos = 0;
    private Vector3 NewPos;
    private float timeAtSpawn;
    private bool bStoppedRising = false;

    private int health = 9;

    // Start is called before the first frame update
    void Start()
    {
        ExplosionSound = GetComponent<AudioSource>();
        timeAtSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (!bStoppedRising && transform.position.y < 265.0f)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(0, 267, Mathf.Pow((Time.time), 2)), transform.position.z);
            if (transform.position.y > 264) bStoppedRising = true;
        }

        else transform.position = new Vector3(transform.position.x, ((Mathf.Sin((Time.time * 2)) * 6) + 265.0f), transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.LogError("MOTHERSHIP COLLIDING WITH: " + other.name);

        other.enabled = false;
        Destroy(other);

        //ExplosionSound.Play();
        //ExplosionFX.Play();


        health--;

        if (health == 4) SmokeFX.Play();

        else if (health < 1)
        {
            Destroy(Mesh);
            Destroy(Light);
            Destroy(SmokeFX);
            Destroy(ExplosionFX);
            Destroy(GetComponent<Light>());
            Destroy(this);
        }
    }
}
