using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Crises_SO;

public class ScreenTitleDisplay : MonoBehaviour, IDisplayable
{
    [field: SerializeField]
    public TextMeshProUGUI TitleTextDisplay
    { get; set; }

    [field: SerializeField]
    public TextMeshProUGUI DescriptionTextDisplay
    { get; set; }

    [field: SerializeField]
    public MeshRenderer DisplayMeshRenderer
    { get; set; }

    public void DisplayText<T>(T type)
    {
        Crises_SO crisis = null;
        if (type != null)
        {
            crisis = type as Crises_SO;
        }
        else
        {
            Debug.LogError("Error");

        }

        if (crisis != null)
        {
            TitleTextDisplay.text = crisis.CrisisTitle;
            DescriptionTextDisplay.text = crisis.CrisisDescription;
        }
        else
        {
            Debug.LogError("Error");
        }
    }

    public void ClearText()
    {
        TitleTextDisplay.text = string.Empty;
        DescriptionTextDisplay.text = string.Empty;
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
}
