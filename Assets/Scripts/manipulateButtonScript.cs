using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manipulateButtonScript : MonoBehaviour
{

    public bool isActive;

    // ClickEventHandler
    public GameObject clickEventHandler;

    // First level buttons
    public Button manipulateButton;
    public Button createButton;
    public Button deleteButton;

    // Second level buttons
    public Button translateButton;
    public Button rotateButton;
    public Button scaleButton;

    // Handles the click event for the button
    public void ButtonClicked() {
        if (!isActive)
        {
            this.isActive = !this.isActive;

            // Change the active color
            var colors = manipulateButton.GetComponent<Button>().colors;
            colors.normalColor = new Color(0, 237, 255);
            manipulateButton.GetComponent<Button>().colors = colors;

            // Expand button view
            translateButton.gameObject.SetActive(true);
            rotateButton.gameObject.SetActive(true);
            scaleButton.gameObject.SetActive(true);

            // Disable other first level buttons
            // createButton.GetComponent<Button>().interactable = false;
            // deleteButton.GetComponent<Button>().interactable = false;

        }
        else
        {
            this.isActive = !this.isActive;

            // Change the active color
            var colors = manipulateButton.GetComponent<Button>().colors;
            colors.normalColor = new Color(255, 255, 255);
            manipulateButton.GetComponent<Button>().colors = colors;

            // Set all active manipulation actions to inactive
            clickEventHandler.gameObject.GetComponent<translateButtonScript>().forceDefault();
            clickEventHandler.gameObject.GetComponent<rotateButtonScript>().forceDefault();
            clickEventHandler.gameObject.GetComponent<scaleButtonScript>().forceDefault();

            // Collapse button view
            translateButton.gameObject.SetActive(false);
            rotateButton.gameObject.SetActive(false);
            scaleButton.gameObject.SetActive(false);

            // Enable other first level buttons
            // createButton.GetComponent<Button>().interactable = true;
            // deleteButton.GetComponent<Button>().interactable = true;
        }
    }
}
