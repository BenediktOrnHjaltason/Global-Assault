using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float lifeSpan;
    private float deltaTimeAccumulator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deltaTimeAccumulator += Time.deltaTime;

        transform.position += transform.forward * 2;

        if (deltaTimeAccumulator > lifeSpan) DestroyImmediate(this, true);

    }
}
