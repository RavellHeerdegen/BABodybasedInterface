using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ArrangeToLeftArm : MonoBehaviour
{

    public Hand leftHand;
    public Hand rightHand;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(90, 120, 0, Space.World);
        transform.position = leftHand.transform.position + new Vector3(-2, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = leftHand.transform.position;
        
    }
}
