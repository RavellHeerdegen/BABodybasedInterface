using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class sphereButtonScript : MonoBehaviour
{

    public GameObject spherePrefab;
    public Player player;

    // Handles the click event for the button
    public void ButtonClicked()
    {
        Instantiate(spherePrefab, new Vector3(0, 1, 0), Quaternion.identity);
    }
}
