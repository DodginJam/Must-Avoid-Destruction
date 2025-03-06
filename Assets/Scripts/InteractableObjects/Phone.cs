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

    public MeshRenderer[] Parts
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

    public enum PhoneStatus
    {
        Quiet,
        Ringing,
        PickedUp,
        Talking,
        Putdown
    }
}
