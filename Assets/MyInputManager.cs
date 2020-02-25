using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager : MonoBehaviour
{

    //Set in inspector
    public Component RightHandAnchor;
    public Component LeftHandAnchor;

    public Color debugColor = new Color(1, 0, 0);

    public LineRenderer RightHandRay;
    private Vector3[] LinePositions = new Vector3[2];

    public GameObject Gun;
    public GameObject AvatarRigBase;
    public GameObject AvatarSceneCompInner;
    public GameObject AvatarSceneCompOuter;

    private Vector3 AnglesToTurn;
    private Vector2 OurInput;
    public float InputMultiplier;

    private Vector3 Axis1 = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 Axis2 = new Vector3(1.0f, 0.0f, 0.0f);

    public GameObject Ball;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (OVRInput.GetDown(OVRInput.Button.One)) Debug.LogWarning("Controller A pressed");
        

        OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
        //Debug.LogWarning("Right Position: " + this.transform.position + OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand));
        //Debug.LogWarning("Left Potation: " + this.transform.position + OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand));

        //Debug.LogWarning("Right Hand Anchor Forward Vector: " + RightHandAnchor.transform.forward);
        //Debug.LogWarning("Left Hand Anchor Forward Vector: " + LeftHandAnchor.transform.forward);


        //Yes. Tegner debug line i editor fra kontrollernes forward vector :D
        Debug.DrawLine(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand), OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand)
        + RightHandAnchor.transform.forward * 50, debugColor);

        //Eller bare
        Debug.DrawLine(LeftHandAnchor.transform.position, LeftHandAnchor.transform.position
        + LeftHandAnchor.transform.forward * 50, debugColor);

        OurInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);


        if (OurInput.x != 0 || OurInput.y != 0)
        {
            AvatarSceneCompInner.transform.Rotate(0.0f, OurInput.x * InputMultiplier, 0, Space.World);
            AvatarSceneCompOuter.transform.Rotate(OurInput.y * InputMultiplier, 0.0f, 0.0f, Space.Self);
            
        }



        LinePositions[0] = RightHandAnchor.transform.position;
        LinePositions[1] = LinePositions[0] + RightHandAnchor.transform.forward * 50;

        RightHandRay.SetPositions(LinePositions);

        Gun.transform.position = RightHandAnchor.transform.position;
        Gun.transform.rotation = RightHandAnchor.transform.rotation;
    }
}
