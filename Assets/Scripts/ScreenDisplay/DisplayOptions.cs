using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Crises_SO;

public class DisplayOptions : MonoBehaviour
{
    [field: SerializeField]
    public GameManager GameManagerScript
    {  get; private set; }

    [field: SerializeField]
    public List<ScreenOptionDisplay> ScreenOptionDisplays
    { get; set; }

    [field: SerializeField]
    public ScreenTitleDisplay ScreenTitleDisplay
    { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        DisplayResolutionOptions(GameManagerScript.AllCrisis[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // For each resolution options.
    // Chose the one of the screens in order, place information about that resolution options.
    // Enable it to be interactable.
    public void DisplayResolutionOptions(Crises_SO crisis)
    {
        // Null check the parameter passed.
        if (crisis.OptionsForResolution == null)
        {
            Debug.LogError("The resolution array passed as a parameter is null.");
            return;
        }

        // Ensure that if the screens displays are not qeual to the resolution options, return an error.
        for (int i = 0; i < crisis.OptionsForResolution.Length; i++)
        {
            if (crisis.OptionsForResolution.Length > ScreenOptionDisplays.Count)
            {
                Debug.LogError("The number of resolution options is greater then the screens available to display them.");
                return;
            }
            else if (crisis.OptionsForResolution.Length < ScreenOptionDisplays.Count)
            {
                Debug.LogError("The number of resolution options is less then the screens available to display them.");
                return;
            }

            if (crisis.OptionsForResolution[i] != null)
            {
                ScreenOptionDisplays[i].DisplayText(crisis.OptionsForResolution[i]);
            }
            else
            {
                Debug.LogError("Resolution Option passed as parameter is null.");
            }
        }

        ScreenTitleDisplay.DisplayText(crisis);
    }
}
