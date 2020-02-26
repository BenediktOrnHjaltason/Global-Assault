using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Cannon : MonoBehaviour
{

    public GameObject ExplosionFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Cannon got hit(OnTrigger)");
        Instantiate(ExplosionFX,transform.position, transform.rotation);
    }
}
