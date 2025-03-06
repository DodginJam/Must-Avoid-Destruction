using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Crises_SO;

public class ScreenOptionDisplay : MonoBehaviour, IInteractable, IDisplayable
{
    [field: SerializeField]
    public float InteractionDistance
    { get; set; }

    public bool IsInteractionEnabled
    { get; set; }

    [field: SerializeField]
    public TextMeshProUGUI TitleTextDisplay
    { get; set; }

    [field: SerializeField]
    public TextMeshProUGUI DescriptionTextDisplay
    { get; set; }

    [field: SerializeField]
    public MeshRenderer DisplayMeshRenderer
    { get; set; }

    public ResolutionOption CurrentDisplayedOption
    { get; set; }



    [field: SerializeField]
    public Material HighLightMaterial
    { get; set; }

    public Material DefaultMaterial
    { get; set; }

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
        Debug.Log("Interaction Acheieved.");

        // Need to have already passed the infroamtion held about the current displayed resolution option to the game manager, or atleast pass the resulting score to be analysed for raising or keeping DEFCON value.
    }

    public void DisplayText<T>(T type)
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

    public void ClearText()
    {
        TitleTextDisplay.text = string.Empty;
        TitleTextDisplay.text = string.Empty;
    }

    public void ToggleDisplay(bool setActiveState)
    {
        if (setActiveState)
        {
            DisplayMeshRenderer.material.EnableKeyword("_EMISSION");
        }
        else
        {
            DisplayMeshRenderer.material.DisableKeyword("_EMISSION");
        }

        TitleTextDisplay.enabled = setActiveState;
        DescriptionTextDisplay.enabled = setActiveState;
    }

    public void OnMouseEnter()
    {
        if (IsInteractionEnabled && Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), transform.position) < InteractionDistance)
        {
            IInteractable.ChangeInteractableMaterial(DisplayMeshRenderer, DefaultMaterial);
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
 
    }
}
