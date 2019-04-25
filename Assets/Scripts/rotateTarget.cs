using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateTarget : MonoBehaviour
{
    public Transform targetTransform;
    public bool manipulationActivated;

    GameObject manipulationObj;

    bool objectCreated = false;
    void Start()
    {

    }

    void Update()
    {
        if (manipulationActivated)
        {
            if (!objectCreated)
            {
                //create empty gameobject
                manipulationObj = new GameObject();
                //0-rotation
                manipulationObj.transform.rotation = Quaternion.Euler(Vector3.zero);
                //verschiebe empty gameobject auf position des zielobjectes
                manipulationObj.transform.position = targetTransform.transform.position;
                //übertrage controller orientation auf empty gameobject
                manipulationObj.transform.rotation = transform.rotation;
                //childe das zielobjekt
                targetTransform.parent = manipulationObj.transform;
                objectCreated = true;
            }
            else {
                //übertrage controller orientation auf empty gameobject
                manipulationObj.transform.rotation = transform.rotation;
            }
            
        }
        else
        {
            //unchilde
            targetTransform.parent = null;
            //deleteobject
            Object.Destroy(manipulationObj);

            objectCreated = false;
        }
    }
}
