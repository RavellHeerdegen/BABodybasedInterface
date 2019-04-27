using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BlindUIHandler : MonoBehaviour
{

    public GameObject CameraUITextElement;
    public Camera viveHeadset;

    private void Update()
    {
        // Check if this button is in viewport of camera
        Vector3 screenPoint = viveHeadset.WorldToViewportPoint(transform.position);
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
        {
            // Button is visible
            CameraUITextElement.SetActive(false);
        }
        else
        {
            // Button is not visible bot hovered
            if (GetComponentInParent<Interactable>().isHovering)
            {
                CameraUITextElement.SetActive(true);
            } else
            {
                CameraUITextElement.SetActive(false);
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("RightHand"))
    //    {

    //        Vector3 screenPoint = viveHeadset.WorldToViewportPoint(transform.position);
    //        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
    //        {
    //            // Button is visible
    //            CameraUITextElement.SetActive(false);
    //            GetComponentInParent<Interactable>().is
    //        }
    //        else
    //        {
    //            // Button is not visible
    //            if (inCollision)
    //            CameraUITextElement.SetActive(true);
    //        }
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    Vector3 screenPoint = viveHeadset.WorldToViewportPoint(transform.position);
    //    if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
    //    {
    //        // Button is visible and hand is not colliding
    //        CameraUITextElement.SetActive(false);
    //    }
    //    else
    //    {
    //        // Button is not visible
    //        if (inCollision)
    //        {
    //            CameraUITextElement.SetActive(true);
    //        } else
    //        {
    //            CameraUITextElement.SetActive(false);
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("RightHand"))
    //    {
    //        CameraUITextElement.SetActive(false);
    //    }
    //}
}
