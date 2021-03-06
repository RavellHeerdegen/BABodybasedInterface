using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class Selectable : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    private bool selected;
    private bool pointed;

    // Selection Materials and Renderer
    Renderer rend;
    public Material highlightedOutlineMaterial;
    public Material highlightMaterial;
    public Material normalMaterial;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        // Add pointer events for pointer
        laserPointer = GameObject.Find("RightHand").gameObject.GetComponent<SteamVR_LaserPointer>();
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Returns the status of the selected attribute
    public bool getSelectedStatus()
    {
        return this.selected;
    }

    // Returns the status of the pointed attribute
    public bool getPointedStatus()
    {
        return this.pointed;
    }

    // Handles the pointerIn event of the steamvr laserpointer
    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (this.gameObject)
        {
            if (e.target.gameObject == this.gameObject && pointed == false)
            {
                pointed = true;
                // Highlight this object when not already highlighted
                if (!selected)
                {
                    rend.material = highlightMaterial;
                }
            }
        }
    }

    // Handles the pointerOut event of the steamvr laserpointer
    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (this.gameObject)
        {
            if (e.target.gameObject == this.gameObject && pointed == true)
            {
                pointed = false;
                // Delete highlighting when not selected
                if (!selected)
                {
                    rend.material = normalMaterial;
                }
            }
        }
    }

    // Handles the pointerClick event of the steamvr laserpointer
    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (this.gameObject)
        {
            // Target of raycasthit is this and this is not selected
            if (e.target.gameObject == this.gameObject && pointed == true && selected == false)
            {
                selected = true;
                // Set selectedObject in SelectedObjectHandler to this one
                if (this.gameObject)
                GameObject.Find("SelectedObjectHandler").GetComponent<SelectedObjectHandler>().setSelectedObject(this.gameObject);

                // Highlight this object permanently
                rend.material = highlightedOutlineMaterial;
                rend.material.shader = Shader.Find("Valve/VR/Highlight");

            }
            // Target of raycasthit is this and this is selected
            else if (e.target.gameObject == this.gameObject && pointed == true && selected == true)
            {
                GameObject.Find("SelectedObjectHandler").GetComponent<SelectedObjectHandler>().setSelectedObject(null);

                forceUnselect();

            } // Target of raycasthit is NOT this and no other vrobject is pointed at
            //else if ((e.target.gameObject.tag != "Button" && e.target.gameObject.tag != "SelectableVRObject") &&
            //    pointed == false)
            //{
            //    // Check if colliding with a button
            //    GameObject[] activeButtonsGameObject = GameObject.FindGameObjectsWithTag("Button");
            //    bool foundCollision = false;

            //    foreach (var button in activeButtonsGameObject)
            //    {
            //        if (button.GetComponent<CollisionHandler>().getIsColliding())
            //        {
            //            foundCollision = true;
            //        }
            //    }

            //    if (!foundCollision)
            //    {
            //        if (selected)
            //        {
            //            GameObject.Find("SelectedObjectHandler").GetComponent<SelectedObjectHandler>().setSelectedObject(null);

            //            forceUnselect();
            //        }
            //        else
            //        {
            //            GameObject.Find("SelectedObjectHandler").GetComponent<SelectedObjectHandler>().setSelectedObject(null);
            //        }
            //    }
            //}
        }
    }

    // Forces this object to get unselected and normal material
    public void forceUnselect()
    {
        selected = false;

        // Delete highlighting color
        rend.material = normalMaterial;
        rend.material.shader = Shader.Find("Standard");
    }

    // Destroys this gameObject
    public void destroyOnCommand()
    {
        // Sets inactive
        this.gameObject.SetActive(false);
    }
}
