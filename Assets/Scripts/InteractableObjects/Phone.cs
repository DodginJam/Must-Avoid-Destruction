using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public float InteractionDistance
    { get; set; }

    public bool IsInteractionEnabled
    { get; set; } = true;

    public MeshRenderer[] Parts
    { get; set; }

    [field: SerializeField]
    public Material HighLightMaterial
    { get; set; }

    public Material DefaultMaterial
    { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Parts = GetComponentsInChildren<MeshRenderer>();

        DefaultMaterial = Parts[0].material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteraction()
    {
        Debug.Log("Interaction Acheieved.");
    }

    public void OnMouseEnter()
    {
        if (IsInteractionEnabled && Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), transform.position) <= InteractionDistance)
        {
            foreach (MeshRenderer mesh in Parts)
            {
                IInteractable.ChangeInteractableMaterial(mesh, HighLightMaterial);
            }
        }
    }

    public void OnMouseExit()
    {
        if (IsInteractionEnabled)
        {
            foreach (MeshRenderer mesh in Parts)
            {
                IInteractable.ChangeInteractableMaterial(mesh, DefaultMaterial);
            }
        }
    }

    public void OnMouseOver()
    {
        if (Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), transform.position) <= InteractionDistance)
        {
            if (IsInteractionEnabled)
            {
                foreach (MeshRenderer mesh in Parts)
                {
                    IInteractable.ChangeInteractableMaterial(mesh, HighLightMaterial);
                }
            }
            else
            {
                foreach (MeshRenderer mesh in Parts)
                {
                    IInteractable.ChangeInteractableMaterial(mesh, DefaultMaterial);
                }
            }
        }
        else
        {
            foreach (MeshRenderer mesh in Parts)
            {
                IInteractable.ChangeInteractableMaterial(mesh, DefaultMaterial);
            }
        }
    }
}
