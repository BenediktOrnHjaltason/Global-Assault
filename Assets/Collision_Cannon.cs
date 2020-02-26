using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Cannon : MonoBehaviour
{

    public GameObject ExplosionFX;
    public GameObject Platform;
    public GameObject Barrel;
    private AudioSource ExplosionSound;


    private int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        ExplosionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
