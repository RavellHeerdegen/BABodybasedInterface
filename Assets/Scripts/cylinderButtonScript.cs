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
        Instantiate(cylinderPrefab, player.transform.position + new Vector3(2, 2, 0), Quaternion.identity);
    }
}
