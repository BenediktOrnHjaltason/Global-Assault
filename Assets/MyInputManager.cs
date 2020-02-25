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

        Debug.DrawLine(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand), OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand)
        + LeftHandAnchor.transform.forward * 50, debugColor);

        
    }
}
