using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cylinderButtonScript : MonoBehaviour
{

    public GameObject cylinderPrefab;
    public Camera playerCamera;

    // Handles the click event for the button
    public void ButtonClicked()
    {
        Instantiate(cylinderPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
