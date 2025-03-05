using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Crises_SO;

public interface IDisplayable
{
    public TextMeshProUGUI TitleTextDisplay
    { get; set; }

    public TextMeshProUGUI DescriptionTextDisplay
    { get; set; }

    public MeshRenderer DisplayMeshRenderer
    { get; set; }

    public void DisplayText(ResolutionOption option)
    {
        if (option != null)
        {
            TitleTextDisplay.text = option.Title;
            TitleTextDisplay.text = option.Title;
        }
        else
        {
            Debug.LogError("The passed ResolutionOption is null.");
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

    public void DisplayResolutionOptions(ResolutionOption resolutionOption)
    {

    }
}
