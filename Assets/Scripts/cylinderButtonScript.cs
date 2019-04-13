using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class cylinderButtonScript : MonoBehaviour
{

    public GameObject cylinderPrefab;
    public Player player;

    // Handles the click event for the button
    public void ButtonClicked()
    {
        Instantiate(cylinderPrefab, new Vector3(0, 1, 0), Quaternion.identity);
    }
}
