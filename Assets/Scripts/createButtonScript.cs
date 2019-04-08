using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createButtonScript : MonoBehaviour
{

    public bool isActive;

    public Button cubebutton;
    public Button spherebutton;
    public Button cylinderButton;

    // Handles the click event for the button
    public void ButtonClicked() {
        Debug.Log("PAINIS");
        if (!isActive) {
            this.isActive = !this.isActive;

            // Change the active color
            var colors = GameObject.Find("CreateButton").GetComponent<Button>().colors;
            colors.normalColor = new Color(0, 237, 255);
            GameObject.Find("CreateButton").GetComponent<Button>().colors = colors;

            // Expand button view
            cubebutton.gameObject.SetActive(true);
            spherebutton.gameObject.SetActive(true);
            cylinderButton.gameObject.SetActive(true);

            // Disable other first level buttons
            GameObject.Find("ManipulateButton").GetComponent<Button>().interactable = false;
            GameObject.Find("DeleteButton").GetComponent<Button>().interactable = false;

        } else {
            this.isActive = !this.isActive;

            // Change the active color
            var colors = GameObject.Find("CreateButton").GetComponent<Button>().colors;
            colors.normalColor = new Color(255, 255, 255);
            GameObject.Find("CreateButton").GetComponent<Button>().colors = colors;

            // Collapse button view
            cubebutton.gameObject.SetActive(false);
            spherebutton.gameObject.SetActive(false);
            cylinderButton.gameObject.SetActive(false);

            // Enable other first level buttons
            GameObject.Find("ManipulateButton").GetComponent<Button>().interactable = true;
            GameObject.Find("DeleteButton").GetComponent<Button>().interactable = true;
        }
    }
}
