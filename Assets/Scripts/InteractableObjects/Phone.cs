using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour, IInteractable, ISoundPlayer
{
    [field: SerializeField]
    public float InteractionDistance
    { get; set; }

    public bool IsInteractionEnabled
    { get; set; }

    [field: SerializeField]
    public Material HighLightMaterial
    { get; set; }

    public Material DefaultMaterial
    { get; set; }


    public PhoneStatus Status
    { get; set; } = PhoneStatus.Quiet;


    [field: SerializeField, Header("Sound")]
    public AudioSource AudioSource
    { get; set; }

    [field: SerializeField]
    public AudioClip PhoneRinging
    { get; set; }

    [field: SerializeField]
    public AudioClip PhonePickUp
    { get; set; }

    [field: SerializeField]
    public AudioClip PhonePutDown
    { get; set; }

    [field: SerializeField]
    public AudioClip[] PhoneTalking
    { get; set; }

    public bool StartedPlayingNewSound
    { get; set; } = false;

    public MeshRenderer[] PartsMeshRenderer
    { get; set; }

    public Material[] PartsDefaultMaterials
    { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        PartsMeshRenderer = GetComponentsInChildren<MeshRenderer>();
        PartsDefaultMaterials = new Material[PartsMeshRenderer.Length];

        for (int i = 0; i < PartsMeshRenderer.Length; i++)
        {
            PartsDefaultMaterials[i] = PartsMeshRenderer[i].material;
        }
        
        // Being assigned but not used at moment due to need to rework interactiable interface.
        DefaultMaterial = PartsMeshRenderer[0].material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteraction()
    {
        if (Status == PhoneStatus.Ringing)
        {
            AudioSource.Stop();
            IsInteractionEnabled = false;
            Status = PhoneStatus.PickedUp;
        }
    }

    public void OnMouseEnter()
    {
        if (IsInteractionEnabled && Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), transform.position) <= InteractionDistance)
        {
            for (int i = 0; i < PartsMeshRenderer.Length; i++)
            {
                IInteractable.ChangeInteractableMaterial(PartsMeshRenderer[i], HighLightMaterial);
            }
        }
    }

    public void OnMouseExit()
    {
        if (IsInteractionEnabled)
        {
            for (int i = 0; i < PartsMeshRenderer.Length; i++)
            {
                IInteractable.ChangeInteractableMaterial(PartsMeshRenderer[i], PartsDefaultMaterials[i]);
            }
        }
    }

    public void OnMouseOver()
    {
        if (Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), transform.position) <= InteractionDistance)
        {
            if (IsInteractionEnabled)
            {
                foreach (MeshRenderer mesh in PartsMeshRenderer)
                {
                    IInteractable.ChangeInteractableMaterial(mesh, HighLightMaterial);
                }
            }
            else
            {
                for (int i = 0; i < PartsMeshRenderer.Length; i++)
                {
                    IInteractable.ChangeInteractableMaterial(PartsMeshRenderer[i], PartsDefaultMaterials[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < PartsMeshRenderer.Length; i++)
            {
                IInteractable.ChangeInteractableMaterial(PartsMeshRenderer[i], PartsDefaultMaterials[i]);
            }
        }
    }

    public enum PhoneStatus
    {
        Quiet,
        Ringing,
        PickedUp,
        Talking,
        Putdown
    }
}
