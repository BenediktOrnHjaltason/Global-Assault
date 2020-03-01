using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float velocity;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * velocity * Time.deltaTime);

    }
}
