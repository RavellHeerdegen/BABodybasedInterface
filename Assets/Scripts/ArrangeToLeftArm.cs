using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ArrangeToLeftArm : MonoBehaviour
{

    public Hand leftHand;

    // Start is called before the first frame update
    void Start()
    {
        // Position at start to the left arm behind the wrist
        transform.position = leftHand.transform.position + new Vector3(0.17f, 0.76f, -0.9f);
        // Rotate at start to lay UI on the arm
        transform.Rotate(130, -7, 7, Space.World);

    }

    // Update is called once per frame
    void Update()
    {
        // Fix position to the actual controller position
        // transform.position = leftHand.transform.position;
        // Fix rotation to actual controller rotation, but freeze in x and z
        // transform.eulerAngles = new Vector3(90, leftHand.transform.eulerAngles.y, 0);
    }
}
