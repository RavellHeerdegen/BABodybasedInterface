using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereButtonScript : MonoBehaviour
{

    public GameObject spherePrefab;
    public Camera playerCamera;

    // Handles the click event for the button
    public void ButtonClicked()
    {
        Instantiate(spherePrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
