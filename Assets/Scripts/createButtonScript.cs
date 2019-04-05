﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createButtonScript : MonoBehaviour
{

    public bool isActive;

    public void buttonClicked() {
        Debug.Log("PAINIS");
        if (!isActive) {
            this.isActive = !this.isActive;

            // Change the active color
            var colors = GameObject.Find("CreateButton").GetComponent<Button>().colors;
            colors.normalColor = new Color(0, 237, 255);
            GameObject.Find("CreateButton").GetComponent<Button>().colors = colors;

            // Expand button view

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

            // Enable other first level buttons
            GameObject.Find("ManipulateButton").GetComponent<Button>().interactable = true;
            GameObject.Find("DeleteButton").GetComponent<Button>().interactable = true;
        }
    }
}
