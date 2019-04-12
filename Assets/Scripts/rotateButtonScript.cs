using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotateButtonScript : MonoBehaviour
{

    private bool isActive;

    public SelectedObjectHandler selectedObjectHandler;
    public GameObject clickEventHandler;

    public Button translateButton;
    public Button rotateButton;
    public Button scaleButton;

    // Sets the active status
    public void setActiveStatus(bool newStatus)
    {
        this.isActive = newStatus;
    }

    // Returns the active status
    public bool getActiveStatus()
    {
        return this.isActive;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Handles the click event for the button
    public void ButtonClicked()
    {
        if (!isActive)
        {
            isActive = !this.isActive;
            // Change the active color
            var colors = rotateButton.GetComponent<Button>().colors;
            colors.normalColor = new Color(255, 200, 0);
            rotateButton.GetComponent<Button>().colors = colors;

            // Set the active status in the selectedObjectHandler
            selectedObjectHandler.GetComponent<SelectedObjectHandler>().setRotationActiveStatus(true);

            // force defaults of other manipulation actions
            clickEventHandler.GetComponent<translateButtonScript>().forceDefault();
            clickEventHandler.GetComponent<scaleButtonScript>().forceDefault();

        }
        else
        {
            // Back to default
            forceDefault();
        }
    }

    // Deactivates the button and draws the normal color
    public void forceDefault()
    {
        setActiveStatus(false);
        var colors = rotateButton.GetComponent<Button>().colors;
        colors.normalColor = new Color(255, 255, 255);
        rotateButton.GetComponent<Button>().colors = colors;

        // Set the active status in the selectedObjectHandler
        selectedObjectHandler.GetComponent<SelectedObjectHandler>().setRotationActiveStatus(false);
    }
}
