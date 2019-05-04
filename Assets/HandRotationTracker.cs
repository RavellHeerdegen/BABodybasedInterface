using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class HandRotationTracker : MonoBehaviour
{

    public Hand leftHand;

    // Last left controller coordinates
    Vector3 lastLeftControllerPosition;
    Quaternion lastLeftControllerRotation;

    // Current left controller pos and rot, current right controller pos and rot
    private Vector3 leftControllerPosition;
    private Quaternion leftControllerRotation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get current controller coordinates
        leftControllerPosition = leftHand.transform.position;
        leftControllerRotation = leftHand.transform.rotation;

        // Calculate difference between last and current position
        Quaternion difference = new Quaternion();
        difference = Quaternion.Inverse(lastLeftControllerRotation) * leftControllerRotation;

        transform.rotation *= new Quaternion(-difference.z, -difference.x * 0.0f, difference.y * 0.0f, 1);

        lastLeftControllerPosition = leftHand.transform.position;
        lastLeftControllerRotation = leftHand.transform.rotation;
    }
}
