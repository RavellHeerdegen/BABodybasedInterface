using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BlindUIHandler : MonoBehaviour
{

    public GameObject CameraUITextElement;
    public Camera viveHeadset;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding aaah");
        if (other.gameObject.CompareTag("RightHand"))
        {
            Debug.Log("Its a hand");
            CameraUITextElement.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 screenPoint = viveHeadset.WorldToViewportPoint(transform.position);
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
        {
            // Button is visible
            CameraUITextElement.SetActive(false);
        } else
        {
            // Button is not visible
            CameraUITextElement.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Colliding EEEEND");
        if (other.gameObject.CompareTag("RightHand"))
        {
            CameraUITextElement.SetActive(false);
        }
    }
}
