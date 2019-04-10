using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deleteButtonScript : MonoBehaviour
{
    
    // Handles the click event for the button
    public void ButtonClicked() {
        // Delete the currently selected object when one exists
        GameObject.Find("SelectedObjectHandler").gameObject.GetComponent<SelectedObjectHandler>().destroySelectedObject();
    }
}
