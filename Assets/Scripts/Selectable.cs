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
    private Renderer materialRenderer;
    public Material highlightMaterial;
    public Material normalMaterial;

    // For highlighting
    protected MeshRenderer[] highlightRenderers;
    protected MeshRenderer[] existingRenderers;
    protected GameObject highlightHolder;
    protected SkinnedMeshRenderer[] highlightSkinnedRenderers;
    protected SkinnedMeshRenderer[] existingSkinnedRenderers;
    protected static Material highlightMat;

    // Start is called before the first frame update
    void Start()
    {
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
        if (e.target.name == this.gameObject.name && pointed == false)
        {
            pointed = true;
            // Highlight this object when not already highlighted
            if (highlightHolder == null)
            {
                startHighlighting();
                updateHighlighting();
            }            
        }
    }

    // Handles the pointerOut event of the steamvr laserpointer
    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && pointed == true)
        {
            pointed = false;
            // Delete highlighting when not selected
            if (!selected)
            {
                stopHighlighting();
            }            
        }
    }

    // Handles the pointerClick event of the steamvr laserpointer
    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && pointed == true && selected == false)
        {
            selected = true;
            // Set selectedObject in SelectedObjectHandler to this one
            GameObject.Find("SelectedObjectHandler").GetComponent<SelectedObjectHandler>().setSelectedObject(this.gameObject);

            // Highlight this object permanently
            updateHighlighting();
            this.materialRenderer.enabled = true;
            this.materialRenderer.sharedMaterial = highlightMaterial;

        } else if(e.target.name == this.gameObject.name && pointed == true && selected == true)
        {
            selected = false;
            // Set selectedObject in SelectedObjectHandler to null
            GameObject.Find("SelectedObjectHandler").GetComponent<SelectedObjectHandler>().setSelectedObject(null);

            // Delete highlighting color
            this.materialRenderer.sharedMaterial = normalMaterial;
            this.materialRenderer.enabled = false;
        }
    }

    // Starts to render a highlighting effect to the object, taken from Valve Component Interactable
    private void startHighlighting()
    {
        existingSkinnedRenderers = this.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        highlightHolder = new GameObject("Highlighter");
        highlightSkinnedRenderers = new SkinnedMeshRenderer[existingSkinnedRenderers.Length];

        for (int skinnedIndex = 0; skinnedIndex < existingSkinnedRenderers.Length; skinnedIndex++)
        {
            SkinnedMeshRenderer existingSkinned = existingSkinnedRenderers[skinnedIndex];

            GameObject newSkinnedHolder = new GameObject("SkinnedHolder");
            newSkinnedHolder.transform.parent = highlightHolder.transform;
            SkinnedMeshRenderer newSkinned = newSkinnedHolder.AddComponent<SkinnedMeshRenderer>();
            Material[] materials = new Material[existingSkinned.sharedMaterials.Length];
            for (int materialIndex = 0; materialIndex < materials.Length; materialIndex++)
            {
                materials[materialIndex] = highlightMat;
            }

            newSkinned.sharedMaterials = materials;
            newSkinned.sharedMesh = existingSkinned.sharedMesh;
            newSkinned.rootBone = existingSkinned.rootBone;
            newSkinned.updateWhenOffscreen = existingSkinned.updateWhenOffscreen;
            newSkinned.bones = existingSkinned.bones;

            highlightSkinnedRenderers[skinnedIndex] = newSkinned;
        }

        MeshFilter[] existingFilters = this.GetComponentsInChildren<MeshFilter>(true);
        existingRenderers = new MeshRenderer[existingFilters.Length];
        highlightRenderers = new MeshRenderer[existingFilters.Length];

        for (int filterIndex = 0; filterIndex < existingFilters.Length; filterIndex++)
        {
            MeshFilter existingFilter = existingFilters[filterIndex];
            MeshRenderer existingRenderer = existingFilter.GetComponent<MeshRenderer>();

            if (existingFilter == null || existingRenderer == null)
                continue;

            GameObject newFilterHolder = new GameObject("FilterHolder");
            newFilterHolder.transform.parent = highlightHolder.transform;
            MeshFilter newFilter = newFilterHolder.AddComponent<MeshFilter>();
            newFilter.sharedMesh = existingFilter.sharedMesh;
            MeshRenderer newRenderer = newFilterHolder.AddComponent<MeshRenderer>();

            Material[] materials = new Material[existingRenderer.sharedMaterials.Length];
            for (int materialIndex = 0; materialIndex < materials.Length; materialIndex++)
            {
                materials[materialIndex] = highlightMat;
            }
            newRenderer.sharedMaterials = materials;

            highlightRenderers[filterIndex] = newRenderer;
            existingRenderers[filterIndex] = existingRenderer;
        }
    }

    // Updates the highlighting effect to the object, taken from Valve Component Interactable
    private void updateHighlighting()
    {
        if (highlightHolder == null)
            return;

        for (int skinnedIndex = 0; skinnedIndex < existingSkinnedRenderers.Length; skinnedIndex++)
        {
            SkinnedMeshRenderer existingSkinned = existingSkinnedRenderers[skinnedIndex];
            SkinnedMeshRenderer highlightSkinned = highlightSkinnedRenderers[skinnedIndex];

            if (existingSkinned != null && highlightSkinned != null)
            {
                highlightSkinned.transform.position = existingSkinned.transform.position;
                highlightSkinned.transform.rotation = existingSkinned.transform.rotation;
                highlightSkinned.transform.localScale = existingSkinned.transform.lossyScale;
                highlightSkinned.localBounds = existingSkinned.localBounds;
                highlightSkinned.enabled = existingSkinned.enabled && existingSkinned.gameObject.activeInHierarchy;

                int blendShapeCount = existingSkinned.sharedMesh.blendShapeCount;
                for (int blendShapeIndex = 0; blendShapeIndex < blendShapeCount; blendShapeIndex++)
                {
                    highlightSkinned.SetBlendShapeWeight(blendShapeIndex, existingSkinned.GetBlendShapeWeight(blendShapeIndex));
                }
            }
            else if (highlightSkinned != null)
                highlightSkinned.enabled = false;

        }

        for (int rendererIndex = 0; rendererIndex < highlightRenderers.Length; rendererIndex++)
        {
            MeshRenderer existingRenderer = existingRenderers[rendererIndex];
            MeshRenderer highlightRenderer = highlightRenderers[rendererIndex];

            if (existingRenderer != null && highlightRenderer != null)
            {
                highlightRenderer.transform.position = existingRenderer.transform.position;
                highlightRenderer.transform.rotation = existingRenderer.transform.rotation;
                highlightRenderer.transform.localScale = existingRenderer.transform.lossyScale;
                highlightRenderer.enabled = existingRenderer.enabled && existingRenderer.gameObject.activeInHierarchy;
            }
            else if (highlightRenderer != null)
                highlightRenderer.enabled = false;
        }
    }

    // Destroys the highlighting effect for the object, taken from Valve Component Interactable
    private void stopHighlighting()
    {
        if (highlightHolder != null)
            Destroy(highlightHolder);
    }
}
