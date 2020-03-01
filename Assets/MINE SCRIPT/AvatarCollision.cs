using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarCollision : MonoBehaviour
{

    public ParticleSystem ExplosionFX;
    private AudioSource ExplosionSound;

    public int health;
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

        Destroy(other);

        ExplosionSound.Play();
        ExplosionFX.Play();
    }
}
