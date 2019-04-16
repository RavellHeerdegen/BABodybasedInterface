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
    public SteamVR_Input_Sources inputLeftHand = SteamVR_Input_Sources.LeftHand;
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

    // Current left controller pos and rot, current right controller pos and rot
    private Vector3 leftControllerPosition;
    private Vector3 rightControllerPosition;
    private Vector3 leftControllerRotation;
    private Vector3 rightControllerRotation;

    // last and current distance between controllers
    private float distanceOfControllers;
    private float lastDistanceOfControllers;

    // States for manipulation tasks
    private bool translationActive;
    private bool rotationActive;
    private bool scalingActive;

    // Sets the selected laserpointer Object, unsets if null
    public void setSelectedObject(GameObject selectedObject)
    {
        // Case 1: a new object got selected and no other is currently selected
        if (selectedObject != null && !this.selectedObject)
        {
            this.selectedObject = selectedObject;
        }
        else if (selectedObject == null && this.selectedObject) // Case 2: object got deselected
        {
            this.selectedObject = null;
        }
        else // Case 3: an object is selected and user wants to select a new one without deselecting the old one
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
        distanceOfControllers = Vector3.Distance(rightControllerPosition, leftControllerPosition);

        // Check if selectedOption of Menu is translation, rotation or scaling
        // Translation active
        if (getTranslationActiveStatus())
        {
            translateSelectedObject();
        }
        else if (getRotationActiveStatus()) // Rotation active
        {
            rotateSelectedObject();
        }
        else if (getScalingActiveStatus()) // Scaling active
        {
            scaleSelectedObject();
        }

        // Set new coordinates for next update and calculate distance between the controllers
        lastLeftControllerPosition = leftHand.transform.position;
        lastLeftControllerRotation = leftHand.transform.eulerAngles;
        lastRightControllerPosition = rightHand.transform.position;
        lastRightControllerRotation = rightHand.transform.eulerAngles;
        lastDistanceOfControllers = Vector3.Distance(rightControllerPosition, leftControllerPosition);
    }

    // Listens for the translate event and handles the translation of the selected object if one exists
    public void translateSelectedObject()
    {
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
                {
                    selectedObject.transform.position = selectedObject.transform.position + differenceVector;
                }  
            }
        }
    }

    // Listens for the rotate event and handles the rotation of the selected object if one exists
    public void rotateSelectedObject()
    {
        Debug.Log(grapGrip.GetState(inputRightHand));
        if (grapGrip.GetState(inputRightHand))
        {
            Debug.Log("Grip button pushed down");
            if (lastRightControllerPosition != rightControllerPosition)
            {
                Debug.Log("Recent and current position differ");
                Vector3 differenceVector = new Vector3();
                differenceVector.x = (rightControllerPosition.x - lastRightControllerPosition.x) * 50;
                differenceVector.y = (rightControllerPosition.y - lastRightControllerPosition.y) * 50;
                differenceVector.z = (rightControllerPosition.z - lastRightControllerPosition.z) * 50;

                if (selectedObject)
                {
                    selectedObject.transform.eulerAngles = selectedObject.transform.eulerAngles + new Vector3(differenceVector.z, differenceVector.y, differenceVector.x);
                }
            }
        }
    }

    // Listens for the scale event and handles the scaling of the selected object if one exists
    public void scaleSelectedObject()
    {
        Debug.Log(grapGrip.GetState(inputRightHand));
        if (grapGrip.GetState(inputRightHand) && grapGrip.GetState(inputLeftHand))
        {
            Debug.Log("Grip button pushed down on both controllers");
            if (lastRightControllerPosition != rightControllerPosition || lastLeftControllerPosition != leftControllerPosition)
            {
                Debug.Log("Recent and current position differ");
                if (distanceOfControllers > lastDistanceOfControllers)
                {
                    Debug.Log("Distance is bigger now");
                    if (selectedObject)
                    {
                        selectedObject.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
                    }
                }
                else if (distanceOfControllers < lastDistanceOfControllers)
                {
                    Debug.Log("Distance is smaller now");
                    if (selectedObject)
                    {
                        selectedObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
                    }
                }
            }
        }
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
