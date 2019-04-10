using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using UnityEngine.UI;

public class SelectedObjectHandler : MonoBehaviour
{

    public GameObject selectedObject;

    public Button translateButton;
    public Button rotateButton;
    public Button scaleButton;

    private Vector3 leftControllerPosition;
    private Vector3 rightControllerPosition;
    private Vector3 leftControllerRotation;
    private Vector3 rightControllerRotation;

    private bool isTranslating;
    private bool isRotating;
    private bool isScaling;

    // Sets the selected laserpointer Object, unsets if null
    public void setSelectedObject(GameObject selectedObject)
    {
        // Case 1: a new object got selected and no other is currently selected
        if(selectedObject != null && !this.selectedObject)
        {
            this.selectedObject = selectedObject;
        } else if(selectedObject == null && this.selectedObject) // Case 2: object got deselected
        {
            this.selectedObject = null;
        } else // Case 3: an object is selected and user wants to select a new one without deselecting the old one
        {
            // Ignore
        }
    }

    // Returns the selected laserpointer object
    public GameObject getSelectedObject()
    {
        return this.selectedObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if selectedOption of Menu is translation, rotation or scaling
    }

    // Listens for the translate event and handles the translation of the selected object if one exists
    public void translateSelectedObject()
    {
        // If SteamVR_Input._default.inActions.ManipulateGrip.GetStateDown(SteamVR_Input_Sources.Right) then
        // check gripPush on right hand, check new position change of controller to previous position
        // Update coordinates of selectedObject.transform with distance
        // i.e. controller goes forward 5 units, selectedObject goes forward 5 units
    }

    // Listens for the rotate event and handles the rotation of the selected object if one exists
    public void rotateSelectedObject()
    {

    }

    // Listens for the scale event and handles the scaling of the selected object if one exists
    public void scaleSelectedObject()
    {

    }

    // Destroys the currently selected gameobject
    public void destroySelectedObject()
    {
        if(this.selectedObject != null)
        Destroy(this.selectedObject);
    }
}
