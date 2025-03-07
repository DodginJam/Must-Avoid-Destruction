using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Crises_SO;

public class ScreenOptionDisplay : ScreenDisplay, IInteractable
{
    [field: SerializeField]
    public float InteractionDistance
    { get; set; }

    public bool IsInteractionEnabled
    { get; set; }

    public ResolutionOption CurrentDisplayedOption
    { get; set; }

    [field: SerializeField]
    public Material HighLightMaterial
    { get; set; }

    public Material DefaultMaterial
    { get; set; }

    public DisplayOptions DisplayManagerScript
    { get; private set; }

    void Awake()
    {
        if (DisplayManagerScript == null)
        {
            if (DisplayManagerScript = GameObject.FindObjectOfType<DisplayOptions>())
            {
                Debug.Log("DisplayOptions was unassigned, but found.");
            }
            else
            {
                Debug.Log("Unable to locate DisplayOptions script component in scene.");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DefaultMaterial = DisplayMeshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteraction()
    {
        // On interact, pass the current resolution option selected to the crisis component to then allow it to be seen globally by the state machine states.
        DisplayManagerScript.GameManagerScript.CurrentCrisis.SelectedResolution = CurrentDisplayedOption;
    }

    public override void DisplayText<T>(T type)
    {
        ResolutionOption option = null;
        if (type != null)
        {
            option = type as ResolutionOption;
        }
        else
        {
            Debug.LogError("Error");
        }

        if (option != null)
        {
            TitleTextDisplay.text = option.Title;
            TitleTextDisplay.text = option.Title;
        }
        else
        {
            Debug.LogError("Error");
        }
    }

    public void OnMouseEnter()
    {
        if (IsInteractionEnabled && Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), transform.position) < InteractionDistance)
        {
            IInteractable.ChangeInteractableMaterial(DisplayMeshRenderer, HighLightMaterial);
        }
    }

    public void OnMouseExit()
    {
        if (IsInteractionEnabled && Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), transform.position) < InteractionDistance)
        {
            IInteractable.ChangeInteractableMaterial(DisplayMeshRenderer, DefaultMaterial);
        }
    }

    public void OnMouseOver()
    {
        if (Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), transform.position) < InteractionDistance)
        {
            if (IsInteractionEnabled)
            {
                IInteractable.ChangeInteractableMaterial(DisplayMeshRenderer, HighLightMaterial);
            }
            else
            {
                IInteractable.ChangeInteractableMaterial(DisplayMeshRenderer, DefaultMaterial);
            }
        }
        else
        {
            IInteractable.ChangeInteractableMaterial(DisplayMeshRenderer, DefaultMaterial);

        }
    }
}
