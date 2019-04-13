using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class SelectedObjectHandler : MonoBehaviour
{

    public GameObject selectedObject;

    public Hand leftHand;
    public Hand rightHand;

    public SteamVR_Action_Boolean grapGrip;
    public SteamVR_Input_Sources inputRightHand = SteamVR_Input_Sources.RightHand;

    public Button translateButton;
    public Button rotateButton;
    public Button scaleButton;

    // Last left controller coordinates
    Vector3 lastLeftControllerPosition;
    Vector3 lastLeftControllerRotation;

    // Last right controller coordinates
    Vector3 lastRightControllerPosition;
    Vector3 lastRightControllerRotation;

    private Vector3 leftControllerPosition;
    private Vector3 rightControllerPosition;
    private Vector3 leftControllerRotation;
    private Vector3 rightControllerRotation;

    private bool translationActive;
    private bool rotationActive;
    private bool scalingActive;

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
            this.selectedObject.GetComponent<Selectable>().forceUnselect();
            this.selectedObject = selectedObject;
        }
    }

    // Sets the status of the translationActive variable
    public void setTranslationActiveStatus(bool newstatus)
    {
        this.translationActive = newstatus;
    }

    // Sets the status of the rotationActive variable
    public void setRotationActiveStatus(bool newstatus)
    {
        this.rotationActive = newstatus;
    }

    // Sets the status of the scalingActive variable
    public void setScalingActiveStatus(bool newstatus)
    {
        this.scalingActive = newstatus;
    }

    // Returns the selected laserpointer object
    public GameObject getSelectedObject()
    {
        return this.selectedObject;
    }

    // Returns the active status of the translation variable
    public bool getTranslationActiveStatus()
    {
        return this.translationActive;
    }

    // Returns the active status of the rotation variable
    public bool getRotationActiveStatus()
    {
        return this.rotationActive;
    }

    // Returns the active status of the scaling variable
    public bool getScalingActiveStatus()
    {
        return this.scalingActive;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get current controller coordinates
        leftControllerPosition = leftHand.transform.position;
        lastLeftControllerRotation = leftHand.transform.eulerAngles;
        rightControllerPosition = rightHand.transform.position;
        rightControllerRotation = rightHand.transform.eulerAngles;

        // Check if selectedOption of Menu is translation, rotation or scaling
        // Translation active
        if (getTranslationActiveStatus())
        {
            Debug.Log("Translation active");
            // True when grip button of right controller is pressed down
            Debug.Log(grapGrip.GetState(inputRightHand));
            if (grapGrip.GetState(inputRightHand))
            {
                Debug.Log("Grip button pushed down");
                if (lastRightControllerPosition != rightControllerPosition)
                {
                    Debug.Log("Recent and current position differ");
                    Vector3 differenceVector = new Vector3();
                    differenceVector.x = (rightControllerPosition.x - lastRightControllerPosition.x) * 5;
                    differenceVector.y = (rightControllerPosition.y - lastRightControllerPosition.y) * 5;
                    differenceVector.z = (rightControllerPosition.z - lastRightControllerPosition.z) * 5;

                    if (selectedObject)
                    selectedObject.transform.position = selectedObject.transform.position + differenceVector;
                }
            }
        }
        else if (getRotationActiveStatus()) // Rotation active
        {

        }
        else if(getScalingActiveStatus()) // Scaling active
        {

        }

        // Set new coordinates for next update
        lastLeftControllerPosition = leftHand.transform.position;
        lastLeftControllerRotation = leftHand.transform.eulerAngles;
        lastRightControllerPosition = rightHand.transform.position;
        lastRightControllerRotation = rightHand.transform.eulerAngles;
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
        if (this.selectedObject)
        {
            this.selectedObject.GetComponent<Selectable>().destroyOnCommand();
            this.selectedObject = null;
        }
    }
}
