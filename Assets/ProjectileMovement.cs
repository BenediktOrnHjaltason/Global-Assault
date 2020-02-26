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
        //Får ikke prosjektilet til å ødelegges?

        deltaTimeAccumulator += Time.deltaTime;

        transform.position += transform.forward * 4;

        if (deltaTimeAccumulator > lifeSpan) { Destroy(this); }
    }
}
