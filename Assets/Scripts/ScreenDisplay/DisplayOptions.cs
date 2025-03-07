using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Crises_SO;

public class DisplayOptions : MonoBehaviour
{
    [field: SerializeField]
    public List<ScreenOptionDisplay> ScreenOptionDisplays
    { get; set; }

    [field: SerializeField]
    public ScreenTitleDisplay ScreenTitleDisplay
    { get; set; }

    public GameManager GameManagerScript
    { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if(TryGetComponent<GameManager>(out GameManager gameManager))
        {
            GameManagerScript = gameManager;
        }
        else
        {
            Debug.LogError("Unable to locate gamemanager script.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // For each resolution options.
    // Chose the one of the screens in order, place information about that resolution options.
    // Enable it to be interactable.
    // Store the resolution reference to the screens display script.
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

            // Display the resolution option to the relevent display screena and store the reference to the resolution option inside the screen display script.
            if (crisis.OptionsForResolution[i] != null)
            {
                ScreenOptionDisplays[i].DisplayText(crisis.OptionsForResolution[i]);
                ScreenOptionDisplays[i].CurrentDisplayedOption = crisis.OptionsForResolution[i];

                if (ScreenOptionDisplays[i].CurrentDisplayedOption != null && ScreenOptionDisplays[i].CurrentDisplayedOption == crisis.OptionsForResolution[i])
                {
                    Debug.Log("Successful assignment of current resolution to screenOptionDisplay");
                }
                else
                {
                    Debug.LogError("Error in assignment of current resolution to screenOptionDisplay");
                }
            }
            else
            {
                Debug.LogError("Resolution Option passed as parameter is null.");
            }
        }

        // Set the screens to be interactable.
        IInteractable.EnableInteraction(ScreenOptionDisplays.ToArray(), true);

        // Display the text.
        ScreenTitleDisplay.DisplayText(crisis);
    }
}
