using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class cubeButtonScript : MonoBehaviour
{
    public GameObject cubePrefab;
    public Camera playerCamera;

    // Handles the click event for the button
    public void ButtonClicked()
    {
        Instantiate(cubePrefab, new Vector3(0, 2, 0), Quaternion.identity);
    }
}
