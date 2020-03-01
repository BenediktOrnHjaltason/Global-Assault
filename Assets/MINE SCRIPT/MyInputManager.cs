using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager : MonoBehaviour
{

    //Set in inspector
    public Component RightHandAnchor;
    public Component LeftHandAnchor;

    private Color debugColor = new Color(1, 0, 0);

    public LineRenderer RightHandRay;
    private Vector3[] LinePositions = new Vector3[2];

    public GameObject Gun;
    public GameObject AvatarRigBase;
    public GameObject AvatarSceneCompInner;
    public GameObject AvatarSceneCompOuter;

    private Vector2 OurInput;
    public float InputMultiplier;

    private Vector3 Axis1 = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 Axis2 = new Vector3(1.0f, 0.0f, 0.0f);

    public GameObject Projectile;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) Instantiate(Projectile,
            RightHandAnchor.transform.position + RightHandAnchor.transform.forward * 8, RightHandAnchor.transform.rotation);

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (AvatarRigBase.transform.position.z + 10 > -250.0f)
                AvatarRigBase.transform.localPosition.Set(0, 0, AvatarRigBase.transform.position.z + 10);
        }



        //OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
        //Debug.LogWarning("Right Position: " + this.transform.position + OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand));
        //Debug.LogWarning("Left Potation: " + this.transform.position + OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand));

                //Debug.LogWarning("Right Hand Anchor Forward Vector: " + RightHandAnchor.transform.forward);
                //Debug.LogWarning("Left Hand Anchor Forward Vector: " + LeftHandAnchor.transform.forward);


                //Yes. Tegner debug line i editor fra kontrollernes forward vector :D
        //Debug.DrawLine(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand), OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand)
        //+ RightHandAnchor.transform.forward * 50, debugColor);

        //Eller bare
        //Debug.DrawLine(LeftHandAnchor.transform.position, LeftHandAnchor.transform.position
        //+ LeftHandAnchor.transform.forward * 50, debugColor);

        OurInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);


            AvatarSceneCompInner.transform.Rotate(0.0f, -OurInput.x * InputMultiplier * Time.deltaTime, 0, Space.World);
            AvatarSceneCompOuter.transform.Rotate(OurInput.y * InputMultiplier * Time.deltaTime, 0.0f, 0.0f, Space.Self);
            


        //--Siktelinjen til pistolen
        LinePositions[0] = RightHandAnchor.transform.position;
        LinePositions[1] = LinePositions[0] + RightHandAnchor.transform.forward * 50;

        RightHandRay.SetPositions(LinePositions);

        Gun.transform.position = RightHandAnchor.transform.position;
        Gun.transform.rotation = RightHandAnchor.transform.rotation;
    }
}
