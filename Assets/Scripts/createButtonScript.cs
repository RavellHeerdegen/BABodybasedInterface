using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createButtonScript : MonoBehaviour
{

    public bool isActive;

    // First level buttons
    public Button manipulateButton;
    public Button createButton;
    public Button deleteButton;

    // Second level buttons
    public Button cubebutton;
    public Button spherebutton;
    public Button cylinderButton;

    // Handles the click event for the button
    public void ButtonClicked() {
        if (!isActive) {
            this.isActive = !this.isActive;

            // Change the active color
            var colors = createButton.GetComponent<Button>().colors;
            colors.normalColor = new Color(0, 237, 255);
            createButton.GetComponent<Button>().colors = colors;

            // Expand button view
            cubebutton.gameObject.SetActive(true);
            spherebutton.gameObject.SetActive(true);
            cylinderButton.gameObject.SetActive(true);

            // Disable other first level buttons
            // manipulateButton.GetComponent<Button>().interactable = false;
            // deleteButton.GetComponent<Button>().interactable = false;

        } else {
            this.isActive = !this.isActive;

            // Change the active color
            var colors = createButton.GetComponent<Button>().colors;
            colors.normalColor = new Color(255, 255, 255);
            createButton.GetComponent<Button>().colors = colors;

            // Collapse button view
            cubebutton.gameObject.SetActive(false);
            spherebutton.gameObject.SetActive(false);
            cylinderButton.gameObject.SetActive(false);

            // Enable other first level buttons
            // manipulateButton.GetComponent<Button>().interactable = true;
            // deleteButton.GetComponent<Button>().interactable = true;
        }
    }
}
