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
        // Rotate at start to lay UI on the arm
        transform.Rotate(90, 0, 0, Space.World);
        // Position at start to the left arm behind the wrist
        transform.position = leftHand.transform.position + new Vector3(-5, -5, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Fix position to the actual controller position
        transform.position = leftHand.transform.position + new Vector3(-5, -5, 0);
        // Fix rotation to actual controller rotation, but freeze in x and z
        transform.eulerAngles = new Vector3(90, leftHand.transform.eulerAngles.y, 0);
    }
}
