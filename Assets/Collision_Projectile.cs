using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Projectile : MonoBehaviour
{
    public ParticleSystem ExplosionVFX;
    private AudioSource ExplosionSound;

    // Start is called before the first frame 
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
        ExplosionVFX.Play();
        ExplosionSound.Play();

        enabled = false;
        Destroy(this);
    }
}
