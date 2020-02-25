using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager : MonoBehaviour
{
    public Vector3 LeftControllerWorldLocation;
    public Vector3 RightControllerWorldLocation;

    public Quaternion LeftControllerRotation;
    public Quaternion RightControllerRotation;

    //Set in inspector
    public Component RightHandAnchor;
    public Component LeftHandAnchor;

    public Color debugColor = new Color(1, 0, 0);

    public LineRenderer RightHandRay;
    private Vector3[] LinePositions = new Vector3[2];

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

        LinePositions[0] = RightHandAnchor.transform.position;
        LinePositions[1] = LinePositions[0] + RightHandAnchor.transform.forward * 50;

        RightHandRay.SetPositions(LinePositions);
        
    }
}
