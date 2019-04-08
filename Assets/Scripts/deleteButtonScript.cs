using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deleteButtonScript : MonoBehaviour
{

    public bool isActive;
    
    // Handles the click event for the button
    public void ButtonClicked() {
        if (!isActive)
        {
            this.isActive = !this.isActive;

            // Change the active color
            var colors = GameObject.Find("DeleteButton").GetComponent<Button>().colors;
            colors.normalColor = new Color(0, 237, 255);
            GameObject.Find("DeleteButton").GetComponent<Button>().colors = colors;

            // Expand button view

            // Disable other first level buttons
            GameObject.Find("CreateButton").GetComponent<Button>().interactable = false;
            GameObject.Find("ManipulateButton").GetComponent<Button>().interactable = false;

        }
        else
        {
            this.isActive = !this.isActive;

            // Change the active color
            var colors = GameObject.Find("DeleteButton").GetComponent<Button>().colors;
            colors.normalColor = new Color(255, 255, 255);
            GameObject.Find("DeleteButton").GetComponent<Button>().colors = colors;

            // Collapse button view

            // Enable other first level buttons
            GameObject.Find("ManipulateButton").GetComponent<Button>().interactable = true;
            GameObject.Find("CreateButton").GetComponent<Button>().interactable = true;
        }
    }
}
