using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlanetLogic : MonoBehaviour
{

    public int startAmountCannons;

    public GameObject AvatarRigBase;
    public GameObject PFCannonBase;

    public List<GameObject> Cannons;


    // Start is called before the first frame update
    void Start()
    {
        SpawnCannons(startAmountCannons);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCannons(int number)
    {
        GameObject NewCannon;

        for (int i = 0; i < number; i++)
        {
            Vector3 Position = new Vector3(Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f)).normalized * 197;

            NewCannon = Instantiate(PFCannonBase, Position, Quaternion.LookRotation(Position));
            NewCannon.GetComponent<Collision_Cannon>().AvatarRigBase = this.AvatarRigBase;
            Cannons.Add(NewCannon);
        }
    }

}
