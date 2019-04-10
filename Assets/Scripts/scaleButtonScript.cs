using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scaleButtonScript : MonoBehaviour
{
    private bool isActive;

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
            var colors = GameObject.Find("ScaleButton").GetComponent<Button>().colors;
            colors.normalColor = new Color(255, 200, 0);
            GameObject.Find("ScaleButton").GetComponent<Button>().colors = colors;
        }
        else
        {
            isActive = !this.isActive;
            // Change the active color
            var colors = GameObject.Find("ScaleButton").GetComponent<Button>().colors;
            colors.normalColor = new Color(255, 255, 255);
            GameObject.Find("ScaleButton").GetComponent<Button>().colors = colors;
        }
    }

    // Deactivates the button and draws the normal color
    public void forceDefault()
    {
        setActiveStatus(false);
        var colors = GameObject.Find("ScaleButton").GetComponent<Button>().colors;
        colors.normalColor = new Color(255, 255, 255);
        GameObject.Find("ScaleButton").GetComponent<Button>().colors = colors;
    }
}
