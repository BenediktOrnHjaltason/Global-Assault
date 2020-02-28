using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Projectile : MonoBehaviour
{
    public ParticleSystem ExplosionVFX;
    private AudioSource ExplosionSound;
    public float lifeSpan;
    private float timeAlive = 0;

    // Start is called before the first frame 
    void Start()
    {
        ExplosionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > lifeSpan) Destroy(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.name.StartsWith("Pro"))
        {
            Destroy(gameObject);
        }
    }
}
