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
        Debug.Log("Interaction Acheieved.");

        // Need to pass the infroamtion held about the current displayed resolution option to the game manager, or atleast pass the resulting score to be analysed for raising or keeping DEFCON value.
    }
}
