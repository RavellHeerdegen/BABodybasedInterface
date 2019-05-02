using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class SelectedObjectHandler : MonoBehaviour
{

    public GameObject selectedObject;

    // Hands
    public Hand leftHand;
    public Hand rightHand;

    // SteamVR Actions and Inputs
    public SteamVR_Action_Boolean triggerDown;
    public SteamVR_Action_Boolean grapGrip;
    public SteamVR_Input_Sources inputLeftHand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources inputRightHand = SteamVR_Input_Sources.RightHand;

    // UI Buttons
    public Button translateButton;
    public Button rotateButton;
    public Button scaleButton;

    // Raycast for Deselection
    RaycastHit hit;
    Ray ray;

    // Last left controller coordinates
    Vector3 lastLeftControllerPosition;
    Quaternion lastLeftControllerRotation;

    // Last right controller coordinates
    Vector3 lastRightControllerPosition;
    Quaternion lastRightControllerRotation;

    // Legacy selectedObject position and Parent object
    Vector3 legacySelectedObjectPosition;

    // Current left controller pos and rot, current right controller pos and rot
    private Vector3 leftControllerPosition;
    private Vector3 rightControllerPosition;
    private Quaternion leftControllerRotation;
    private Quaternion rightControllerRotation;

    // last and current distance between controllers
    private float distanceOfControllers;
    private float lastDistanceOfControllers;

    // States for manipulation tasks
    private bool translationActive;
    private bool rotationActive;
    private bool scalingActive;
    private bool rotationFreezed;

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
        //Debug.Log(rightHand.transform.rotation);
        // Get current controller coordinates
        leftControllerPosition = leftHand.transform.position;
        lastLeftControllerRotation = leftHand.transform.rotation;
        rightControllerPosition = rightHand.transform.position;
        rightControllerRotation = rightHand.transform.rotation;
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
        lastLeftControllerRotation = leftHand.transform.rotation;
        lastRightControllerPosition = rightHand.transform.position;
        lastRightControllerRotation = rightHand.transform.rotation;
        lastDistanceOfControllers = Vector3.Distance(rightControllerPosition, leftControllerPosition);
    }

    // Listens for the translate event and handles the translation of the selected object if one exists
    public void translateSelectedObject()
    {
        // True when grip button of right controller is pressed down
        //Debug.Log(grapGrip.GetState(inputRightHand));
        if (grapGrip.GetState(inputRightHand))
        {
            if (lastRightControllerPosition != rightControllerPosition)
            {
                // Calculate difference between last and current position
                Vector3 differenceVector = new Vector3();
                differenceVector.x = (rightControllerPosition.x - lastRightControllerPosition.x) * 10;
                differenceVector.y = (rightControllerPosition.y - lastRightControllerPosition.y) * 10;
                differenceVector.z = (rightControllerPosition.z - lastRightControllerPosition.z) * 10;

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
        if (grapGrip.GetState(inputRightHand))
        {
            if (selectedObject)
            {
                //// No parent
                //if (selectedObject.transform.parent == null)
                //{
                //    // legacySelectedObjectPosition = selectedObject.transform.position;

                //    selectedObjectParent = new GameObject();
                //    selectedObjectParent.transform.rotation = Quaternion.Euler(Vector3.zero);
                //    selectedObjectParent.transform.position = selectedObject.transform.position;
                //    selectedObjectParent.transform.rotation = rightHand.transform.rotation;

                //    selectedObject.transform.SetParent(selectedObjectParent.transform);
                //    selectedObject.GetComponent<Rigidbody>().isKinematic = true;
                //}
                //else // Parent set but rotation still active
                //{
                //    selectedObject.transform.rotation *= rightHand.transform.rotation;
                //}

                // No parent
                if (selectedObject.transform.parent == null)
                {
                    legacySelectedObjectPosition = selectedObject.transform.position;

                    selectedObject.transform.SetParent(rightHand.transform);
                    rightHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    selectedObject.GetComponent<Rigidbody>().isKinematic = true;
                } else // Parent set but rotation still active
                {
                    selectedObject.transform.position = legacySelectedObjectPosition;
                }
            }
        } else
        {
            // Parent set but rotation inactive
            if (selectedObject && selectedObject.transform.parent)
            {
                selectedObject.GetComponent<Rigidbody>().isKinematic = false;
                selectedObject.transform.SetParent(null);
                rightHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }

    // Listens for the scale event and handles the scaling of the selected object if one exists
    public void scaleSelectedObject()
    {
        if (grapGrip.GetState(inputRightHand) && grapGrip.GetState(inputLeftHand))
        {
            if (lastRightControllerPosition != rightControllerPosition && lastLeftControllerPosition != leftControllerPosition)
            {
                // Distance is bigger than before
                if (distanceOfControllers > lastDistanceOfControllers && distanceOfControllers > 0.3f)
                {
                    if (selectedObject)
                    {
                        selectedObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                    }
                }
                // Distance is lower than before
                else if (distanceOfControllers < lastDistanceOfControllers && distanceOfControllers > 0.3f)
                {
                    if (selectedObject)
                    {
                        selectedObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
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
