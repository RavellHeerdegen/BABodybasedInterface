using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class MenuCollisionHandler : MonoBehaviour
{

    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.name == "CollisionHelper")
        {
            GetComponent<SteamVR_LaserPointer>().active = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.transform.name == "CollisionHelper")
        {
            GetComponent<SteamVR_LaserPointer>().active = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.transform.name == "CollisionHelper")
        {
            GetComponent<SteamVR_LaserPointer>().active = true;
        }
    }
}
