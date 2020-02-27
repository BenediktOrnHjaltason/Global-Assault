using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float velocity;
    public GameObject AvatarRigBase;


    private float spawnTime;
    private float lerpTime = 0;
    private float aliveTime;

    private Quaternion StartRotation;
    private Vector3 AvatarTurnLocation;
    private Vector3 MissileTurnLocation;
    private bool turnLocationsTaken = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        StartRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * velocity * Time.deltaTime);

        aliveTime = Time.time - spawnTime;

        if (aliveTime < 4.5f && aliveTime > 2.5f)
        {

            lerpTime += Time.deltaTime * 0.5f;

            if (!turnLocationsTaken) { 
                AvatarTurnLocation = AvatarRigBase.transform.position; 
                MissileTurnLocation = transform.position;
                turnLocationsTaken = true;
            }
            Quaternion.Lerp(StartRotation, Quaternion.LookRotation((AvatarTurnLocation - MissileTurnLocation)), lerpTime);
        }
        else if (aliveTime > 4.5f) transform.rotation = Quaternion.LookRotation((AvatarRigBase.transform.position - transform.position).normalized);
        
    }
    void OnTriggerEnter(Collider other)
    {
        //Får lære mer kollisjonsfiltrering senere :P
        if (!other.name.Contains("Mot")) { 
        
            foreach (Transform child in transform) GameObject.Destroy(child.gameObject);
            GameObject.Destroy(this);
        }
    }

}