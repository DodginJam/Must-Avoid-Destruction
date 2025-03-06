using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Advisor : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public Advisor_SO AdvisorScriptableObject
    {  get; private set; }

    public bool IsInteractionEnabled
    { get; set; }

    [field: SerializeField]
    public float InteractionDistance
    { get; set; } = 20f;


    [field: SerializeField]
    public Material HighLightMaterial
    { get; set; }

    public Material DefaultMaterial
    { get; set; }

    public List<MeshRenderer> Parts
    { get; set; } = new List<MeshRenderer>();

    public void Awake()
    {
        if (AdvisorScriptableObject == null)
        {
            Debug.LogError("Must assign a advisor_SO to an NPC");
        }

        DefaultMaterial = GetComponent<MeshRenderer>().material;

        Parts.AddRange(GetComponentsInChildren<MeshRenderer>().ToList<MeshRenderer>());
        Parts.Add(GetComponent<MeshRenderer>());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnInteraction()
    {
        Debug.Log($"Interacted with {AdvisorScriptableObject.Name}, who's role is {AdvisorScriptableObject.TitleRole}");
    }

    public void OnMouseEnter()
    {
        if (IsInteractionEnabled)
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
        
    }
}
